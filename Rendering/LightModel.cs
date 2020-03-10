using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public enum LightModel
    {
        Constant,
        Gouraud,
        Phong
    }

    public class Light
    {
        public Vector<float> Position { get; set; }

        public Color Color { get; set; }

        public Light(float x, float y, float z, Color color)
        {
            this.Position = Vector<float>.Build.DenseOfArray(new float[] { x, y, z });
            this.Color = color;
        }


    }

    public class Flashlight : Light
    {
        public Vector<float> Direction { get; set; }

        public float Angle { get; set; }

        public Flashlight(float x, float y, float z, Color color, float angle, Vector<float> dir) : base(x, y, z, color)
        {
            this.Direction = dir.Normalize(2);
            this.Angle = (float)Math.Cos(MathExtentions.DegreeToRadian(angle));
        }

    }

    public class PlayerFlashlight : Flashlight
    {
        public Model3D Player { get; set; }

        public PlayerFlashlight(float x, float y, float z, Color color, float angle, Vector<float> dir, Model3D player) : base(x, y, z, color, angle, dir)
        {
            this.Player = player;
            this.Position = player.Position;
        }

        public Vector<float> GetDirection()
        {
            float x = (float)Math.Cos(MathExtentions.DegreeToRadian(Player.Rotation[2]));
            float y = (float)Math.Sin(MathExtentions.DegreeToRadian(Player.Rotation[2]));
            return Vector<float>.Build.DenseOfArray(new float[] { x, y, 0f }).Normalize(2);
            
        }
    }

}
