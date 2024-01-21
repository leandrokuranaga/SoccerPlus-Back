using Abp.Domain.Entities;
using SoccerPlus.Domain.PlayerAggregate;

namespace SoccerPlus.Domain.SoccerMatchAggregate
{
    public class SoccerMatchDomain : Entity
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? UF { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime MatchDay { get; set; }
        public virtual ICollection<PlayerDomain> Players { get; set; }
      
    }
}
