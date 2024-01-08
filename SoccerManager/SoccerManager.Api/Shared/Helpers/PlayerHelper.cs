using SoccerManager.Api.Shared.Enums;

namespace SoccerManager.Api.Shared.Helpers
{
    public static class PlayerHelper
    {
        public static string RandomFirstNamePlayer()
        {
            Array values = Enum.GetValues(typeof(PlayerFirstNameEnum));
            Random random = new Random();
            var randomName = (PlayerFirstNameEnum)values.GetValue(random.Next(values.Length));
            return randomName.ToString();
        }

        public static string RandomLastNamePlayer()
        {
            Array values = Enum.GetValues(typeof(PlayerLastNameEnum));
            Random random = new Random();
            var randomName = (PlayerLastNameEnum)values.GetValue(random.Next(values.Length));
            return randomName.ToString();
        }

        public static long RandomNewValue(long value) 
        {
            Random random = new Random();
            var percentage =  1 + (random.Next(10, 101)/100);
            return value * percentage;
        }
    }
}
