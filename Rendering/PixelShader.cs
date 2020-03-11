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

        public static Color CalculateColor(Color objectColor, Vector<float> worldPosition, Vector<float> N, Vector<float> V,
                                           List<Light> lights, int m, float ka, float kd, float ks)
        {
            N = N.Normalize(2);
            V = V.Normalize(2);
            float r = ka;
            float b = ka;
            float g = ka;
            foreach (var light in lights)
            {
                
                Vector<float> L = (light.Position - worldPosition).Normalize(2);
                Vector<float> R = (2 * N * N.DotProduct(L) - L).Normalize(2);
                PlayerFlashlight l = light as PlayerFlashlight;
                if (l != null)
                {
                    
                    float theta = l.GetDirection().DotProduct(-L);
                    if (theta <= l.Angle)
                        continue;
                }
                float lightR = (float)light.Color.R / 255f;
                float lightG = (float)light.Color.G / 255f;
                float lightB = (float)light.Color.B / 255f;
                float NLproduct = N[0] * L[0] + N[1] * L[1] + N[2] * L[2];
                float VRproduct = V[0] * R[0] + V[1] * R[1] + V[2] * R[2];
                if (VRproduct < 0)
                    VRproduct = 0;
                VRproduct = (float)Math.Pow(VRproduct, m);
                float opt = (kd * NLproduct + ks * VRproduct);

                r += lightR * opt;
                b += lightG * opt;
                g += lightB * opt;
            }
            int cR = ClampColor((int)((float)objectColor.R * r));
            int cG = ClampColor((int)((float)objectColor.G * g));
            int cB = ClampColor((int)((float)objectColor.B * b));
            
            return Color.FromArgb(cR, cG, cB);
        }

        private static int ClampColor(int c)
        {
            if (c > 255)
                return 255;
            if (c < 0)
                return 0;
            return c;
        }
    }
}
