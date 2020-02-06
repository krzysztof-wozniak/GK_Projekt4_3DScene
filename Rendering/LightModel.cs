using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public enum LightModel
    {
        Constant,
        Gouraud,
        Phong
    }

    public class LightSource
    {
        public Vector<float> LightPosition { get; set; }

        public Color LightColor { get; set; }

        public LightSource(float x, float y, float z, Color color)
        {
            this.LightPosition = Vector<float>.Build.DenseOfArray(new float[] { x, y, z });
            this.LightColor = color;
        }
    }
}
