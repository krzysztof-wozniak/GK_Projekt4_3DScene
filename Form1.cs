﻿using MathNet.Numerics.LinearAlgebra;
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

        private List<Model3D> Models { get; set; } = new List<Model3D>();

        private float time = 0;

        private List<Camera> Cameras = new List<Camera>();

        private int chosenCameraIndex = 0;

        private int frames = 0;

        public List<Light> Lights = new List<Light>();

        private Model3D Player;

        public Form1()
        {
            InitializeComponent();
            this.Focus();
            this.KeyPreview = true;
            Engine = new Engine() { };
            var builder = Vector<float>.Build;

            //Player
            InitPlayer();
            

            //Player flashlight
            Flashlight l = new PlayerFlashlight(0f, 0f, 0f, Color.BlueViolet, 10f, builder.DenseOfArray(new float[] { 1f, 0f, 0f }), Player);
            Lights.Add(l);

            //Cameras
            InitCameras(builder, Player);

            //Lights
            InitLights();

            //Plane
            Model3D plane = Model3D.CreateCuboid(3, 3, 5f, 5f, 0.1f, Color.Gray);
            plane.Position[2] = -0.5f;
            Models.Add(plane);

            //Big cuboid
            Models.Add(Model3D.CreateCuboid(7, 7, 7f, 7f, 7f, Color.White));
            Models.Last().Position[0] = 10f;
            Models.Last().Position[1] = 10f;
            
            //Cone
            Models.Add(Model3D.CreateCone(20, 2, 1, Color.Teal));
            
            fpsTimer.Start();
        }

        private void InitPlayer()
        {
            Player = Model3D.CreateSphere(25, 30, 1f, Color.IndianRed);
            Player.Position[0] = -3f;
            Player.Position[1] = -3f;
            Player.Position[2] = 0f;
            Models.Add(Player);
            
        }

        private void InitCameras(VectorBuilder<float> builder, Model3D player)
        {
            //Cameras
            //Static Camera
            Vector<float> cameraPosition = builder.DenseOfArray(new float[] { 4f, 6f, 5f });
            Vector<float> cameraTarget = builder.DenseOfArray(new float[] { 0f, 0f, 0f });
            Vector<float> cameraUpVector = builder.DenseOfArray(new float[] { 0f, 0f, -1f });
            Camera cam = new Camera(cameraPosition, player.Position, cameraUpVector);
            Camera staticCamera = new Camera(cameraPosition.Clone(), cameraTarget.Clone(), cameraUpVector.Clone());
            Cameras.Add(staticCamera);
            Cameras.Add(cam);

            //Camera behind player
            PlayerCamera playerCamera = new PlayerCamera(cameraPosition.Clone(), cameraTarget.Clone(), cameraUpVector.Clone());
            playerCamera.PlayerToFollow = player;
            Cameras.Add(playerCamera);
        }

        private void InitLights()
        {
            Light light;
            Model3D lightModel;
            light = new Light(0f, 2f, 0f, Color.FromArgb(255, 255, 255));
            Lights.Add(light);
            lightModel = Model3D.CreateCuboid(1, 1, 0.15f, 0.15f, 0.15f, Color.Yellow);
            Models.Add(lightModel);
            lightModel.Position = light.Position;

            light = new Light(0f, 2f, 20f, Color.FromArgb(255, 255, 255));
            Lights.Add(light);
            lightModel = Model3D.CreateCuboid(1, 1, 0.15f, 0.15f, 0.15f, Color.Yellow);
            Models.Add(lightModel);
            lightModel.Position = light.Position;

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
            Models.Last().Position[2] =  1.1f + (float)(1.3 * Math.Sin(time * 0.1));
            Models.Last().Rotation[2] += 2f;
            
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
                    Player.Position[0] += 0.5f * (float)Math.Cos(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    Player.Position[1] += 0.5f * (float)Math.Sin(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    break;
                case Keys.S:
                    Player.Position[0] -= 0.5f * (float)Math.Cos(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    Player.Position[1] -= 0.5f * (float)Math.Sin(MathExtentions.DegreeToRadian(Models[0].Rotation[2]));
                    break;
                case Keys.A:
                    Player.Rotation[2] -= 3f;
                    break;
                case Keys.D:
                    Player.Rotation[2] += 3f;
                    break;
                case Keys.E:
                    Player.Position[2] += 0.5f;
                    break;
                case Keys.Q:
                    Player.Position[2] -= 0.5f;
                    break;
            }
        }
    }
}
