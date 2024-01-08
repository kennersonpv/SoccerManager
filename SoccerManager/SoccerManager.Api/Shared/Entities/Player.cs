using SoccerManager.Api.Shared.Enums;

namespace SoccerManager.Api.Shared.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
    }
}
