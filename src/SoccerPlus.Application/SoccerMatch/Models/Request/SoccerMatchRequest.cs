namespace SoccerPlus.Application.SoccerMatch.Models.Request
{
    public class SoccerMatchRequest
    {
        public double Latitude { get; set; } = -23.1149542;
        public double Longitude { get; set; } = -47.2244995;
        public string Position { get; set; } = "MidField";
        public string Name { get; set; } = "Leandro Kuranaga";
        public int PlayerId { get; set; } = 10;
        public DateTime DatePlay { get; set; } = DateTime.Now;

    }
}
