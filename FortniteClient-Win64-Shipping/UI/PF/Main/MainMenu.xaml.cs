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

namespace FortniteClient_Win64_Shipping.UI.PF.Main
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayHovered(object sender, RoutedEventArgs e)
        {
            ButtonAction.Text = "Start your Fortnite adventure!";
        }

        private void OptionsHovered(object sender, RoutedEventArgs e)
        {
            ButtonAction.Text = "Configure Fortnite options.";
        }

        private void QuitHovered(object sender, RoutedEventArgs e)
        {
            ButtonAction.Text = "Quit the game.";
        }

        private void OptionsClicked(object sender, RoutedEventArgs e)
        {
            MainMenuBtns.Opacity = 0;
            ButtonAction.Opacity = 0;
            Options.Opacity = 1;
        }

        private void OptionsBackPressed(object sender, RoutedEventArgs e)
        {
            MainMenuBtns.Opacity = 1;
            ButtonAction.Opacity = 1;
            Options.Opacity = 0;
        }
    }
}
