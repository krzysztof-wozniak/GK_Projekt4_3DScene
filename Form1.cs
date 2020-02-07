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

        private int frames = 0;

        public List<LightSource> Lights;
        


        public Form1()
        {

            InitializeComponent();
            Engine = new Engine() { };
            Models = new List<Model3D>();
            var builder = Vector<float>.Build;
            Vector<float> cameraPosition = builder.DenseOfArray(new float[] { 1.2f, -1.5f, 1.2f });
            Vector<float> cameraTarget = builder.DenseOfArray(new float[] { 0f, 0f, 0f });
            Vector<float> cameraUpVector = builder.DenseOfArray(new float[] { 0f, 0f, -1f });
            Camera cam = new Camera(cameraPosition, cameraTarget, cameraUpVector);
            Cameras.Add(cam);
            //for(int i = 0; i < 1; i++)
            //Models.Add(Model3D.CreateCone(10, 0.7f, 0.2f, Color.Green));
            Lights = new List<LightSource>();
            Lights.Add(new LightSource(0.1f, 0.1f, 0.3f, Color.FromArgb(255, 255, 255)));
            //Models.Add(Model3D.CreateCuboid(4, 4, 0.5f, 0.5f, 0.5f, Color.FromArgb(50, 100, 150)));
            

            Models.Add(Model3D.CreateSphere(15, 20, 0.5f, Color.FromArgb(70, 100, 130)));
            Models.Add(Model3D.CreateCuboid(1, 1, 0.05f, 0.05f, 0.05f, Color.Green));
            //Models[1].Position[2] = 0.5f;
            //Models[2].Position[0] = 0;
            //Models[2].Position[1] = 0;
            //Models[2].Position[2] = 0;
            fpsTimer.Start();
            //Models[1].Rotation[1] = 90f;
            //Models[1].Scale[0] = 1.1f;
            //Models[1].Scale[1] = 1.1f;
            //Models[1].Scale[2] = 1.1f;
            //Models.Add(Model3D.CreatePyramid(20, -0.5f, 0.5f));
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            UpdatePicture();
            if (!timer.Enabled)
                timer.Start();
            else
                timer.Stop();
        }

        private void UpdatePicture()
        {
            this.oldImage = this.image;
            this.image = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            Engine.DrawModels(Models, image, Cameras[chosenCameraIndex], Lights);
            pictureBox.Image = image.Bitmap;
            if (oldImage != null)
                oldImage.Dispose();
        }

        private Random r = new Random();
        private void timer_Tick(object sender, EventArgs e)
        {
            //Cameras[0].CameraPosition[0] -= 0.0003f;
            //Cameras[0].CameraPosition[1] -= 0.0003f;
            //Cameras[0].CameraPosition[2] -= 0.0003f;
            //Models[0].Rotation[2] += 1f;
            //Models[0].Rotation[0] += -0.1f;
            //Models[0].Rotation[0] = 90f;
            //Models[0].Rotation[2] += 1f;
            //Models[0].Rotation[1] += 0.5f;
            //Models[0].Position[0] = (float)Math.Sin(0.01f * time);
            //Models[0].Position[0] += ((float)r.NextDouble() - 0.5f) / 50f;
            //Models[0].Position[1] += ((float)r.NextDouble() - 0.5f) / 50f;
            //Models[1].Rotation[0] += 1f;
            //Models[1].Rotation[1] += 0.2f;


            //Cameras[0].CameraPosition[0] = 0.7f;
            //Cameras[0].CameraPosition[1] = -0.7f;
            //Cameras[0].CameraPosition[2] = 1.5f;

            //Cameras[0].CameraPosition[0] += 0.01f;
            //Cameras[0].CameraPosition[1] += 0.01f;


            //Models[0].Position[0] = 0f;
            //Models[0].Position[1] = 0f;
            Lights[0].LightPosition[0] = 2.0f * (float)Math.Sin(0.1f * time);
            Lights[0].LightPosition[1] = 2.0f * (float)Math.Cos(0.1f * time);
            Models[1].Position = Lights[0].LightPosition;


            //Cameras[0].CameraPosition[0] = 1.3f * (float)Math.Cos(0.01f * time);
            //Cameras[0].CameraPosition[1] = 1.3f * (float)Math.Sin(0.01f * time);
            //Cameras[0].CameraPosition[2] = 1.3f * (float)Math.Sin(0.01f * time);
            label1.Text = "Camera position: \n" + Cameras[0].CameraPosition.ToVectorString() + 
                "\n f: " + Cameras[0].FarPlaneDistance + "\n n: " + Cameras[0].NearPlaneDistance;

            //Cameras[0].CameraTarget[0] = (float)Math.Cos(time * Math.PI / 180);
            //Cameras[0].CameraTarget[1] = (float)Math.Sin(time * Math.PI / 180);
            //Cameras[0].CameraTarget[2] = (float)Math.Sin(time * Math.PI / 180)/2f;
            UpdatePicture();
            time += 1;
            frames += 1;
        }

        private void fpsTimer_Tick(object sender, EventArgs e)
        {
            fpsLabel.Text = "FPS: " + frames.ToString();
            frames = 0;
        }

        private void constantLightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Engine.LightModel = LightModel.Constant;
        }

        private void gouraudLightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Engine.LightModel = LightModel.Gouraud;
        }

        private void phongLightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Engine.LightModel = LightModel.Phong;
        }
    }
}
