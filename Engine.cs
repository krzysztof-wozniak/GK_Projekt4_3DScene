﻿using MathNet.Numerics.LinearAlgebra;
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
        public Camera Camera { get; set; }

        //public Matrix<float> 
        public void DrawModel(Model3D model3d, DirectBitmap image)
        {
            Matrix<float> transformationMatrix = Camera.CreatePerspectiveFieldOfView().Multiply(Camera.CreateLookAt()).Multiply(model3d.ModelMatrix);
            Model2D model2d = VertexShader.TransformModel(model3d, transformationMatrix);
            MapModel(model2d, image.Width, image.Height);
            foreach(var t in model2d.Triangles)
            {
                Drawer.DrawPolygon(t, image);
            }
        }

        private void MapModel(Model2D model, int imageWidth, int imageHeight)
        {
            Parallel.For(0, model.Triangles.Count, i =>
            {
                model.Triangles[i].A = new Point((int)((model.Triangles[i].VectorA[0] + (float)1) * (float)imageWidth / (float)2),
                        (int)((model.Triangles[i].VectorA[1] + (float)1) * (float)imageHeight / (float)2));
                model.Triangles[i].B = new Point((int)((model.Triangles[i].VectorB[0] + (float)1) * (float)imageWidth / (float)2),
                        (int)((model.Triangles[i].VectorB[1] + (float)1) * (float)imageHeight / (float)2));
                model.Triangles[i].C = new Point((int)((model.Triangles[i].VectorC[0] + (float)1) * (float)imageWidth / (float)2),
                        (int)((model.Triangles[i].VectorC[1] + (float)1) * (float)imageHeight / (float)2));
            });
        }

        public void DrawModels(List<Model3D> models, DirectBitmap image)
        {
            List<Model2D> models2d = new List<Model2D>();
            Parallel.ForEach(models, model =>
            {
                Matrix<float> transformationMatrix = Camera.CreatePerspectiveFieldOfView().Multiply(Camera.CreateLookAt()).Multiply(model.ModelMatrix);
                Model2D model2d = VertexShader.TransformModel(model, transformationMatrix);
                MapModel(model2d, image.Width, image.Height);
                models2d.Add(model2d);
            });
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
