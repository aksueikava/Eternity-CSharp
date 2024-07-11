namespace Eternity.DiscordIntegration
{
    public static class Config
    {
        public static string ClientId => "1253321839204241489";
        public static string LargeImageKey => "myprogram-roundedimage";
        public static string LargeImageText => "LargeImageText";
        public static string ButtonLabel => "Telegram";
        public static string ButtonUrl => "https://t.me/aksueikava";
        public static int UpdateIntervalMs => 3000; // (3 секунды)

        public static string[] DetailsTexts =>
        [
            "Фраза 1",
            "Фраза 2",
            "Фраза 3"
        ];

        public static string GetRichPresenceState(string currentPage)
        {
            return $"Просматривает {PageNameInterpreter.GetPageDisplayName(currentPage)}";
        }
    }
}