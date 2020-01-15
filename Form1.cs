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
        

        

        public Form1()
        {
            InitializeComponent();
            Engine = new Engine() { Camera = new Camera() };
            Models = new List<Model3D>();
            Models.Add(Model3D.CreateCone(10, -0.5f, 1f));
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            UpdatePicture();
        }

        private void UpdatePicture()
        {
            this.oldImage = this.image;
            this.image = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            Engine.DrawModels(Models, image);
            pictureBox.Image = image.Bitmap;
            if (oldImage != null)
                oldImage.Dispose();
        }
    }
}
