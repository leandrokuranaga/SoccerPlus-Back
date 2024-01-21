using SoccerPlus.Domain.PlayerAggregate;
using SoccerPlus.Infra.Data.Repository;

namespace SoccerPlus.Infra.Data.Repositories
{
    public class PlayerRepository : BaseRepository<Context, PlayerDomain>, IPlayerRepository
    {
        private readonly Context _context;
        public PlayerRepository(Context context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
    }
}

