using SoccerManager.Api.Shared.Enums;

namespace SoccerManager.Api.Shared.Helpers
{
    public static class CountryHelper
    {
        public static string RandomCountry()
        {
            Array values = Enum.GetValues(typeof(CountryEnum));
            Random random = new Random();
            var randomCountry = (CountryEnum)values.GetValue(random.Next(values.Length));
            return randomCountry.ToString();
        }
    }
}
