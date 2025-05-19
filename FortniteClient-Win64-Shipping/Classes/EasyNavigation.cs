using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FortniteClient_Win64_Shipping.Classes
{
    internal class EasyNavigation
    {
        public static void OpenNotice(string header, string description, string button = "Ok")
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.OpenNotice(header, description, button);
            }
        }
    }
}
