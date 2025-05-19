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

namespace FortniteClient_Win64_Shipping.ChooseVersion
{
    /// <summary>
    /// Interaction logic for ChooseVersion.xaml
    /// </summary>
    public partial class ChooseVersion : Page
    {
        public ChooseVersion()
        {
            InitializeComponent();
            AudioPlayer.PlayMusic("pack://application:,,,/Content/Sounds/Fort_Music/Menu/store_tem_music_a.ogg", volume: 0.4f, loop: true);
        }

        private async void LaunchOT6(object sender, RoutedEventArgs e)
        {
            Engine.SelectedVersion = "ot6";
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);
            await Task.Delay(500);
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Settings.Content = null;
                mainWindow.OpenLoading();
            }
        }

        private async void LaunchOT10(object sender, RoutedEventArgs e)
        {
            Engine.SelectedVersion = "ot10";
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);

            await Task.Delay(500);
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Settings.Content = null;
                mainWindow.OpenLoading();
            }
        }

        private async void LaunchOT11(object sender, RoutedEventArgs e)
        {
            Engine.SelectedVersion = "ot11";
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);

            await Task.Delay(500);
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Settings.Content = null;
                mainWindow.OpenLoading();
            }
        }
    }
}
