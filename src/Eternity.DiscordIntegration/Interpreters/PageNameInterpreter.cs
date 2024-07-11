namespace Eternity.DiscordIntegration.Interpreters
{
    public static class PageNameInterpreter
    {
        private static readonly Dictionary<string, string> PageDisplayNames = new()
        {
            { "MainPage", "Главная страница" },
            { "TestPage", "Тестовая страница" }
        };

        public static string GetPageDisplayName(string pageIdentifier)
        {
            if (PageDisplayNames.TryGetValue(pageIdentifier, out string displayName))
            {
                return displayName;
            }
            else
            {
                throw new ArgumentException($"Не удалось найти читаемое имя для страницы: {pageIdentifier}");
            }
        }
    }
}