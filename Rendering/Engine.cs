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

        public void DrawModels(List<Model3D> models, DirectBitmap image, Camera camera, List<Light> lights)
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
                Matrix<float> modelMatrix = model.GetModelMatrix();
                Matrix<float> transformationMatrix = camera.CreatePerspectiveFieldOfView().Multiply(camera.CreateLookAt()).Multiply(modelMatrix);
                Model2D model2d = VertexShader.TransformModel(model, transformationMatrix, camera, lights);
                model2d.ka = model.ka;
                model2d.kd = model.kd;
                model2d.ks = model.ks;
                model2d.m = model.m;
                model2d.Color = model.Color;
                MapModel(model2d, image.Width, image.Height);
                if (model2d != null)
                    models2d.Add(model2d);
            }
            switch(this.LightModel)
            {
                case LightModel.Constant:
                    foreach (var model2d in models2d)
                    {
                        for (int i = 0; i < model2d.Triangles.Count; i++)
                        {
                            Drawer.FillPolygon(model2d.Triangles[i], image, model2d.Color, ref zBuffer, lights,
                                model2d.m, model2d.kd, model2d.ks, model2d.ka);
                        }
                    }
                    break;
                case LightModel.Gouraud:
                    foreach (var model2d in models2d)
                    {
                        for (int i = 0; i < model2d.Triangles.Count; i++)
                        {
                            Drawer.FillPolygonGouraud(model2d.Triangles[i], image, model2d.Color, ref zBuffer, lights,
                                model2d.m, model2d.kd, model2d.ks, model2d.ka);
                        }
                    }
                    break;
                case LightModel.Phong:
                    foreach (var model2d in models2d)
                    {
                        for (int i = 0; i < model2d.Triangles.Count; i++)
                        {
                            Drawer.FillPolygonPhong(model2d.Triangles[i], image, model2d.Color, ref zBuffer, lights,
                                model2d.m, model2d.kd, model2d.ks, model2d.ka, camera);
                        }
                    }
                    break;
            }
            //foreach (var model2d in models2d)
            //{
            //    for (int i = 0; i < model2d.Triangles.Count; i++)
            //    {
            //        Drawer.FillPolygon(model2d.Triangles[i], image, model2d.Color, ref zBuffer);
            //    }
            //}

            //foreach (var model2d in models2d)
            //{
            //    for (int i = 0; i < model2d.Triangles.Count; i++)
            //    {
            //        Drawer.DrawModel(model2d, image);
            //    }
            //}


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
