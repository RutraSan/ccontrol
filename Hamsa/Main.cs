using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        private VideoCapture Camera = null;
        NamedPipeClientStream PipeClient;
        BinaryReader PipeStream;
        Process HandDetection;

        public Main()
        {
            InitializeComponent();
            // set the _capture device
            Camera = new VideoCapture();
            Camera.ImageGrabbed += ProcessFrame;
            Camera.FlipHorizontal = true;

            RunHandDetection();
            //Thread.Sleep(1000); // make sure the server pipe opens first
            PipeClient = new NamedPipeClientStream(".", "HandDetection", PipeDirection.In);
            PipeClient.Connect();
            PipeStream = new BinaryReader(PipeClient);

            Application.Idle += HandleApplication;

        }

        private void HandleApplication(object sender, EventArgs e)
        {
            try
            {
                while (IsApplicationIdle())
                {
                    float val;
                    byte[] buffer = new byte[4];
                    PipeStream.Read(buffer, 0, 4);
                    val = BitConverter.ToSingle(buffer, 0);
                    Console.WriteLine(val);
                }
            }
            catch
            {

            }
        }

        bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);


        /// <summary>
        /// Runs the hand detection script.
        /// </summary>
        private void RunHandDetection()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:\\python39\\python.exe";
            start.Arguments = "../hand_detection.py";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            HandDetection = Process.Start(start);
        }

        /// <summary>
        /// Handle of the <c>ImageGrabbed</c> Event.
        /// responsible for getting the image from <c>_capture</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ProcessFrame(object sender, EventArgs args)
        {
            //using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "CSServer", PipeDirection.In))
            //{
            //    pipeClient.Connect();
            //}
                // setting the frame
                Mat image = new Mat();
            if( Camera.Retrieve(image))
            {
                CameraBox.Image = (image.ToImage<Bgr, Byte>()).ToBitmap();
            }
        }

        /// <summary>
        /// Event called when the app is closed. Frees resources and closes other processes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            PipeClient.Close();
            if (!HandDetection.HasExited) {
                HandDetection.Kill();
            }
        }
    }
}
