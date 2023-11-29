using bookfy.domain.Apartaments;

namespace bookfy.infrastructure.Repositories
{
    internal sealed class ApartmentRepository : Repository<Apartament>, IApartamentRepository
    {
        public ApartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
