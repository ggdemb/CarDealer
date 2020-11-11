using CarDealer.Application.ExternalContracts;
using CarDealer.Domain.Common;
using CarDealer.Domain.Sale.Car;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Persistence
{
    public class CarDealerContext : DbContext, ICarDealerContext
    {
        private readonly List<IDomainEvent> _domainEvents;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public CarDealerContext(DbContextOptions options, ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _domainEvents = new List<IDomainEvent>();
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<AvailibleCar> AvailibleCars { get; set; }
        public DbSet<CarState> CarStates { get; set; }
        public DbSet<CarHistoryItem> CarHistoryItems { get; set; }
        public override int SaveChanges()
        {
            base.ChangeTracker.DetectChanges();
            var timestamp = _dateTime.Now;
            foreach (var entry in base.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified)
                && !e.Metadata.IsOwned())) // OwnedTypes are ValueObjects
            {
                CollectDomainEvents(entry);
                FillAuditProperties(timestamp, entry);
            }
            return base.SaveChanges();
        }

        private void FillAuditProperties(DateTime timestamp, EntityEntry entry)
        {
            //shadow properties changes:
            entry.Property("LastModified").CurrentValue = timestamp;
            entry.Property("LastModifiedBy").CurrentValue = _currentUserService.Login;
            if (entry.State == EntityState.Added)
            {
                entry.Property("Created").CurrentValue = timestamp;
                entry.Property("CreatedBy").CurrentValue = _currentUserService.Login;
            }
        }

        private void CollectDomainEvents(EntityEntry entry)
        {
            if (entry.Entity is AggregateRoot aggregateRoot)
            {
                _domainEvents.AddRange(aggregateRoot.DomainEvents);
                aggregateRoot.ClearEvents();
            }
        }

        public void HandleDomainEvents()
        {
            foreach (IDomainEvent domainEvent in _domainEvents)
            {
                DomainEvents.Dispatch(domainEvent);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table-Per-Hierarchy - to deliver dedicated behaviour i OCP way:
            //https://www.learnentityframeworkcore.com/inheritance
            modelBuilder.Entity<AvailibleCar>().ToTable("Cars")
                .HasDiscriminator<CarType>("Type")
                .HasValue<RegularCar>(CarType.Regular)
                .HasValue<SportCar>(CarType.Sport);

            //ownedTypes configuration:
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.BasePrice);
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.CurrentMileage);
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.Name);
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.Engine, d =>
            {
                d.OwnsOne(p => p.BatteryCapacity);
                d.OwnsOne(p => p.EngineCapacity);
                d.OwnsOne(p => p.EuroStandard);
                d.Property(e => e.Type).InterchangeableWithString();
            }
            );

            modelBuilder.Entity<CarHistoryItem>().OwnsOne(x => x.Mileage);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()))
            {
                //if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    //shadow properties for all entities in CarDealerContext:
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                    modelBuilder.Entity(entityType.Name).Property<string>("CreatedBy");
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
                    modelBuilder.Entity(entityType.Name).Property<string>("LastModifiedBy");
                }
            }


            //required dict/status structure (in separate table)
            modelBuilder.Entity<AvailibleCar>().HasOne(x => x.State).WithMany(x => x.Cars).IsRequired();

            //required dict/status structure as enum (in same table)
            modelBuilder.Entity<AvailibleCar>().Property(e => e.Type).InterchangeableWithString();
            modelBuilder.Entity<AvailibleCar>().Property(e => e.Transmission).InterchangeableWithString();

            base.OnModelCreating(modelBuilder);
        }

    }
    internal static class BuilderExtensions
    {
        public static PropertyBuilder InterchangeableWithString<TProperty>(this PropertyBuilder<TProperty> builder) where TProperty : struct
        {
            return builder.HasConversion(
                    @enum => @enum.ToString(),
                    @string => Enum.Parse<TProperty>(@string)
                 );
        }
    }
}
