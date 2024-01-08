namespace SoccerManager.Api.Shared.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public long Value { get; set; }
        public  Team TeamFrom { get; set; }
        public Team TeamTo { get; set; }
        public bool IsSold { get; set; }
    }
}
