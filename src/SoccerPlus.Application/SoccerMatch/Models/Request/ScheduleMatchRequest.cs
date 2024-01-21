namespace SoccerPlus.Application.SoccerMatch.Models.Request
{
    public class ScheduleMatchRequest
    {
        public double Distance { get; set; }
        public string Position { get; set; }
        public string WeekDays { get; set; }
        public string Time { get; set; }
        public string GenderToPlay { get; set; }
        public double Price { get; set; }
        public int Age { get; set; }
    }
}
