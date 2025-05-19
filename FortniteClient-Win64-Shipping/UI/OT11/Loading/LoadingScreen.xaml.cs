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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FortniteClient_Win64_Shipping.Classes;

namespace FortniteClient_Win64_Shipping.UI.OT11.Loading
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Page
    {
        public LoadingScreen()
        {
            InitializeComponent();
            Logger.Log("Loading Online Test 11");
            StartLoadingAnim();
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                AnimationEngine.FadeIn(mainWindow.FG, 0.01);
            }
            Load();
        }

        private async void Load()
        {
            await Task.Delay(2000);
            AnimationEngine.FadeOut(Loading, 0.2);
            await Task.Delay(2000);
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                Logger.Log("Triggering navigation to Login. (OT11)");
                mainWindow.TriggerLogin();
            }
        }

        private void StartLoadingAnim()
        {
            var rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(0.8)),
                RepeatBehavior = RepeatBehavior.Forever
            };
            LoadingDotsRotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

    }
}
