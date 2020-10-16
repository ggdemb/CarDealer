using CarDealer.Application.CommonContracts;
using CarDealer.Domain.Common;
using CarDealer.Domain.Sale.Car;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            var aggregateRoot = entry.Entity as AggregateRoot;
            if (aggregateRoot is object)
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
            //ownedTypes configuration:
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.BasePrice);
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.CurrentMileage);
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.Name);
            modelBuilder.Entity<AvailibleCar>().OwnsOne(x => x.Engine, d =>
            {
                d.OwnsOne(p => p.BatteryCapacity);
                d.OwnsOne(p => p.EngineCapacity);
                d.OwnsOne(p => p.EuroStandard);
            }
            );

            modelBuilder.Entity<CarHistoryItem>().OwnsOne(x => x.Mileage);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()))
            {
                //shadow properties for all entities in CarDealerContext:
                modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                modelBuilder.Entity(entityType.Name).Property<string>("CreatedBy");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
                modelBuilder.Entity(entityType.Name).Property<string>("LastModifiedBy");
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
