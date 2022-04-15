namespace TodoBack.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _dbContext;

        public UnitOfWork(TodoDbContext todoDbContext)
        {
            _dbContext = todoDbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
