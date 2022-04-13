using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//EMGU
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

//DiresctShow
//using DirectShowLib;

namespace Hamsa
{
    public partial class Main : Form
    {

        #region Variables
        #region Camera Capture Variables
        private VideoCapture _capture = null; //Camera
        private bool _captureInProgress = false; //Variable to track camera state
        int CameraDevice = 0; //Variable to track camera device selected
        //Video_Device[] WebCams; //List containing all the camera available
        #endregion
        #region Camera Settings
        int Brightness_Store = 0;
        int Contrast_Store = 0;
        int Sharpness_Store = 0;
        #endregion
        #endregion

        public Main()
        {
            InitializeComponent();
            _capture = new VideoCapture();
            _capture.ImageGrabbed += ProcessFrame;
            _capture.FlipHorizontal = true;
            _capture.Start();

        }

        /// <summary>
        /// Handle of the <c>ImageGrabbed</c> Event.
        /// responsible for the getting the image from <c>_capture</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ProcessFrame(object sender, EventArgs args)
        {
            Mat image = new Mat();
            if( _capture.Retrieve(image))
            {
                CameraBox.Image = (image.ToImage<Bgr, Byte>()).ToBitmap();
            }
        }


        

    }
}
