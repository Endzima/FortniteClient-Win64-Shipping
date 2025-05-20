using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FortniteClient_Win64_Shipping.Classes;

namespace FortniteClient_Win64_Shipping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenSelect();
            LoadAudioConfiguration();
        }

        private void LoadAudioConfiguration()
        {
            // Load audio prior to ClientSettings.


        }

        #region Navigation

        // Every overlay works on the same Frame, even though there is an overlay Frame, I've decided to just use the Settings one.

        public void OpenSelect()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Settings.IsHitTestVisible = true;
                Settings.Visibility = Visibility.Visible;
                Settings.Navigate(new ChooseVersion.ChooseVersion());
            }));
        }

        public void CloseNotice()
        {
            Settings.IsHitTestVisible = false;
            Settings.Content = null;
        }

        public void OpenNotice(string header, string description, string button)
        {
            Settings.IsHitTestVisible = true;
            Settings.Content = null;
            Settings.Visibility = Visibility.Visible;
            Settings.Navigate(new UI.Universal.GameMessage.Message(header, description, button));
        }

        public async void TriggerLogin()
        {
            if (Engine.SelectedVersion == "OT6")
            {
                Settings.Visibility = Visibility.Visible;
                Login.Navigate(new UI.OT6.LoginWindow.Login());
                await AnimationEngine.FadeOut(FG, 0.5);
                FG.IsHitTestVisible = false;
                FG.Content = null;
            }
            else if (Engine.SelectedVersion == "OT10")
            {
                Settings.Visibility = Visibility.Visible;
                Login.Navigate(new UI.OT10.LoginWindow.Login());
                await AnimationEngine.FadeOut(FG, 0.5);
                FG.IsHitTestVisible = false;
                FG.Content = null;
            }
            else if (Engine.SelectedVersion == "OT11")
            {
                Settings.Visibility = Visibility.Visible;
                Login.Navigate(new UI.OT11.LoginWindow.Login());
                await AnimationEngine.FadeOut(FG, 0.5);
                FG.IsHitTestVisible = false;
                FG.Content = null;
            }
            else if (Engine.SelectedVersion == "OT2")
            {
                Settings.Visibility = Visibility.Visible;
                Login.Navigate(new UI.OT2.MainMenu.MainUI());
                await AnimationEngine.FadeOut(FG, 0.5);
                FG.IsHitTestVisible = false;
                FG.Content = null;
            }

            FG.Opacity = 1;
        }

        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/fort_ui_open_stats.ogg", 0.4f);
            Overlay.Opacity = 1;
            Overlay.Navigate(new UI.Universal.SettingsBox());
        }

        public void CloseFort()
        {
            Settings.Content = null;
            Settings.Navigate(new UI.Universal.Settings.CloseFortnite());
        }

        public void OpenOptions()
        {
            Settings.IsHitTestVisible = true;
            Settings.Content = null;
            Settings.Visibility = Visibility.Visible;
            Settings.Navigate(new UI.Universal.Settings.Options());
        }

        #endregion

        #region LaunchVersion

        public void LaunchOT6()
        {
           FG.Visibility = Visibility.Visible;
           FG.Navigate(new UI.OT6.Loading.LoadingScreen());
        }

        public void LaunchOT10()
        {
            FG.Visibility = Visibility.Visible;
            FG.Navigate(new UI.OT10.Loading.LoadingScreen());
        }

        public void LaunchOT11()
        {
            FG.Visibility = Visibility.Visible;
            FG.Navigate(new UI.OT11.Loading.LoadingScreen());
        }

        public void LaunchPF()
        {
            FG.Visibility = Visibility.Visible;
            FG.Navigate(new UI.PF.Main.MainMenu());
        }
        public void LaunchOT2()
        {
            FG.Visibility = Visibility.Visible;
            FG.Navigate(new UI.OT2.Loading.LoadingScreen());
        }


        public void OpenLoading()
        {
            Settings.IsHitTestVisible = true;
            Settings.Content = null;
            Settings.Visibility = Visibility.Visible;
            Settings.Navigate(new UI.Universal.LoadNewVer.LoadNewVersion());
        }

        #endregion

        #region Helper functions

        public void ClearAllFrames()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //Login.IsHitTestVisible = true;
                Login.Content = null;
                Login.Opacity = 1;
                //FG.IsHitTestVisible = true;
                FG.Content = null;
                FG.Opacity = 1;
                //Overlay.IsHitTestVisible = true;
                Overlay.Content = null;
                Overlay.Opacity = 1;
                //Settings.Content = IsHitTestVisible = true;
            });

        }

        public void KillSettings()
        {
            Settings.IsHitTestVisible = false;
            Settings.Content = null;
        }
        public void KillFG()
        {
            FG.IsHitTestVisible = false;
            FG.Content = null;
        }

        public void KillLogin()
        {
            Login.IsHitTestVisible = false;
            Login.Content = null;
        }

        public void ActivateSettings()
        {
            Settings.IsHitTestVisible = true;
            Settings.Content = null;
        }
        public void ActivateFG()
        {
            FG.IsHitTestVisible = true;
            FG.Content = null;
        }

        public void ActivateLogin()
        {
            Login.IsHitTestVisible = true;
            Login.Content = null;
        }
        #endregion
    }
}