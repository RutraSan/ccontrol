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
        static Dictionary<string, double> previousPosition;
        public static void InitControls()
        {
            previousPosition = new Dictionary<string, double>();
            previousPosition["X"] = 0.0;
            previousPosition["Y"] = 0.0;
        }
        public static void HandleControl(in Hand hand, in Preferences preferences)
        {
            // get screen width
            var screen = Screen.PrimaryScreen;
            var bounds = screen.Bounds;

            // Update cursor position
            if (hand.detected)
            {
                var finger = hand.GetFinger(preferences.mouse);
                var position = new Point((int)(bounds.Width * finger["X"]), (int)(bounds.Height * finger["Y"]));

                // if the change is little, then for more stability the position doesn't update
                if ((int)(finger["X"] * 1000) != (int)(previousPosition["X"] * 1000)  && (int)(finger["Y"] * 1000) != (int)(previousPosition["Y"] * 1000))
                {
                    Cursor.Position = position;
                }
                previousPosition = new Dictionary<string, double>(finger);
            }
        }
    }
}
