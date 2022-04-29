using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace Hamsa
{
    /// <summary>
    /// This enum helps to work with different fingers and know when which finger to use.
    /// </summary>
    enum Fingers
    {
        Thumb,
        Index,
        Middle,
        Ring,
        Pinky
    }
    class Preferences
    {
        private FileInfo configFile;

        /// <summary>
        /// The finger for controling the mouse cursor position.
        /// </summary>
        public Fingers mouse { get; set; }

        /// <summary>
        /// The finger indicating left mouse button click.
        /// </summary>
        public Fingers leftClick { get; set; }

        /// <summary>
        /// Show camera output with the detected hand.
        /// </summary>
        public bool showCamera { get; set; }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public Preferences()
        {
            configFile = new FileInfo("config.ini");

            // if file exists read it
            if (configFile.Exists)
            {
                mouse = GetFinger("mouse");
                leftClick = GetFinger("leftClick");
            }
            // file doesn't exist, common settings
            else
            {
                mouse = Fingers.Index;
                leftClick = Fingers.Pinky;
                configFile.Create();
            }
            showCamera = true;

        }

        /// <summary>
        /// Saves all the settings.
        /// </summary>
        public void Save()
        {
            WritePrivateProfileString("data", "mouse", Enum.GetName(typeof(Fingers), mouse), configFile.FullName);
            WritePrivateProfileString("data", "leftClick", Enum.GetName(typeof(Fingers), leftClick), configFile.FullName);
        }

        /// <summary>
        /// Read the value of the specifed key in data section and returns the Fingers member.
        /// </summary>
        /// <param name="key">The field to get.</param>
        /// <returns>Fingers enum value</returns>
        private Fingers GetFinger(string key)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString("data", key, "", RetVal, 255, configFile.FullName);
            switch (RetVal.ToString())
            {
                case "Thumb":
                    return Fingers.Thumb;
                case "Index":
                    return Fingers.Index;
                case "Middle":
                    return Fingers.Middle;
                case "Ring":
                    return Fingers.Ring;
                case "Pinky":
                    return Fingers.Pinky;
                default:
                    return Fingers.Index;
            }
        }
    }
}
