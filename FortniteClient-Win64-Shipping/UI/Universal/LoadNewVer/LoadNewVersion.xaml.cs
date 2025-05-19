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

namespace FortniteClient_Win64_Shipping.UI.Universal.LoadNewVer
{
    /// <summary>
    /// Interaction logic for LoadNewVersion.xaml
    /// </summary>
    public partial class LoadNewVersion : Page
    {
        public LoadNewVersion()
        {
            InitializeComponent();
            descText2.Text = $"Currently loading {Engine.SelectedVersion}";
            ClearOld();
        }

        private async void ClearOld()
        {
            try
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.ClearAllFrames();
                }

                await NavAfterLoading();
            }
            catch(Exception ex) 
            {
                Logger.Log($"Exception occured when trying to launch a new version. | {ex}");
                EasyNavigation.OpenNotice("Failed to open the new version.", $"We ran into an error when trying to open {Engine.SelectedVersion}. Try and restart or contact support.");
            }
        }

        private async Task NavAfterLoading()
        {
            await Task.Delay(2000);

            try
            {
                if (Engine.SelectedVersion == "ot10")
                {
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        mainWindow.LaunchOT10();
                        mainWindow.KillSettings();
                        await AnimationEngine.FadeOut(Grid, 0.20);
                        await AudioPlayer.FadeOutMusic(2.0f);
                    }
                }
                else if (Engine.SelectedVersion == "ot11")
                {
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        mainWindow.LaunchOT11();
                        mainWindow.KillSettings();
                        await AnimationEngine.FadeOut(Grid, 0.20);
                        await AudioPlayer.FadeOutMusic(2.0f);
                    }
                    //EasyNavigation.OpenNotice("You selected a version that is not available yet!", $"{Engine.SelectedVersion} is not currently available. Please pick a different version.");
                }
                else if (Engine.SelectedVersion == "ot6")
                {
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        mainWindow.LaunchOT6();
                        mainWindow.KillSettings();
                        await AnimationEngine.FadeOut(Grid, 0.20);
                        await AudioPlayer.FadeOutMusic(2.0f);
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Log($"Exception occured when trying to launch a new version. | {ex}");
                EasyNavigation.OpenNotice("Failed to open the new version.", $"We ran into an error when trying to open {Engine.SelectedVersion}. Try and restart or contact support.");
            }
        }
    }
}
