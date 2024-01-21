using SoccerPlus.Domain.SoccerMatchAggregate;
using SoccerPlus.Infra.Data.Repository;

namespace SoccerPlus.Infra.Data.Repositories
{
    public class SoccerMatchRepository : BaseRepository<Context ,SoccerMatchDomain>, ISoccerMatchRepository
    {
        private readonly Context _context;
        public SoccerMatchRepository(Context context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
    }
}
