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

        public Fingers mouse { get; set; }

        public Preferences()
        {
            mouse = Fingers.Index;
        }
    }
}
