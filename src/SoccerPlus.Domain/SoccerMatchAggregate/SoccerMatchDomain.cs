using Abp.Domain.Entities;
using SoccerPlus.Domain.PlayerAggregate;

namespace SoccerPlus.Domain.SoccerMatchAggregate
{
    public class SoccerMatchDomain : Entity
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? UF { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime MatchDay { get; set; }
        public virtual ICollection<PlayerDomain> Players { get; set; }
        public SoccerMatchDomain(int id, string? address, string? city, string? uF, double latitude, double longitude, DateTime matchDay)
        {
            Id = id;
            Address = address;
            City = city;
            UF = uF;
            Latitude = latitude;
            Longitude = longitude;
            MatchDay = matchDay;
        }
    }
}
