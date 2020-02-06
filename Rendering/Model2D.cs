using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class Model2D
    {
        public List<Triangle2D> Triangles { get; set; }
        
        public Color Color { get; set; }

        public int m { get; set; }

        public float ka { get; set; }

        public float kd { get; set; }

        public float ks { get; set; }
    }
}
