namespace CarDealer.Application.ExternalContracts
{
    public interface IUnitOfWork
    {
        public void Commit();
        public void Rollback();
    }
}
