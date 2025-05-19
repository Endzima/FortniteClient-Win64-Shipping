using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FortniteClient_Win64_Shipping.Classes;

namespace FortniteClient_Win64_Shipping.UI.Universal.Settings
{
    /// <summary>
    /// Interaction logic for CloseFortnite.xaml
    /// </summary>
    public partial class CloseFortnite : Page
    {
        public CloseFortnite()
        {
            InitializeComponent();
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                AnimationEngine.FadeIn(Grid, 0.20);
                mainWindow.ActivateSettings();
            }
        }

        private async void Cancel(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                await AnimationEngine.FadeOut(Grid, 0.20);
                mainWindow.Settings.Content = null;
                mainWindow.Settings.IsHitTestVisible = false;
            }
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
