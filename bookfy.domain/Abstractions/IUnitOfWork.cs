namespace bookfy.domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
