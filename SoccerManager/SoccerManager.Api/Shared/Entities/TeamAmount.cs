namespace SoccerManager.Api.Shared.Entities
{
    public class TeamAmount
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public long Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
