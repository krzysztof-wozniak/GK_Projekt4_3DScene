using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class Triangle3D
    {
        public Vector<float> A { get; set; }

        public Vector<float> B { get; set; }

        public Vector<float> C { get; set; }

        public Vector<float> NormalVectorA { get; set; }

        public Vector<float> NormalVectorB { get; set; }

        public Vector<float> NormalVectorC { get; set; }

        public Color Color { get; set; }

        public Triangle3D(Vector<float> a, Vector<float> b, Vector<float> c)
        {
            A = a;
            B = b;
            C = c;
        }

        public List<Vector<float>> GetVertices()
        {
            return new List<Vector<float>>() { A, B, C };
        }
    }
}
