namespace bookfy.domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByidAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(User user);
    }
}
