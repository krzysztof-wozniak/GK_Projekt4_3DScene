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
        public Point A { get; private set; }

        public Point B { get; private set; }

        public Point C { get; private set; }

        

        public Vector<float> VectorA { get; set; } //in range <-1, 1>, before mapping

        public Vector<float> VectorB { get; set; } //as above

        public Vector<float> VectorC { get; set; } //as above

        public Color Color { get; set; }

        public Triangle2D(Point a, Point b, Point c)
        {
            List<Point> points = new List<Point>() { a, b, c };
            points.Sort((x, y) => x.Y.CompareTo(y.Y));//rosnaca
            A = points[0];
            B = points[1];
            C = points[2];
        }

        public void SetPoints(Point a, Point b, Point c)
        {
            List<Point> points = new List<Point>() { a, b, c };
            points.Sort((x, y) => x.Y.CompareTo(y.Y));//rosnaca
            A = points[0];
            B = points[1];
            C = points[2];
        }

        public Triangle2D(Vector<float> a, Vector<float> b, Vector<float> c)
        {
            List<Vector<float>> points = new List<Vector<float>>() { a, b, c };
            points.Sort((x, y) => x[2].CompareTo(y[2]));//rosnaca
            VectorA = points[0];
            VectorB = points[1];
            VectorC = points[2];
        }

        public List<Point> GetVertices()
        {
            return new List<Point>() { A, B, C };
        }

        public List<ActiveEdge> GetActiveEdges()
        {
            List<Point> points = new List<Point>() { a, b, c };
            points.Sort((x, y) => x.Y.CompareTo(y.Y));//rosnaca
            A = points[0];
            B = points[1];
            C = points[2];

            List<ActiveEdge> edges = new List<ActiveEdge>();
            double dx, dy, m;
            dx = C.X - B.X;
            dy = C.Y - B.Y;
            if (dy != 0)
            {
                m = dx / dy;
                edges.Add(new ActiveEdge(C.Y, B.Y, B.X, m));
            }
            dx = C.X - A.X;
            dy = C.Y - A.Y;
            if (dy != 0)
            {
                m = dx / dy;
                edges.Add(new ActiveEdge(C.Y, A.Y, A.X, m));
            }
            dx = B.X - A.X;
            dy = B.Y - A.Y;
            if (dy != 0)
            {
                m = dx / dy;
                edges.Add(new ActiveEdge(B.Y, A.Y, A.X, m));
            }
            return edges;
        }
    }
}
