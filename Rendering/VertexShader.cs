﻿using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public static class VertexShader
    {
        ////Better to mult the matrices once before transforming everything
        //public static Model2D TransformModel(Model3D model3D, Matrix<float> modelMatrix, Matrix<float> viewMatrix, Matrix<float> projectionMatrix)
        //{
        //    Model2D model2D = new Model2D();
        //    model2D.Triangles = new List<Triangle2D>();
        //    Vector<float> a, b, c;
        //    foreach (Triangle3D triangle3D in model3D.Triangles)
        //    {
        //        a = TransformVector(triangle3D.A, modelMatrix, viewMatrix, projectionMatrix);
        //        b = TransformVector(triangle3D.B, modelMatrix, viewMatrix, projectionMatrix);
        //        c = TransformVector(triangle3D.C, modelMatrix, viewMatrix, projectionMatrix);
        //        if (!isInRange(a) || !isInRange(b) || !isInRange(c))
        //            continue;
        //        Triangle2D t = new Triangle2D(a, b, c);
        //        model2D.Triangles.Add(t);
        //    }
        //    return model2D;
        //}

        public static Model2D TransformModel(Model3D model3D, Matrix<float> transformationMatrix, Camera camera, List<Light> lights)
        {
            Matrix<float> modelMatrix = model3D.GetModelMatrix();
            Model2D model2D = new Model2D();
            model2D.Triangles = new List<Triangle2D>();
            Vector<float> a, b, c;
            foreach (Triangle3D triangle3D in model3D.Triangles)
            {
                //Points on the screen
                a = TransformVector(triangle3D.A, transformationMatrix);
                b = TransformVector(triangle3D.B, transformationMatrix);
                c = TransformVector(triangle3D.C, transformationMatrix);
                if (!isInRange(a) || !isInRange(b) || !isInRange(c))
                    continue;
                Triangle2D t = new Triangle2D(a, b, c);

                //Real world cords
                t.WorldA = TransformVector(triangle3D.A, modelMatrix).SubVector(0, 3);
                t.WorldB = TransformVector(triangle3D.B, modelMatrix).SubVector(0, 3);
                t.WorldC = TransformVector(triangle3D.C, modelMatrix).SubVector(0, 3);
                var MNormal = modelMatrix.Inverse().Transpose();

                //Real world normal vectors
                t.NormalVectorA = MNormal.Multiply(triangle3D.NormalVectorA).SubVector(0, 3).Normalize(2);
                t.NormalVectorB = MNormal.Multiply(triangle3D.NormalVectorB).SubVector(0, 3).Normalize(2);
                t.NormalVectorC = MNormal.Multiply(triangle3D.NormalVectorC).SubVector(0, 3).Normalize(2);

                //Real world vector to the camera
                t.CameraVectorA = (camera.CameraPosition - t.WorldA.SubVector(0, 3)).Normalize(2);
                t.CameraVectorB = (camera.CameraPosition - t.WorldB.SubVector(0, 3)).Normalize(2);
                t.CameraVectorC = (camera.CameraPosition - t.WorldC.SubVector(0, 3)).Normalize(2);

                if (t.CameraVectorA.DotProduct(t.NormalVectorA) < -1)
                    continue;

                t.LightVectorsA = new List<Vector<float>>();
                t.LightVectorsB = new List<Vector<float>>();
                t.LightVectorsC = new List<Vector<float>>();

                foreach (var light in lights)
                {
                    t.LightVectorsA.Add(light.Position - t.WorldA.SubVector(0, 3));
                    t.LightVectorsB.Add(light.Position - t.WorldB.SubVector(0, 3));
                    t.LightVectorsC.Add(light.Position - t.WorldC.SubVector(0, 3));
                }

                t.Color = triangle3D.Color;
                model2D.Triangles.Add(t);
            }
            return model2D;
        }

        private static Vector<float> TransformVector(Vector<float> v, Matrix<float> modelMatrix, Matrix<float> viewMatrix, Matrix<float> projectionMatrix)
        {
            var p = ((projectionMatrix.Multiply(viewMatrix)).Multiply(modelMatrix)).Multiply(v);
            p = p.Multiply((float)1 / p[3]); //x y z 1, wspolrzedne naleza do <-1, 1>
            return p;
        }

        private static Vector<float> TransformVector(Vector<float> v, Matrix<float> transformationMatrix)
        {
            var p = transformationMatrix.Multiply(v);
            p = p.Multiply((float)1 / p[3]); //x y z 1, wspolrzedne naleza do <-1, 1>
            return p;
        }


        private static bool isInRange(Vector<float> v)
        {
            if (v[0] <= -1 || v[0] >= 1 || v[1] <= -1 || v[1] >= 1 || v[2] <= -1 || v[2] >= 1)
                return false;
            return true;
        }
    }
}
