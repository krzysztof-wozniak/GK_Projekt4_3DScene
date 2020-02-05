using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public static class PixelShader
    {
        public static Color CalculateColor(Color objectColor, float ka, float kd, float ks, Vector<float> normalVecotr, List<LightSource> lights,
                                           Vector<float> cameraVector, int m)
        {

            return Color.Red;
        }
    }
}
