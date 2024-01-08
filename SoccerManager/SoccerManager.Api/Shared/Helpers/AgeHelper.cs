namespace SoccerManager.Api.Shared.Helpers
{
    public static class AgeHelper
    {
        public static int RandomAge()
        {
            Random random = new Random();
            return random.Next(18, 41);
        }
    }
}
