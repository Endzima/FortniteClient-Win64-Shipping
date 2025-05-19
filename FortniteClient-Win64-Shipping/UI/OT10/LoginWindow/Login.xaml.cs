using System.Windows.Controls;
using System.Timers;
using System.Threading.Tasks;
using FortniteClient_Win64_Shipping.Classes.HTTPEngine;
using System.Windows;
using System.Security.Cryptography.Xml;
using FortniteClient_Win64_Shipping.Classes;

namespace FortniteClient_Win64_Shipping.UI.OT10.LoginWindow
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        System.Timers.Timer timer = new System.Timers.Timer(30000);
        Requests request = new Requests();

        public Login()
        {
            InitializeComponent();
            AudioPlayer.PlayMusic("pack://application:,,,/Content/Sounds/Fort_Music/Menu/fortnite_login_screen.ogg", volume: 0.4f, loop: true);
            Logger.Log("Login page initialized. (OT10)");
            PlayChimesCountdown();
            GetBuildInfo();
            GetAccessToken();
        }

        private void EmailChanged(object sender, RoutedEventArgs e)
        {
            if (Email.Text.Contains("@") && Email.Text.Contains(".") && Password.Password.Length > 0)
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Length > 0 && Email.Text.Contains("."))
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private async void GetBuildInfo()
        {
            try
            {
                Logger.Log("Attempting a GET request to fortnite/api/version. (OT10)");
                RequestSaving.BuildInfo info = await request.GetJSON<RequestSaving.BuildInfo>("fortnite/api/version");

                BuildNumber.Text = $"BUILD {info.build}";
                GameName.Text = info.app;
                CLNumber.Text = $"CL# {info.cln} ({info.branch})";
                BuildDate.Text = info.buildDate.ToString("yyyy-MM-dd");
                VersionNumber.Text = $"VERSION {info.version}";
                ServerDate.Text = info.serverDate.ToString("yyyy-MM-dd");
            }
            catch(Exception ex)
            {
                Logger.Log($"GET request failed! (OT10) | ${ex.ToString()}");
                BuildInfo.Visibility = System.Windows.Visibility.Collapsed;
                EasyNavigation.OpenNotice("Update Check Failed", "There was a problem determining if you need an update. You may or may not need an update. Error: 6");
            }
        }

        private async void GetAccessToken()
        {
            Logger.Log("Attempting a POST request to /account/api/oauth/token (OT10)");
            try
            {
                var body = new Dictionary<string, string>
                    {
                        { "grant_type", "client_credentials" }
                    };

                var response = await request.Authenticate<RequestSaving.GetAccessToken>("account/api/oauth/token", body);
            }

            catch (Exception ex)
            {
                Logger.Log($"Cannot fetch access token from Authenticate function. | {ex}");
                ErrorHandling.ThrowError();
            }
        }

        private async Task PlayChimesCountdown()
        {
            timer.Elapsed += (s, e) =>
            {
                PlayChimes();
            };

            timer.AutoReset = true;
            timer.Start();
        }

        private void PlayChimes()
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/AmbientLooping/Windchimes_Gentle02.ogg", 0.2f);
        }

        private void LoginPressed(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);
        }

        private async void LoginBtn(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/fort_ui_new_chris_13.ogg", 0.4f);
            await AttemptAuthentication();
        }

        private async Task AttemptAuthentication()
        {
            Logger.Log("Attempting authentication using email and password. (OT10)");
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/fort_new_frontend_ui_click_13.ogg", 0.4f);
            ErrorText.Opacity = 0;
            LoginUI.Visibility = Visibility.Collapsed;
            LoggingIn.Visibility = Visibility.Visible;
            try
            {
                var body = new
                {
                    grant_type = "password",
                    username = Email.Text,
                    password = Password.Password,
                    includePerms = true,
                };
                var response = await request.PostJSON<RequestSaving.GetCreds>("account/api/oauth/token", body);
            }
            catch (Exception ex)
            {
                Logger.Log($"Login failed! Falling back to original menu. (OT10) | {ex}");
                ErrorHandling.ThrowError();

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ErrorText.Opacity = 1;
                    LoginUI.Visibility = Visibility.Visible;
                    LoggingIn.Visibility = Visibility.Collapsed;
                }));
            }
        }
    }
}
