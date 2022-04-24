using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hamsa
{
    /// <summary>
    /// Class that gives the functionality to control the mouse.
    /// </summary>
    class MouseControl
    {
        public static void HandleControl(in Hand hand, in Preferences preferences)
        {
            // get screen width
            var screen = Screen.PrimaryScreen;
            var bounds = screen.Bounds;

            // Update cursor position
            if(hand.detected)
            {
                var finger = hand.GetFinger(preferences.mouse);
                Cursor.Position = new Point((int)(bounds.Width * finger["X"]), (int)(bounds.Height * finger["Y"]));
            }
        }
    }
}
