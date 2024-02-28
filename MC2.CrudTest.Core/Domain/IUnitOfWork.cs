using MC2.CrudTest.Core.Domain.Repository.Abstraction;

namespace MC2.CrudTest.Core.Domain;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : class;
    void CreateTransaction();
    Task CreateTransactionAsync();
    void Commit();
    Task CommitAsync();
    void Rollback();
    Task RollbackAsync();
    int Save();
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}