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

namespace FortniteClient_Win64_Shipping.UI.Universal
{
    /// <summary>
    /// Interaction logic for SettingsBox.xaml
    /// </summary>
    public partial class SettingsBox : Page
    {
        public SettingsBox()
        {
            InitializeComponent();
        }

        private void CloseFortnite(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.CloseFort();
                mainWindow.Overlay.Content = null;
            }
        }

        private void OpenOptions(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/fort_ui_open_stats.ogg", 0.4f);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                //mainWindow.OpenOptions();
                mainWindow.Settings.Navigate(new UI.Universal.Settings.Options());
                mainWindow.Overlay.Content = null;
            }
        }

        private void ChooseVersion(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/fort_ui_open_stats.ogg", 0.4f);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Overlay.Content = null;
                mainWindow.Settings.IsHitTestVisible = true;
                mainWindow.OpenSelect();
            }
        }
    }
}
