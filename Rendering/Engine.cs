using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class Engine
    {
        private float[,] zBuffer;

        public LightModel LightModel { get; set; } = LightModel.Constant;

        
        
        //public void DrawModel(Model3D model3d, DirectBitmap image, Camera camera)
        //{
        //    if (!model3d.Visible)
        //        return;
        //    Matrix<float> transformationMatrix = camera.CreatePerspectiveFieldOfView().Multiply(camera.CreateLookAt()).Multiply(model3d.GetModelMatrix());
        //    Model2D model2d = VertexShader.TransformModel(model3d, transformationMatrix);
        //    MapModel(model2d, image.Width, image.Height);
        //    foreach(var t in model2d.Triangles)
        //    {
        //        Drawer.FillPolygon(t, image, t.Color, ref zBuffer);
        //        //Drawer.DrawPolygon(t, image);
        //    }
        //}

        public void DrawModels(List<Model3D> models, DirectBitmap image, Camera camera)
        {
            zBuffer = new float[image.Width, image.Height];
            for(int i = 0; i < zBuffer.GetLength(0); i++)
            {
                for(int j = 0; j < zBuffer.GetLength(1); j++)
                {
                    zBuffer[i, j] = float.MaxValue;
                }
            }

            List<Model2D> models2d = new List<Model2D>();

            foreach (var model in models.Where(m => m.Visible))
            {
                Matrix<float> transformationMatrix = camera.CreatePerspectiveFieldOfView().Multiply(camera.CreateLookAt()).Multiply(model.GetModelMatrix());
                Model2D model2d = VertexShader.TransformModel(model, transformationMatrix);
                model2d.Color = model.Color;
                MapModel(model2d, image.Width, image.Height);
                if (model2d != null)
                    models2d.Add(model2d);
            }

            foreach (var model2d in models2d)
            {
                for (int i = 0; i < model2d.Triangles.Count; i++)
                {
                    Drawer.FillPolygon(model2d.Triangles[i], image, model2d.Color, ref zBuffer);
                }
                //for (int i = 0; i < model2d.Triangles.Count; i++)
                //{
                //    Drawer.DrawPolygon(model2d.Triangles[i], image);
                //}
                ////foreach (var t in model2d.Triangles)
                //{
                //    Drawer.DrawPolygon(t, image);
                //    //Drawer.FillPolygonConstColor(t, image, t.Color);
                //    //using (Graphics g = Graphics.FromImage(image.Bitmap))
                //    //{
                //    //    g.FillPolygon(Brushes.Red, new Point[] { t.A, t.B, t.C });
                //    //}
                //    //Drawer.FillPolygonConstColor(t, image, t.Color);


                //}
            }


        }

        private void MapModel(Model2D model, int imageWidth, int imageHeight)
        {
            for (int i = 0; i < model.Triangles.Count; i++)
            {
                Point a, b, c;
                a = new Point((int)((model.Triangles[i].TransformedA[0] + 1) * (float)imageWidth / 2f),
                        (int)((model.Triangles[i].TransformedA[1] + 1f) * (float)imageHeight / 2f));
                b = new Point((int)((model.Triangles[i].TransformedB[0] + (float)1) * (float)imageWidth / 2f),
                        (int)((model.Triangles[i].TransformedB[1] + 1f) * (float)imageHeight / 2f));
                c = new Point((int)((model.Triangles[i].TransformedC[0] + 1f) * (float)imageWidth / 2f),
                        (int)((model.Triangles[i].TransformedC[1] + 1f) * (float)imageHeight / 2f));

                model.Triangles[i].A = a;
                model.Triangles[i].B = b;
                model.Triangles[i].C = c;
            };
        }

    }
}
