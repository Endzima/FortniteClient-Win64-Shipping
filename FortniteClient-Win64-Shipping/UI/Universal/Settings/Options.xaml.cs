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
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Page
    {
        public Options()
        {
            InitializeComponent();
            if (Application.Current.MainWindow is MainWindow mainWindow)
            { 
                AnimationEngine.FadeIn(this, 0.1);
                mainWindow.ActivateSettings();
            }
        }

        private async void CloseOptions(object sender, RoutedEventArgs e)
        {
            await AnimationEngine.FadeOut(this, 0.1);
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Settings.Content = null;
                mainWindow.Overlay.Content = null;
                mainWindow.Settings.IsHitTestVisible = true;
            }
        }

        private void ApplyOptions(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);
        }

        private void ResetOptions(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);
        }
    }
}
