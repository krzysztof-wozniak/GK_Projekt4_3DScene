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
                //R = R.Normalize(2);
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
                if(N[2] > 0)
                {
                    Console.Write("elo");
                }
                //NLproduct = N.DotProduct(L);
                //if (NLproduct < 0)
                //{
                //    break;
                //    NLproduct = 0;
                //    //return Color.Blue;
                //}
                float VRproduct = V[0] * R[0] + V[1] * R[1] + V[2] * R[2];
                if (VRproduct < 0)
                    VRproduct = 0;
                VRproduct = (float)Math.Pow(VRproduct, m);
                float opt = (kd * NLproduct + ks * VRproduct);

                r += lightR * opt;
                b += lightG * opt;
                g += lightB * opt;
            }
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
