using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class ActiveEdge
    {
        public int yMax { get; set; }
        public int yMin { get; set; }
        public double x { get; set; }
        public double m { get; set; }

        public ActiveEdge(int yMax, int yMin, double x, double m)
        {
            this.yMax = yMax;
            this.yMin = yMin;
            this.x = x;
            this.m = m;
        }

        public void IncreaseX()
        {
            this.x += m;
        }
    }
}
