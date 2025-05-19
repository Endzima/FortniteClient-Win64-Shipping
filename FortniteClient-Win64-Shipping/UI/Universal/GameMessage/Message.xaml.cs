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

namespace FortniteClient_Win64_Shipping.UI.Universal.GameMessage
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : Page
    {
        public Message(string header, string desc, string button)
        {
            InitializeComponent();
            SetUI(header, desc, button);
            AnimationEngine.FadeIn(Grid, 0.20);
            Logger.Log($"GlobalGameMessage: loaded. | {header}, {desc}, {button}");
        }

        private void SetUI(string header, string desc, string button)
        {
            Logger.Log("GlobalGameMessage: Setting content.");
            headerText.Text = header;
            descText.Text = desc;
            buttonText.Text = button;
        }

        private async void Close(object sender, RoutedEventArgs e)
        {
            Logger.Log("GlobalGameMessage: closed.");
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/Fort_Cancel_Button_01.ogg", 0.4f);
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                await AnimationEngine.FadeOut(Grid, 0.20);
                mainWindow.CloseNotice();
            }
        }
    }
}
