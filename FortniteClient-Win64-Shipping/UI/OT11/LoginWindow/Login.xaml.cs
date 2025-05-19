using System.Windows.Controls;
using System.Timers;
using System.Threading.Tasks;
using FortniteClient_Win64_Shipping.Classes.HTTPEngine;
using System.Windows;
using System.Security.Cryptography.Xml;
using FortniteClient_Win64_Shipping.Classes;

namespace FortniteClient_Win64_Shipping.UI.OT11.LoginWindow
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
                Logger.Log($"Cannot fetch access token from Authenticate function. | {ex}");
                ErrorHandling.ThrowError();
            }
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
                Logger.Log($"Login failed | {ex}");
                ErrorHandling.ThrowError();

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ErrorText.Opacity = 1;
                    LoginUI.Visibility = Visibility.Visible;
                    LoggingIn.Visibility = Visibility.Collapsed;
                }));
            }
        }

        private void ShowLogin(object sender, RoutedEventArgs e)
        {
            PressToStart.Opacity = 0;
            LoginBorder.IsHitTestVisible = true;
            LoginBorder.Opacity = 0;
        }
    }
}
