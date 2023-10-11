namespace bookfy.domain.Apartaments
{
    public interface IApartamentRepository
    {
        Task<Apartament?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
