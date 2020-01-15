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
        
        public void DrawModel(Model3D model3d, DirectBitmap image, Camera camera)
        {
            if (!model3d.Visible)
                return;
            Matrix<float> transformationMatrix = camera.CreatePerspectiveFieldOfView().Multiply(camera.CreateLookAt()).Multiply(model3d.ModelMatrix);
            Model2D model2d = VertexShader.TransformModel(model3d, transformationMatrix);
            MapModel(model2d, image.Width, image.Height);
            foreach(var t in model2d.Triangles)
            {
                Drawer.DrawPolygon(t, image);
            }
        }

        private void MapModel(Model2D model, int imageWidth, int imageHeight)
        {
            for(int i = 0; i < model.Triangles.Count; i++)
            {
                model.Triangles[i].A = new Point((int)((model.Triangles[i].VectorA[0] + (float)1) * (float)imageWidth / (float)2),
                        (int)((model.Triangles[i].VectorA[1] + (float)1) * (float)imageHeight / (float)2));
                model.Triangles[i].B = new Point((int)((model.Triangles[i].VectorB[0] + (float)1) * (float)imageWidth / (float)2),
                        (int)((model.Triangles[i].VectorB[1] + (float)1) * (float)imageHeight / (float)2));
                model.Triangles[i].C = new Point((int)((model.Triangles[i].VectorC[0] + (float)1) * (float)imageWidth / (float)2),
                        (int)((model.Triangles[i].VectorC[1] + (float)1) * (float)imageHeight / (float)2));
            };
        }

        public void DrawModels(List<Model3D> models, DirectBitmap image, Camera camera)
        {
            List<Model2D> models2d = new List<Model2D>();
            //for some reason sometimes a random model in the next foreach is null
            //Parallel.ForEach(models.Where(m => m.Visible), model =>
            //{
            //    Matrix<float> transformationMatrix = Camera.CreatePerspectiveFieldOfView().Multiply(Camera.CreateLookAt()).Multiply(model.ModelMatrix);
            //    Model2D model2d = VertexShader.TransformModel(model, transformationMatrix);
            //    MapModel(model2d, image.Width, image.Height);
            //    if (model2d != null)
            //        models2d.Add(model2d);
            //});
           

            foreach (var model in models.Where(m => m.Visible))
            {
                Matrix<float> transformationMatrix = camera.CreatePerspectiveFieldOfView().Multiply(camera.CreateLookAt()).Multiply(model.ModelMatrix);
                Model2D model2d = VertexShader.TransformModel(model, transformationMatrix);
                MapModel(model2d, image.Width, image.Height);
                if (model2d != null)
                    models2d.Add(model2d);
            }
            foreach (var model2d in models2d)
            {
                foreach (var t in model2d.Triangles)
                {
                    Drawer.DrawPolygon(t, image);
                }
            }
        }
    }
}
