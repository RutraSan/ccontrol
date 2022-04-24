using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsa
{
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

        /// <summary>
        /// The finger for controling the mouse cursor position
        /// </summary>
        public Fingers mouse { get; set; }

        /// <summary>
        /// Show camera output with the detected hand.
        /// </summary>
        public bool showCamera { get; set; }
        public Preferences()
        {
            mouse = Fingers.Index;
            showCamera = true;
        }
    }
}
