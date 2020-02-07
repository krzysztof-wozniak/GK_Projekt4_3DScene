using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public static class Drawer
    {
        private static Random r = new Random();

        public static void FillPolygonNoLight(Triangle2D t, DirectBitmap image, Color c, ref float[,] zbuffer, List<LightSource> lights, int m, float kd, float ks, float ka)
        {
            Color color = c;
            float zA = t.TransformedA[2];
            float zB = t.TransformedB[2];
            float zC = t.TransformedC[2];
            Point A = t.A;
            Point B = t.B;
            Point C = t.C;
            var edges = t.GetActiveEdges();
            List<ActiveEdge>[] ET = new List<ActiveEdge>[image.Height];
            foreach (ActiveEdge e in edges)
            {
                if (e.yMax >= image.Height)
                    e.yMax = image.Height - 1;
                if (e.yMin < 0)
                    e.yMin = 0;
                if (ET[e.yMin] == null)
                    ET[e.yMin] = new List<ActiveEdge>();
                ET[e.yMin].Add(e);
            }//tablica ET
            int y = -1;
            for (int i = 0; i < ET.Length; i++)
            {
                if (ET[i] != null)
                {
                    y = i;
                    break;
                }
            }//najmniejszy index y

            var AET = new List<ActiveEdge>();
            if (y != -1)
            {
                for (int i = y; i < ET.Length; i++)
                {
                    if (ET[i] != null)
                        AET.AddRange(ET[i]);
                    AET.Sort((e1, e2) => e1.x.CompareTo(e2.x));//posortowane
                    for (int j = 0; j + 1 < AET.Count; j += 2)
                    {
                        int x1 = (int)Math.Round(AET[j].x);
                        int x2 = (int)Math.Round(AET[j + 1].x);
                        while (x1 <= x2)
                        {
                            float z = Interpolate(A, B, C, new Point(x1, i), zA, zB, zC);
                            if (z <= zbuffer[x1, i])
                            {
                                image.SetPixel(x1, i, color);
                                zbuffer[x1, i] = z;
                            }
                            x1++;
                        }
                    }
                    AET.RemoveAll(x => x.yMax - 1 == i);
                    foreach (var e in AET)
                    {
                        e.IncreaseX();
                    }
                }
            }

        }


        public static void FillPolygon(Triangle2D t, DirectBitmap image, Color c, ref float[,] zbuffer, List<LightSource> lights, int m, float kd, float ks, float ka)
        {
            Vector<float> cords = (t.WorldA + t.WorldB + t.WorldC).Divide(3);
            Vector<float> N = (t.NormalVectorA + t.NormalVectorB + t.NormalVectorC).Divide(3).Normalize(2);
            Vector<float> V = (t.CameraVectorA + t.CameraVectorB + t.CameraVectorC).Divide(3).Normalize(2);
            Color color = PixelShader.CalculateColor(c, cords, N, V, lights, m, ka, kd, ks);
            float zA = t.TransformedA[2];
            float zB = t.TransformedB[2];
            float zC = t.TransformedC[2];
            Point A = t.A;
            Point B = t.B;
            Point C = t.C;
            var edges = t.GetActiveEdges();
            List<ActiveEdge>[] ET = new List<ActiveEdge>[image.Height];
            foreach (ActiveEdge e in edges)
            {
                if (e.yMax >= image.Height)
                    e.yMax = image.Height - 1;
                if (e.yMin < 0)
                    e.yMin = 0;
                if (ET[e.yMin] == null)
                    ET[e.yMin] = new List<ActiveEdge>();
                ET[e.yMin].Add(e);
            }//tablica ET
            int y = -1;
            for (int i = 0; i < ET.Length; i++)
            {
                if (ET[i] != null)
                {
                    y = i;
                    break;
                }
            }//najmniejszy index y

            var AET = new List<ActiveEdge>();
            if (y != -1)
            {
                for (int i = y; i < ET.Length; i++)
                {
                    if (ET[i] != null)
                        AET.AddRange(ET[i]);
                    AET.Sort((e1, e2) => e1.x.CompareTo(e2.x));//posortowane
                    for (int j = 0; j + 1 < AET.Count; j += 2)
                    {
                        int x1 = (int)Math.Round(AET[j].x);
                        int x2 = (int)Math.Round(AET[j + 1].x);
                        while (x1 <= x2)
                        {
                            float z = Interpolate(A, B, C, new Point(x1, i), zA, zB, zC);
                            if (z <= zbuffer[x1, i])
                            {
                                image.SetPixel(x1, i, color);
                                zbuffer[x1, i] = z;
                            }
                            x1++;
                        }
                    }
                    AET.RemoveAll(x => x.yMax - 1 == i);
                    foreach (var e in AET)
                    {
                        e.IncreaseX();
                    }
                }
            }

        }


        public static void FillPolygonGouraud(Triangle2D t, DirectBitmap image, Color c, ref float[,] zbuffer, List<LightSource> lights, int m, float kd, float ks, float ka)
        {
            //Vector<float> cords = (t.WorldA + t.WorldB + t.WorldC).Divide(3);
            //Vector<float> N = (t.NormalVectorA + t.NormalVectorB + t.NormalVectorC).Divide(3).Normalize(2);
            //Vector<float> V = (t.CameraVectorA + t.CameraVectorB + t.CameraVectorC).Divide(3).Normalize(2);
            //Color color = PixelShader.CalculateColor(c, cords, N, V, lights, m, ka, kd, ks);
            Color colorA = PixelShader.CalculateColor(c, t.WorldA, t.NormalVectorA, t.CameraVectorA, lights, m, ka, kd, ks);
            Color colorB = PixelShader.CalculateColor(c, t.WorldB, t.NormalVectorB, t.CameraVectorB, lights, m, ka, kd, ks);
            Color colorC = PixelShader.CalculateColor(c, t.WorldC, t.NormalVectorC, t.CameraVectorC, lights, m, ka, kd, ks);

            float zA = t.TransformedA[2];
            float zB = t.TransformedB[2];
            float zC = t.TransformedC[2];
            Point A = t.A;
            Point B = t.B;
            Point C = t.C;
            var edges = t.GetActiveEdges();
            List<ActiveEdge>[] ET = new List<ActiveEdge>[image.Height];
            foreach (ActiveEdge e in edges)
            {
                if (e.yMax >= image.Height)
                    e.yMax = image.Height - 1;
                if (e.yMin < 0)
                    e.yMin = 0;
                if (ET[e.yMin] == null)
                    ET[e.yMin] = new List<ActiveEdge>();
                ET[e.yMin].Add(e);
            }//tablica ET
            int y = -1;
            for (int i = 0; i < ET.Length; i++)
            {
                if (ET[i] != null)
                {
                    y = i;
                    break;
                }
            }//najmniejszy index y

            var AET = new List<ActiveEdge>();
            if (y != -1)
            {
                for (int i = y; i < ET.Length; i++)
                {
                    if (ET[i] != null)
                        AET.AddRange(ET[i]);
                    AET.Sort((e1, e2) => e1.x.CompareTo(e2.x));//posortowane
                    for (int j = 0; j + 1 < AET.Count; j += 2)
                    {
                        int x1 = (int)Math.Round(AET[j].x);
                        int x2 = (int)Math.Round(AET[j + 1].x);
                        while (x1 <= x2)
                        {
                            Point p = new Point(x1, i);
                            float z = Interpolate(A, B, C, p, zA, zB, zC);
                            
                            if (z <= zbuffer[x1, i])
                            {
                                int r = (int)(255 * Interpolate(A, B, C, p, (float)colorA.R / 255f, (float)colorB.R / 255f, (float)colorC.R /255f));
                                int g = (int)(255 * Interpolate(A, B, C, p, (float)colorA.G / 255f, (float)colorB.G / 255f, (float)colorC.G / 255f));
                                int b = (int)(255 * Interpolate(A, B, C, p, (float)colorA.B / 255f, (float)colorB.B / 255f, (float)colorC.B / 255f));
                                //int r = (int)Interpolate(A, B, C, p, colorA.R, colorB.R, colorC.R);
                                //int g = (int)Interpolate(A, B, C, p, colorA.G, colorB.G, colorC.G);
                                //int b = (int)Interpolate(A, B, C, p, colorA.B, colorB.B, colorC.B);
                                if (r > 255)
                                    r = 255;
                                if (r < 0)
                                    r = 0;

                                if (g > 255)
                                    g = 255;
                                if (g < 0)
                                    g = 0;

                                if (b > 255)
                                    b = 255;
                                if (b < 0)
                                    b = 0;
                                Color color = Color.FromArgb(r, g, b);
                                image.SetPixel(x1, i, color);
                                zbuffer[x1, i] = z;
                            }
                            x1++;
                        }
                    }
                    AET.RemoveAll(x => x.yMax - 1 == i);
                    foreach (var e in AET)
                    {
                        e.IncreaseX();
                    }
                }
            }

        }

        public static void FillPolygonPhong(Triangle2D t, DirectBitmap image, Color c, ref float[,] zbuffer, List<LightSource> lights, int m, float kd, float ks, float ka, Camera camera)
        {
            
            //Color color = PixelShader.CalculateColor(c, cords, N, V, lights, m, ka, kd, ks);
            //Color colorA = PixelShader.CalculateColor(c, t.WorldA, t.NormalVectorA, t.CameraVectorA, lights, m, ka, kd, ks);
            //Color colorB = PixelShader.CalculateColor(c, t.WorldB, t.NormalVectorB, t.CameraVectorB, lights, m, ka, kd, ks);
            //Color colorC = PixelShader.CalculateColor(c, t.WorldC, t.NormalVectorC, t.CameraVectorC, lights, m, ka, kd, ks);

            float zA = t.TransformedA[2];
            float zB = t.TransformedB[2];
            float zC = t.TransformedC[2];
            Point A = t.A;
            Point B = t.B;
            Point C = t.C;
            var edges = t.GetActiveEdges();
            List<ActiveEdge>[] ET = new List<ActiveEdge>[image.Height];
            foreach (ActiveEdge e in edges)
            {
                if (e.yMax >= image.Height)
                    e.yMax = image.Height - 1;
                if (e.yMin < 0)
                    e.yMin = 0;
                if (ET[e.yMin] == null)
                    ET[e.yMin] = new List<ActiveEdge>();
                ET[e.yMin].Add(e);
            }//tablica ET
            int y = -1;
            for (int i = 0; i < ET.Length; i++)
            {
                if (ET[i] != null)
                {
                    y = i;
                    break;
                }
            }//najmniejszy index y

            var AET = new List<ActiveEdge>();
            if (y != -1)
            {
                for (int i = y; i < ET.Length; i++)
                {
                    if (ET[i] != null)
                        AET.AddRange(ET[i]);
                    AET.Sort((e1, e2) => e1.x.CompareTo(e2.x));//posortowane
                    for (int j = 0; j + 1 < AET.Count; j += 2)
                    {
                        int x1 = (int)Math.Round(AET[j].x);
                        int x2 = (int)Math.Round(AET[j + 1].x);
                        while (x1 <= x2)
                        {
                            Point p = new Point(x1, i);
                            float z = Interpolate(A, B, C, p, zA, zB, zC);
                            if (z <= zbuffer[x1, i])
                            {
                                Vector<float> cords = InterpolateVector(A, B, C, p, t.WorldA, t.WorldB, t.WorldC);
                                Vector<float> N = InterpolateVector(A, B, C, p, t.NormalVectorA, t.NormalVectorB, t.NormalVectorC).Normalize(2);
                                Vector<float> V = (camera.CameraPosition - cords).Normalize(2);
                                Color color = PixelShader.CalculateColor(c, cords, N, V, lights, m, ka, kd, ks);
                                image.SetPixel(x1, i, color);
                                zbuffer[x1, i] = z;
                            }
                            x1++;
                        }
                    }
                    AET.RemoveAll(x => x.yMax - 1 == i);
                    foreach (var e in AET)
                    {
                        e.IncreaseX();
                    }
                }
            }

        }

        public static void DrawPolygon(Triangle2D t, DirectBitmap image)
        {
            using (Graphics g = Graphics.FromImage(image.Bitmap))
            {
                g.DrawLine(Pens.Black, t.A, t.B);
                g.DrawLine(Pens.Black, t.B, t.C);
                g.DrawLine(Pens.Black, t.C, t.A);
            }
        }

        public static void DrawModel(Model2D model, DirectBitmap image)
        {
            foreach(Triangle2D t in model.Triangles)
            {
                DrawPolygon(t, image);
            }
        }

        /// <summary>
        /// Interpolates aValue from point a, bValue from point b and cValue from point c to the point p. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <param name="aValue"></param>
        /// <param name="bValue"></param>
        /// <param name="cValue"></param>
        /// <returns></returns>
        public static float Interpolate(Point a, Point b, Point c, Point p, float aValue, float bValue, float cValue)
        {
            float denominator = (b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y);

            float counter1 = (b.Y - c.Y) * (p.X - c.X) + (c.X - b.X) * (p.Y - c.Y);

            float counter2 = (c.Y - a.Y) * (p.X - c.X) + (a.X - c.X) * (p.Y - c.Y);

            float lambda1 = counter1 / denominator;
            float lambda2 = counter2 / denominator;
            float lambda3 = 1f - lambda1 - lambda2;

            return aValue * lambda1 + bValue * lambda2 + cValue * lambda3;
        }

        public static Vector<float> InterpolateVector(Point a, Point b, Point c, Point p, Vector<float> aValue, Vector<float> bValue, Vector<float> cValue)
        {
            Vector<float> res = Vector<float>.Build.Dense(aValue.Count);
            for(int i = 0; i < aValue.Count; i++)
            {
                res[i] = Interpolate(a, b, c, p, aValue[i], bValue[i], cValue[i]);
            }
            return res;
        }
    }
}
