namespace CarDealer.Application.CommonContracts
{
    public interface IUnitOfWork
    {
        public void Commit();
        public void Rollback();
    }
}
