using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteClient_Win64_Shipping.Classes
{
    internal class ErrorHandling
    {
        public static void ThrowError()
        {
            AudioPlayer.PlayTimedLoop("pack://application:,,,/Content/Sounds/UI/fort_new_frontend_ui_click_15.ogg", 0.4f);
        }

        public static void ThrowException()
        {

        }
    }
}
