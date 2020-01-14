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

        public static Matrix<float> modelMatrix = Matrix<float>.Build.DenseOfArray(new float[,] {
                                                                                     { 1, 0, 0, 0 },
                                                                                     { 0, 1, 0, 0 },
                                                                                     { 0, 0, 1, 0 },
                                                                                     { 0, 0, 0, 1 } });

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            //Model3D Cone = Model3D.CreateCone(5, (float)0.8, (float)1);
            int n = 5;
            Model3D Cone = new Model3D();
            Cone.Triangles = new List<Triangle3D>();
            var vectorBuilder = Vector<float>.Build;
            Vector<float> p1 = Vector<float>.Build.DenseOfArray(new float[] { 0, 0, -1.2f, 1 });
            Vector<float> p00 = Vector<float>.Build.DenseOfArray(new float[] { 0, 0, 0, 1 });
            Vector<float>[] points = new Vector<float>[n];//punkty w podstawie
            float deg = (float)360 / (float)n;
            for (int i = 0; i < n; i++)
            {
                points[i] = vectorBuilder.DenseOfArray(new float[] { (float)Math.Cos(Math.PI / 180 * i * deg), (float)Math.Sin(Math.PI / 180 * i * deg), 0, 1 });
            }

            for (int i = 0; i < points.Length; i++)
            {
                Cone.Triangles.Add(new Triangle3D(points[i], points[(i + 1) % points.Length], p00));
                Cone.Triangles.Add(new Triangle3D(points[i], points[(i + 1) % points.Length], p1));
            }
            Camera cam = new Camera();
            Model2D m = VertexShader.Transform(Cone, modelMatrix, cam.CreteLookAt(), cam.CreatePerspectiveFieldOfView());
            this.oldImage = this.image;

            this.image = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            for (int i = 0; i < m.Triangles.Count; i++)
            {
                m.Triangles[i].A = new Point((int)((m.Triangles[i].VectorA[0] + (float)1) * (float)image.Width / (float)2),
                        (int)((m.Triangles[i].VectorA[1] + (float)1) * (float)image.Height / (float)2));
                m.Triangles[i].B = new Point((int)((m.Triangles[i].VectorB[0] + (float)1) * (float)image.Width / (float)2),
                        (int)((m.Triangles[i].VectorB[1] + (float)1) * (float)image.Height / (float)2));
                m.Triangles[i].C = new Point((int)((m.Triangles[i].VectorC[0] + (float)1) * (float)image.Width / (float)2),
                        (int)((m.Triangles[i].VectorC[1] + (float)1) * (float)image.Height / (float)2));
            }
            foreach (var t in m.Triangles)
            {
                Drawer.DrawPolygon(t, image);
            }
            pictureBox.Image = image.Bitmap;
            if (oldImage != null)
                oldImage.Dispose();
        }
    }
}
