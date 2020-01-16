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
        public static void FillPolygonConstColor(Triangle2D t, DirectBitmap image, Color c)
        {
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
                            image.SetPixel(x1++, i, c);
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

        private static Random r = new Random();
        public static void DrawPolygon(Triangle2D t, DirectBitmap image)
        {
            using (Graphics g = Graphics.FromImage(image.Bitmap))
            {
                //Random r = new Random();
                int a = 0;
                if (a == 1)
                {


                    Color c = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                    Pen p = new Pen(c);
                    g.DrawLine(p, t.A, t.B);
                    g.DrawLine(p, t.B, t.C);
                    g.DrawLine(p, t.C, t.A);
                }
                else
                {
                    g.DrawLine(Pens.Black, t.A, t.B);
                    g.DrawLine(Pens.Black, t.B, t.C);
                    g.DrawLine(Pens.Black, t.C, t.A);
                }
            }
        }

        public static void DrawModel(Model2D model, DirectBitmap image)
        {
            foreach(Triangle2D t in model.Triangles)
            {
                DrawPolygon(t, image);
            }
        }
    }
}
