using bookfy.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace bookfy.infrastructure.Repositories
{
    public abstract class Repository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected Repository(ApplicationDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(Guid guid, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(user => user.Id == guid, cancellationToken);
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }
    }
}
