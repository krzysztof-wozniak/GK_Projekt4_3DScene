using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class Model3D
    {
        public List<Triangle3D> Triangles { get; set; }

        public static Model3D CreateCone(int n, float height, float radius)
        {
            Model3D cone = new Model3D() { Triangles = new List<Triangle3D>() };
            var builder = Vector<float>.Build;
            Vector<float> p0 = builder.DenseOfArray(new float[] { 0f, 0f, 0f, 1f }); //base center
            Vector<float> p1 = builder.DenseOfArray(new float[] { 0f, 0f, height, 1f }); //wierzcholek gorny
            Vector<float>[] basePoints = new Vector<float>[n];//cone base points
            for(int i = 0; i < n; i++)
            {
                double deg = Math.PI * (double)i * 2.0 / (double)n;
                basePoints[i] = builder.DenseOfArray(new float[] { radius * (float)Math.Cos(deg), radius * (float)Math.Sin(deg), 0, 1 });
            }
            for(int i = 0; i < n - 1; i++)
            {
                cone.Triangles.Add(new Triangle3D(basePoints[i], basePoints[i + 1], p0));
                cone.Triangles.Add(new Triangle3D(basePoints[i], basePoints[i + 1], p1));
            }
            cone.Triangles.Add(new Triangle3D(basePoints[n - 1], basePoints[0], p0));
            cone.Triangles.Add(new Triangle3D(basePoints[n - 1], basePoints[0], p1));
            return cone;
        }
    }
}
