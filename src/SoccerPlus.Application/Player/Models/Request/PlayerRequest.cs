namespace SoccerPlus.Application.Player.Models.Request;
public class PlayerRequest
{
    public int Id { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public int IdField { get; set; }
}

