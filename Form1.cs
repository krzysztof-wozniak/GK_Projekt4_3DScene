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

        public List<Light> Lights;
        


        public Form1()
        {

            InitializeComponent();
            this.Focus();
            this.KeyPreview = true;
            Engine = new Engine() { };
            Models = new List<Model3D>();
            var builder = Vector<float>.Build;
            Vector<float> cameraPosition = builder.DenseOfArray(new float[] { 4f, 6f, 5f });
            Vector<float> cameraTarget = builder.DenseOfArray(new float[] { 0f, 0f, 0f });
            Vector<float> cameraUpVector = builder.DenseOfArray(new float[] { 0f, 0f, -1f });
            Camera cam = new Camera(cameraPosition, cameraTarget, cameraUpVector);
            Camera staticCamera = new Camera(cameraPosition.Clone(), cameraTarget.Clone(), cameraUpVector.Clone());
            Cameras.Add(staticCamera);
            Cameras.Add(cam);
            //for(int i = 0; i < 1; i++)
            //Models.Add(Model3D.CreateCone(10, 0.7f, 0.2f, Color.Green));
            Lights = new List<Light>();
            Lights.Add(new Light(0f, 2f, 0f, Color.FromArgb(255, 255, 255)));
            Lights.Add(new Light(0f, 2f, 20f, Color.FromArgb(255, 255, 255)));
            //Lights.Add(new LightSource(-0.5f, -0.6f, 0.2f, Color.Red));
            //Models.Add(Model3D.CreateCuboid(4, 4, 0.5f, 0.5f, 0.5f, Color.FromArgb(50, 100, 150)));
            Model3D plane = Model3D.CreateCuboid(3, 3, 5f, 5f, 0.1f, Color.Gray);
            plane.Position[2] = -0.5f;
            //Model3D player = Model3D.CreateCuboid(1, 1, 1f, 1f, 1f, Color.IndianRed);
            Model3D player = Model3D.CreateSphere(25, 30, 1f, Color.IndianRed);
            PlayerCamera playerCamera = new PlayerCamera(cameraPosition.Clone(), cameraTarget.Clone(), cameraUpVector.Clone());
            playerCamera.PlayerToFollow = player;
            Cameras.Add(playerCamera);
            player.Position[0] = -3f;
            player.Position[1] = -3f;
            player.Position[2] = 0f;
            cam.CameraTarget = player.Position;
            Models.Add(player);
            Models.Add(plane);
            Model3D lightModel = Model3D.CreateCuboid(1, 1, 0.05f, 0.05f, 0.05f, Color.Yellow);
            Models.Add(lightModel);
            lightModel.Position = Lights[0].Position;
            Models.Add(Model3D.CreateCuboid(7, 7, 7f, 7f, 7f, Color.White));
            Models.Last().Position[0] = 10f;
            Models.Last().Position[1] = 10f;
            Flashlight l = new PlayerFlashlight(0f, 0f, 0f, Color.BlueViolet, 10f, builder.DenseOfArray(new float[] { 1f, 0f, 0f }), player);
            Lights.Add(l);

            //Models.Add(Model3D.CreateSphere(15, 15, 0.5f, Color.FromArgb(255, 255, 255)));

            //Models.Add(Model3D.CreateCuboid(3, 3, 0.6f, 0.6f, 0.5f, Color.Beige));
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
            label1.Text = "Camera position: \n" + Cameras[0].CameraPosition.ToVectorString() + 
                "\n f: " + Cameras[0].FarPlaneDistance + "\n n: " + Cameras[0].NearPlaneDistance;
            
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
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.F1:
                    chosenCameraIndex = 0;
                    break;
                case Keys.F2:
                    chosenCameraIndex = 1;
                    break;
                case Keys.F3:
                    chosenCameraIndex = 2;
                    break;
                case Keys.W:
                    Models[0].Position[0] += 0.5f * (float)Math.Cos(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    Models[0].Position[1] += 0.5f * (float)Math.Sin(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    break;
                case Keys.S:
                    Models[0].Position[0] -= 0.5f * (float)Math.Cos(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    Models[0].Position[1] -= 0.5f * (float)Math.Sin(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    break;
                case Keys.A:
                    Models[0].Rotation[2] -= 3f;
                    break;
                case Keys.D:
                    Models[0].Rotation[2] += 3f;
                    break;
                case Keys.E:
                    Models[0].Position[2] += 0.5f;
                    break;
                case Keys.Q:
                    Models[0].Position[2] -= 0.5f;
                    break;
            }
        }
    }
}
