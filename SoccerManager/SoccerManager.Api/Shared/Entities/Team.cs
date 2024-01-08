using SoccerManager.Api.Shared.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerManager.Api.Shared.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

    }
}
