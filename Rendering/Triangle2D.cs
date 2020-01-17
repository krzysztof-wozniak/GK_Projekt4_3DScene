using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class Triangle2D
    {
        public Point A { get; set; }

        public Point B { get; set; }

        public Point C { get; set; }

        public float ZBufferA { get; set; }

        public float ZBufferB { get; set; }

        public float ZBufferC { get; set; }

        public Vector<float> VectorA { get; set; } //in range <-1, 1>, before mapping

        public Vector<float> VectorB { get; set; } //as above

        public Vector<float> VectorC { get; set; } //as above

        public Color Color { get; set; }

        public Triangle2D(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Triangle2D(Vector<float> a, Vector<float> b, Vector<float> c)
        {
            VectorA = a;
            VectorB = b;
            VectorC = c;
        }

        public List<Point> GetVertices()
        {
            return new List<Point>() { A, B, C };
        }

        public List<ActiveEdge> GetActiveEdges()
        {
            List<Point> points = new List<Point>() { this.A, this.B, this.C };
            points.Sort((x, y) => x.Y.CompareTo(y.Y));//rosnaca
            var a = points[0];
            var b = points[1];
            var c = points[2];

            List<ActiveEdge> edges = new List<ActiveEdge>();
            double dx, dy, m;
            dx = c.X - b.X;
            dy = c.Y - b.Y;
            if (dy != 0)
            {
                m = dx / dy;
                edges.Add(new ActiveEdge(c.Y, b.Y, b.X, m));
            }
            dx = c.X - a.X;
            dy = c.Y - a.Y;
            if (dy != 0)
            {
                m = dx / dy;
                edges.Add(new ActiveEdge(c.Y, a.Y, a.X, m));
            }
            dx = b.X - a.X;
            dy = b.Y - a.Y;
            if (dy != 0)
            {
                m = dx / dy;
                edges.Add(new ActiveEdge(b.Y, a.Y, a.X, m));
            }
            return edges;
        }
    }
}
