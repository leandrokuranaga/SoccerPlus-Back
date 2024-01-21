using Abp.Domain.Entities;
using SoccerPlus.Domain.SoccerMatchAggregate;

namespace SoccerPlus.Domain.PlayerAggregate
{
    public class PlayerDomain : Entity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Position { get; set; }
        public string? FullName { get; set; }
        public string? CPF { get; set; }
        public DateOnly BirthdayDate { get; set; }
        public int IdMatch { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public virtual SoccerMatchDomain SoccerMatch { get; set; }
        public PlayerDomain(double latitude, double longitude, string? position, string? fullName, string? cPF, DateOnly birthdayDate, int idMatch, string? email, string? password)
        {
            Latitude = latitude;
            Longitude = longitude;
            Position = position;
            FullName = fullName;
            CPF = cPF;
            BirthdayDate = birthdayDate;
            IdMatch = idMatch;
            Email = email;
            Password = password;
        }
    }
}
