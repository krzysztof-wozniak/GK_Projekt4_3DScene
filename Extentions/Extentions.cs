using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene.Extentions
{
    public static class MathExtentions
    {
        public static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180.0;
        }
    }

    public static class MathNetExtentions
    {
        public static Vector<float> CrossProduct(this Vector<float> v1, Vector<float> v2)
        {
            if ((v1.Count != 3 || v2.Count != 3))
            {
                string message = "Vectors must have a length of 3.";
                throw new Exception(message);
            }
            Vector<float> result = Vector<float>.Build.Dense(3); 
            result[0] = v1[1] * v2[2] - v1[2] * v2[1];
            result[1] = -v1[0] * v2[2] + v1[2] * v2[0];
            result[2] = v1[0] * v2[1] - v1[1] * v2[0];
            return result;
        }
    }
}
