namespace SoccerManager.Api.Shared.Entities
{
    public class PlayerValue
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public long Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
