using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
        /// <summary>
        /// Used to make a little stabilization. 
        /// </summary>
        static Dictionary<string, double> previousPosition;

        /// <summary>
        /// Screen bounds.
        /// </summary>
        static Rectangle bounds;

        /// <summary>
        /// Count amount of the time finger is up. Used to not respond on an error.
        /// </summary>
        static int fingerCount;

        /// <summary>
        /// Flag signing if a finger is up or not.
        /// </summary>
        static bool fingerUp;

        static INPUT[] input;

        /// <summary>
        /// Should be called one time in order to initialize the static members and make sure <c>handleControl()</c> works properly.
        /// </summary>
        public static void InitControls()
        {
            previousPosition = new Dictionary<string, double>();
            previousPosition["X"] = 0.0;
            previousPosition["Y"] = 0.0;

            bounds = Screen.PrimaryScreen.Bounds;

            fingerCount = 0;
            fingerUp = false;

            input = new INPUT[1];

            input[0].type = 0;
            input[0].U.mi.time = 0;
            input[0].U.mi.dwFlags = MOUSEEVENTF.LEFTDOWN | MOUSEEVENTF.ABSOLUTE;
            input[0].U.mi.dwExtraInfo = UIntPtr.Zero;
            input[0].U.mi.dx = 1;
            input[0].U.mi.dy = 1;
        }

        /// <summary>
        /// Handles the control of the mouse. use <c>InitControls()</c> to init the static members before using this function.
        /// </summary>
        /// <param name="hand">Current hand object reference</param>
        /// <param name="preferences">Preferences reference</param>
        public static void HandleControl(in Hand hand, in Preferences preferences)
        {
            Dictionary<string, Dictionary<string, double>> finger;
            Point position;

            // stop execution if there is no hand.
            if (hand.detected == false)
            {
                return;
            }

            // UPDATE CURSOR POSITION
            finger = hand.GetFinger(preferences.mouse);
            var fingertip = finger["tip"];
            position = new Point((int)(bounds.Width * fingertip["X"]), (int)(bounds.Height * fingertip["Y"]));

            // if the change is little, then for more stability the position doesn't update
            if ((int)(fingertip["X"] * 1000) != (int)(previousPosition["X"] * 1000)  && (int)(fingertip["Y"] * 1000) != (int)(previousPosition["Y"] * 1000))
            {
                SetCursorPos(position.X, position.Y);
            }
            // update previous position
            previousPosition = new Dictionary<string, double>(fingertip);

            // CHECK FOR MOUSE LEFT BUTTON CLICK
            fingerUp = hand.IsFingerUp(preferences.leftClick);
            fingerCount = fingerUp ? fingerCount + 1 : 0;
            if (fingerCount == 5)
            {
                input[0].U.mi.dwFlags = MOUSEEVENTF.LEFTDOWN | MOUSEEVENTF.ABSOLUTE | MOUSEEVENTF.LEFTUP;
                SendInput(1, input, INPUT.Size);
            }
            
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            internal uint type;
            internal InputUnion U;
            internal static int Size
            {
                get { return Marshal.SizeOf(typeof(INPUT)); }
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            internal int dx;
            internal int dy;
            internal int mouseData;
            internal MOUSEEVENTF dwFlags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }

        [Flags]
        internal enum MOUSEEVENTF : uint
        {
            ABSOLUTE = 0x8000,
            HWHEEL = 0x01000,
            MOVE = 0x0001,
            MOVE_NOCOALESCE = 0x2000,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            VIRTUALDESK = 0x4000,
            WHEEL = 0x0800,
            XDOWN = 0x0080,
            XUP = 0x0100
        }
    }
}
