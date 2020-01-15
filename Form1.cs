using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_Projekt4_3DScene
{
    public partial class Form1 : Form
    {
        private DirectBitmap image;

        private DirectBitmap oldImage;

        private Engine Engine { get; set; }

        private List<Model3D> Models { get; set; }

        private float time = 0;

        private List<Camera> Cameras = new List<Camera>();

        private int chosenCameraIndex = 0;
        


        public Form1()
        {
            InitializeComponent();
            Engine = new Engine() { };
            Models = new List<Model3D>();
            var builder = Vector<float>.Build;
            Vector<float> cameraPosition = builder.DenseOfArray(new float[] { 3f, 0.5f, 0.5f });
            Vector<float> cameraTarget = builder.DenseOfArray(new float[] { 0f, 0f, 0f });
            Vector<float> cameraUpVector = builder.DenseOfArray(new float[] { 0f, 0f, 1f });
            Camera cam = new Camera(cameraPosition, cameraTarget, cameraUpVector);
            Cameras.Add(cam);
            for(int i = 0; i < 1; i++)
                Models.Add(Model3D.CreatePyramid(10, -0.5f, 0.5f));
            //Models.Add(Model3D.CreatePyramid(20, -0.5f, 0.5f));
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            UpdatePicture();
            timer.Start();
        }

        private void UpdatePicture()
        {
            this.oldImage = this.image;
            this.image = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            Engine.DrawModels(Models, image, Cameras[chosenCameraIndex]);
            pictureBox.Image = image.Bitmap;
            if (oldImage != null)
                oldImage.Dispose();
        }

        private void timer_Tick(object sender, EventArgs e)
        {

            Models[0].ModelMatrix[0, 0] = (float)Math.Cos(time * Math.PI / 180);
            Models[0].ModelMatrix[0, 1] = -(float)Math.Sin(time * Math.PI / 180);
            Models[0].ModelMatrix[1, 0] = (float)Math.Sin(time * Math.PI / 180);
            Models[0].ModelMatrix[1, 1] = (float)Math.Cos(time * Math.PI / 180);
            UpdatePicture();
            time += 1;
        }
    }
}
