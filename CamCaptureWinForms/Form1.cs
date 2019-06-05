using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        Mat frame;
        Bitmap image;
        private Task camera;
        bool isCameraRunning = false;
        public Form1()
        {
            InitializeComponent();
        }

        //TODO: añadir comentarios que expliquen el código
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Tomar Foto"))
            {
                CaptureCamera();
                button1.Text = "Stop";
                isCameraRunning = true;
            }
            else
            {
                capture.Release();
                button1.Text = "Tomar Foto";
                isCameraRunning = false;
            }
        }



        private Task CaptureCamera()
        {
            camera = Task.Run(()=>CaptureCameraCallback());
            return camera;
        }

        private void CaptureCameraCallback()
        {

            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);

            if (capture.IsOpened())
            {
                while (isCameraRunning)
                {

                    capture.Read(frame);
                    image = (Bitmap)BitmapConverter.ToBitmap(frame);
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    pictureBox1.Image = image;
                }
            }
        }

    }


}
