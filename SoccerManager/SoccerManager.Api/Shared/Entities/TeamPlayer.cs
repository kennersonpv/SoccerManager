namespace SoccerManager.Api.Shared.Entities
{
    public class TeamPlayer
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public Player Player { get; set; }
        public bool IsPlayer { get; set; }
    }
}
