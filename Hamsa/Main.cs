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

        NamedPipeClientStream pipeClient;
        BinaryReader pipeStream;
        Process HandDetection;

        Preferences preferences;

        Screen screen;
        Rectangle bounds;

        public Main()
        {
            InitializeComponent();

            RunHandDetection();
            //Thread.Sleep(1000); // make sure the server pipe opens first
            pipeClient = new NamedPipeClientStream(".", "HandDetection", PipeDirection.In);
            pipeClient.Connect();
            pipeStream = new BinaryReader(pipeClient);

            Application.Idle += HandleApplication;
            Cursor.Position = new System.Drawing.Point(0, 0);

            // For each screen, add the screen to a list.
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                comDeviceList.Items.Add(screen.DeviceName);

            }
            screen = Screen.PrimaryScreen;
            bounds = screen.Bounds;
            Console.WriteLine(bounds);
            chkShowCamera.Checked = true;

            preferences = new Preferences();
        }

        /// <summary>
        /// This function is responsible for the main loop of the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleApplication(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                float x;
                float y;
                byte[] buffer = new byte[200];

                //pipeStream.Read(buffer, 0, 8);
                pipeClient.Read(buffer, 0, buffer.Length);
                x = 1-BitConverter.ToSingle(buffer, 0);
                y = BitConverter.ToSingle(buffer, 4);
                Hand hand = new Hand(buffer);
                //Console.WriteLine("Y:"+ y);
                //Console.WriteLine("X:"+ x);
                //Console.WriteLine(Cursor.Position);
                //Console.WriteLine(Screen.Bounds);
                if (chkShowCamera.Checked)
                {
                    try
                    {
                        using (FileStream ImgFile = new FileStream(@"C:\temp\hamsaimg.jpeg", FileMode.Open, FileAccess.Read))
                        {
                            CameraBox.Image = new Bitmap(ImgFile);
                        }
                    }
                    catch { }
                }
                if (hand.detected)
                {
                    Cursor.Position = new Point((int)(bounds.Width * hand.handKeyPoints[8]["X"]), (int)(bounds.Height * hand.handKeyPoints[8]["Y"]));
                    //Console.WriteLine((float)System.Math.Round(hand.handKeyPoints[8]["X"], 2));
                }
            }
        }

        /// <summary>
        /// Checks if application is Idle.
        /// </summary>
        /// <returns>true if Idle</returns>
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
        /// Event called when the app is closed. Frees resources and closes other processes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            pipeClient.Close();
            if (!HandDetection.HasExited) {
                HandDetection.Kill();
            }
        }

        /// <summary>
        /// Change finger controling the mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comFingersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(((ComboBox)sender).SelectedIndex)
            {
                case (int)Fingers.Thumb:
                    preferences.mouse = Fingers.Thumb;
                    break;
                case (int)Fingers.Index:
                    preferences.mouse = Fingers.Index;
                    break;
                case (int)Fingers.Middle:
                    preferences.mouse = Fingers.Middle;
                    break;
                case (int)Fingers.Ring:
                    preferences.mouse = Fingers.Ring;
                    break;
                case (int)Fingers.Pinky:
                    preferences.mouse = Fingers.Pinky;
                    break;
                default:
                    break;
            }

        }
    }
}
