using GK_Projekt4_3DScene.Extentions;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class Model3D
    {
        public static Random random = new Random();

        public List<Triangle3D> Triangles { get; set; }

        //dodac funkcje ktora zwraca ModelMatrix na podstawie pozycji, obrotu, skalowania   
        //dodac pola na pozycje, obrot, skalowanie
        public Vector<float> Position { get; set; } = Vector<float>.Build.Dense(3, 0f);//position in world coordinates

        public Vector<float> Rotation { get; set; } = Vector<float>.Build.Dense(3, 0f); //x, y, z rotation

        public Vector<float> Scale { get; set; } = Vector<float>.Build.Dense(3, 1f);

        public Vector<float> MiddlePoint { get; set; } = Vector<float>.Build.Dense(3, 0f);

        public bool Visible { get; set; } = true;


        //public Matrix<float> ModelMatrix = Matrix<float>.Build.DenseOfArray(new float[,] {
        //                                                                             { 1, 0, 0, 0 },
        //                                                                             { 0, 1, 0, 0 },
        //                                                                             { 0, 0, 1, 0 },
        //                                                                             { 0, 0, 0, 1 } }); //default model matrix

        public Matrix<float> GetModelMatrix()
        {

            Matrix<float> translationMatrix = Matrix<float>.Build.DenseDiagonal(4, 4, 1f);
            Matrix<float> scaleMatrix = Matrix<float>.Build.DenseDiagonal(4, 4, 1f);
            Matrix<float> rotationMatrixX = Matrix<float>.Build.DenseDiagonal(4, 4, 1f);
            Matrix<float> rotationMatrixY = Matrix<float>.Build.DenseDiagonal(4, 4, 1f);
            Matrix<float> rotationMatrixZ = Matrix<float>.Build.DenseDiagonal(4, 4, 1f);

            //translation:
            Vector<float> offset = Vector<float>.Build.Dense(4, 1f);
            for (int i = 0; i < 3; i++)
                offset[i] = Position[i] - MiddlePoint[i];
            translationMatrix.SetColumn(3, offset);

            //rotation:
            float angle = (float)MathExtentions.DegreeToRadian(Rotation[0]);
            rotationMatrixX[1, 1] = (float)Math.Cos(angle);
            rotationMatrixX[2, 2] = rotationMatrixX[1, 1];
            rotationMatrixX[2, 1] = (float)Math.Sin(angle);
            rotationMatrixX[1, 2] = -rotationMatrixX[2, 1];

            angle = (float)MathExtentions.DegreeToRadian(Rotation[1]);
            rotationMatrixY[0, 0] = (float)Math.Cos(angle);
            rotationMatrixY[2, 2] = rotationMatrixY[0, 0];
            rotationMatrixY[0, 2] = (float)Math.Sin(angle);
            rotationMatrixY[2, 0]= -rotationMatrixY[0, 2];

            angle = (float)MathExtentions.DegreeToRadian(Rotation[2]);
            rotationMatrixZ[0, 0] = (float)Math.Cos(angle);
            rotationMatrixZ[1, 1] = rotationMatrixZ[0, 0];
            rotationMatrixZ[1, 0] = (float)Math.Sin(angle);
            rotationMatrixZ[0, 1] = -rotationMatrixZ[1, 0];

            //scale:
            for (int i = 0; i < 3; i++)
                scaleMatrix[i, i] = Scale[i];

            return translationMatrix.Multiply(rotationMatrixX).Multiply(rotationMatrixY)
                .Multiply(rotationMatrixZ).Multiply(scaleMatrix);


        }

        public static Model3D CreatePyramid(int n, float height, float radius)
        {
            Model3D cone = new Model3D() { Triangles = new List<Triangle3D>() };
            
            var builder = Vector<float>.Build;
            Vector<float> p0 = builder.DenseOfArray(new float[] { 0f, 0f, 0f, 1f }); //base center
            cone.MiddlePoint = p0;
            Vector<float> p1 = builder.DenseOfArray(new float[] { 0f, 0f, height, 1f }); //wierzcholek gorny
            Vector<float>[] basePoints = new Vector<float>[n];//cone base points
            for(int i = 0; i < n; i++)
            {
                double deg = Math.PI * (double)i * 2.0 / (double)n;
                basePoints[i] = builder.DenseOfArray(new float[] { radius * (float)Math.Cos(deg), radius * (float)Math.Sin(deg), 0, 1 });
            }
            for(int i = 0; i < n - 1; i++)
            {
                var t1 = new Triangle3D(basePoints[i], basePoints[i + 1], p0);
                cone.Triangles.Add(t1);
                var t2 = new Triangle3D(basePoints[i], basePoints[i + 1], p1);
                cone.Triangles.Add(t2);
            }
            cone.Triangles.Add(new Triangle3D(basePoints[n - 1], basePoints[0], p0));
            cone.Triangles.Add(new Triangle3D(basePoints[n - 1], basePoints[0], p1));
            for(int i = 0; i < cone.Triangles.Count; i++)
            {
                cone.Triangles[i].Color = Color.FromArgb(random.Next(50, 200), random.Next(50, 200), random.Next(50, 200));
            }
            return cone;
        }
    }
}
