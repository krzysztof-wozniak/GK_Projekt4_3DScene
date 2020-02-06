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
        //Dostaje na wejscie wektor normalny, wektor do swiatla, wektor do obserwatora
        public static Color CalculateColor(Color objectColor, Vector<float> worldPosition, Vector<float> N, Vector<float> V,
                                           List<LightSource> lights, int m, float ka, float kd, float ks)
        {
            var light = lights[0];
            Vector<float> L = (light.LightPosition - worldPosition).Normalize(2);
            Vector<float> R = 2 * N * N.CrossProduct(L) - L;
            R.Normalize(2);

            float lightR = (float)light.LightColor.R / 255f;
            float lightG = (float)light.LightColor.G / 255f;
            float lightB = (float)light.LightColor.B / 255f;
            float lightIntensR = (float)light.LightColor.R / 255f;
            float lightIntensG = (float)light.LightColor.G / 255f;
            float lightIntensB = (float)light.LightColor.B / 255f;
            float NLproduct = N[0] * L[0] + N[1] * L[1] + N[2] * L[2];//N.DotProduct(L);
            if (NLproduct < 0)
                NLproduct = 0;
            float VRproduct = V[0] * R[0] + V[1] * R[1] + V[2] * R[2];//N.DotProduct(L);
            VRproduct = (float)Math.Pow(VRproduct, m);
            if (VRproduct < 0)
                VRproduct = 0;

            float r = ka + kd * lightIntensR * NLproduct + ks * VRproduct;
            float g = ka + kd * lightIntensG * NLproduct + ks * VRproduct;
            float b = ka + kd * lightIntensB * NLproduct + ks * VRproduct;

            //float test = ks * VRproduct;
            int cR = (int)((float)objectColor.R * r);
            int cG = (int)((float)objectColor.G * g);
            int cB = (int)((float)objectColor.B * b);

            if (cR > 255)
                cR = 255;
            if (cR < 0)
                cR = 0;

            if (cG > 255)
                cG = 255;
            if (cG < 0)
                cG = 0;

            if (cB > 255)
                cB = 255;
            if (cB < 0)
                cB = 0;
            return Color.FromArgb(cR, cG, cB);
        }
    }
}
