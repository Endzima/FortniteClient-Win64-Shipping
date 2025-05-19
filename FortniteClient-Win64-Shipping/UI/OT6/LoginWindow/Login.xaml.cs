using System.Windows.Controls;
using System.Timers;
using System.Threading.Tasks;
using FortniteClient_Win64_Shipping.Classes.HTTPEngine;
using System.Windows;
using System.Security.Cryptography.Xml;
using FortniteClient_Win64_Shipping.Classes;

namespace FortniteClient_Win64_Shipping.UI.OT6.LoginWindow
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
            Logger.Log("Login page initialized. (OT6)");
            AudioPlayer.PlayMusic("pack://application:,,,/Content/Sounds/Fort_Music/Menu/fortnite_login_screen.ogg", volume: 0.4f, loop: true);
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
            Logger.Log("Attempting to get build info from /fortnite/api/version (OT6)");
            try
            {
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
                Logger.Log($"GET request failed! (OT6) | ${ex.ToString()}");
                BuildInfo.Visibility = System.Windows.Visibility.Collapsed; 
            }
        }

        private async void GetAccessToken()
        {
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
                Logger.Log($"Cannot POST /account/api/oauth/token. HTTP error! (OT6) | {ex}");
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
            Logger.Log("Attempting authentication by POST from /account/api/oauth/token! (OT6)");
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
                Logger.Log($"Login failed! (OT6) | {ex}");
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
