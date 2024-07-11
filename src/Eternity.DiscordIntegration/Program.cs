using System.Timers;

namespace Eternity.DiscordIntegration
{
    public class Init
    {
        private static readonly Lazy<Init> _instance = new Lazy<Init>(() => new Init());
        private readonly object _lock = new object();
        private DiscordRpcClient _client;
        private RichPresence _richPresence;
        private System.Timers.Timer _updateTimer;
        private int _detailsIndex = 0;
        private string _currentPage = "MainPage";

        private Init()
        {
            _client = new DiscordRpcClient(Config.ClientId)
            {
                Logger = new ConsoleLogger() { Level = LogLevel.Warning }
            };

            _client.Initialize();

            _richPresence = new RichPresence()
            {
                State = $"Находится на {_currentPage}",
                Timestamps = new Timestamps()
                {
                    Start = DateTime.UtcNow
                },
                Assets = new Assets()
                {
                    LargeImageKey = Config.LargeImageKey,
                    LargeImageText = Config.LargeImageText
                },
                Buttons = new[]
                {
                    new Button() { Label = Config.ButtonLabel, Url = Config.ButtonUrl }
                }
            };

            UpdatePresence();

            _updateTimer = new System.Timers.Timer(Config.UpdateIntervalMs);
            _updateTimer.Elapsed += UpdateDetails;
            _updateTimer.Start();
        }

        public static Init Instance => _instance.Value;

        public void Initialize()
        {
            // Метод инициализации уже не требуется здесь, так как он выполнен в конструкторе
        }

        public void SetCurrentPage(string pageName)
        {
            _currentPage = pageName;
            UpdatePresence();
        }

        private void UpdatePresence()
        {
            if (_richPresence != null)
            {
                _richPresence.State = Config.GetRichPresenceState(_currentPage);
                _richPresence.Details = Config.DetailsTexts[_detailsIndex];
                _client.SetPresence(_richPresence);
            }
        }

        private void UpdateDetails(object sender, ElapsedEventArgs e)
        {
            _detailsIndex = (_detailsIndex + 1) % Config.DetailsTexts.Length;
            UpdatePresence();
        }

        public void Shutdown()
        {
            _updateTimer?.Stop();
            _updateTimer?.Dispose();
            _client.Dispose();
        }
    }
}