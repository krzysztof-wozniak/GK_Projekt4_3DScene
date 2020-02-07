using GK_Projekt4_3DScene;
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
        public Color Color { get; set; }

        public int m { get; set; } = 30;

        public float ka { get; set; } = 0.2f;

        public float kd { get; set; } = 0.5f;

        public float ks { get; set; } = 1f;

        public static Random random = new Random();

        public List<Triangle3D> Triangles { get; set; }

        //dodac funkcje ktora zwraca ModelMatrix na podstawie pozycji, obrotu, skalowania   
        //dodac pola na pozycje, obrot, skalowanie
        public Vector<float> Position { get; set; } = Vector<float>.Build.Dense(3, 0f);//position in world coordinates

        public Vector<float> Rotation { get; set; } = Vector<float>.Build.Dense(3, 0f); //x, y, z rotation

        public Vector<float> Scale { get; set; } = Vector<float>.Build.Dense(3, 1f);

        public Vector<float> MiddlePoint { get; set; }

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

            //return translationMatrix.Multiply(rotationMatrixX).Multiply(rotationMatrixY)
            //    .Multiply(rotationMatrixZ).Multiply(scaleMatrix);

            return scaleMatrix.Multiply(rotationMatrixX).Multiply(rotationMatrixY).Multiply(rotationMatrixZ).Multiply(translationMatrix);


        }

        public static Model3D CreateCone(int n, float height, float radius, Color color)
        {
            Model3D cone = new Model3D() { Triangles = new List<Triangle3D>() };
            cone.Color = color;
            var builder = Vector<float>.Build;
            cone.MiddlePoint = builder.DenseOfArray(new float[] { 0f, 0f, height / 2, 1f });
            Vector<float> p0 = builder.DenseOfArray(new float[] { 0f, 0f, 0f, 1f }); //base center
            Vector<float> p1 = builder.DenseOfArray(new float[] { 0f, 0f, height, 1f }); //wierzcholek gorny
            Vector<float>[] basePoints = new Vector<float>[n];//cone base points
            for(int i = 0; i < n; i++)
            {
                double deg = Math.PI * (double)i * 2.0 / (double)n;
                basePoints[i] = builder.DenseOfArray(new float[] { radius * (float)Math.Cos(deg), radius * (float)Math.Sin(deg), 0, 1 });
            }
            //triangles:
            //parzyste - trojkaty podstawy, nieparzyste sciany boczne
            for(int i = 0; i < n - 1; i++)
            {
                var t1 = new Triangle3D(basePoints[i], basePoints[i + 1], p0);
                cone.Triangles.Add(t1);
                var t2 = new Triangle3D(basePoints[i], basePoints[i + 1], p1);
                cone.Triangles.Add(t2);
            }
            cone.Triangles.Add(new Triangle3D(basePoints[n - 1], basePoints[0], p0));
            cone.Triangles.Add(new Triangle3D(basePoints[n - 1], basePoints[0], p1));

            //Kazdy model ma jeden model, juz nie potrzeba w kazdym modelu
            ////Color:
            //for(int i = 0; i < cone.Triangles.Count; i++)
            //{
            //    cone.Triangles[i].Color = Color.FromArgb(random.Next(50, 200), random.Next(50, 200), random.Next(50, 200));
            //    //cone.Triangles[i].Color = Color.DarkGreen;
            //}

            //normal vectors:
            //parzyste - podstawa
            for(int i = 0; i + 1 < cone.Triangles.Count; i += 2)
            {
                cone.Triangles[i].NormalVectorA = builder.DenseOfArray(new float[] { 0, 0, -1, 1f }); //wektor w dol
                cone.Triangles[i].NormalVectorB = builder.DenseOfArray(new float[] { 0, 0, -1, 1f });
                cone.Triangles[i].NormalVectorC = builder.DenseOfArray(new float[] { 0, 0, -1, 1f });

                //do poprawy, teraz chyba jest ostroslup, chce stozek

                Vector<float> BAVector = cone.Triangles[i].B.Subtract(cone.Triangles[i].A).SubVector(0, 3);
                Vector<float> CAVector = cone.Triangles[i].C.Subtract(cone.Triangles[i].A).SubVector(0, 3);
                Vector<float> normalVectorSide = BAVector.CrossProduct(CAVector).Normalize(2);

                cone.Triangles[i + 1].NormalVectorA = normalVectorSide;
                cone.Triangles[i + 1].NormalVectorB = normalVectorSide.Clone();
                cone.Triangles[i + 1].NormalVectorC = normalVectorSide.Clone();

                //cone.Triangles[i + 1].NormalVector = builder.DenseOfArray(new float[] { -height, radius, })
            }

            return cone;
        }


        //Kazda sciana podzielona na n x m trojkatow
        public static Model3D CreateCuboid(int n, int m, float xLength, float yLength, float zLength, Color color)
        {
            Model3D cuboid = new Model3D() { Triangles = new List<Triangle3D>() };
            var builder = Vector<float>.Build;
            cuboid.Color = color;
            cuboid.MiddlePoint = builder.DenseOfArray(new float[] { xLength / 2, yLength / 2, 0f, 1f });

            Vector<float>[] points = new Vector<float>[8];

            
            
            points[0] = builder.DenseOfArray(new float[] { 0f, 0f, 0f, 1f });
            points[1] = builder.DenseOfArray(new float[] { xLength, 0f, 0f, 1f });
            points[2] = builder.DenseOfArray(new float[] { xLength, yLength, 0f, 1f });
            points[3] = builder.DenseOfArray(new float[] { 0f, yLength, 0f, 1f });

            points[4] = builder.DenseOfArray(new float[] { 0f, 0f, zLength, 1f });
            points[5] = builder.DenseOfArray(new float[] { xLength, 0f, zLength, 1f });
            points[6] = builder.DenseOfArray(new float[] { xLength, yLength, zLength, 1f });
            points[7] = builder.DenseOfArray(new float[] { 0f, yLength, zLength, 1f });


            Vector<float>[,] vectors = new Vector<float>[n + 1, m + 1];


            //plaszczyzna XY:
            float xJump = xLength / (float)n;
            float yJump = yLength / (float)m;
            float zJump = 0;
            for (int i = 0; i < n + 1; i++)
            {
                for(int j = 0; j < m + 1; j++)
                {
                    vectors[i, j] =  builder.DenseOfArray(new float[] { i * xJump, j * yJump });
                }
            }
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    Triangle3D t = new Triangle3D(
                         builder.DenseOfArray(new float[] { vectors[i, j][0], vectors[i, j][1], 0f, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i, j + 1][0], vectors[i, j + 1][1], 0f, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], 0f, 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, 0f, -1f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, 0f, -1f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, 0f, -1f, 1f });
                    cuboid.Triangles.Add(t);


                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { vectors[i, j][0], vectors[i, j][1], 0f, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j][0], vectors[i + 1, j][1], 0f, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], 0f, 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, 0f, -1f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, 0f, -1f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, 0f, -1f, 1f });
                    cuboid.Triangles.Add(t);

                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { vectors[i, j][0], vectors[i, j][1], zLength, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i, j + 1][0], vectors[i, j + 1][1], zLength, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], zLength, 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, 0f, 1f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, 0f, 1f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, 0f, 1f, 1f });
                    cuboid.Triangles.Add(t);

                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { vectors[i, j][0], vectors[i, j][1], zLength, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j][0], vectors[i + 1, j][1], zLength, 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], zLength, 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, 0f, 1f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, 0f, 1f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, 0f, 1f, 1f });
                    cuboid.Triangles.Add(t);
                }
            }

            //Plaszczyzna YZ:
            xJump = 0;
            yJump = yLength / (float)n;
            zJump = zLength / (float)m;

            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < m + 1; j++)
                {
                    vectors[i, j] = builder.DenseOfArray(new float[] { i * yJump, j * zJump });
                }
            }
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Triangle3D t = new Triangle3D(
                         builder.DenseOfArray(new float[] { 0f, vectors[i, j][0], vectors[i, j][1], 1f }),
                         builder.DenseOfArray(new float[] { 0f, vectors[i, j + 1][0], vectors[i, j + 1][1], 1f }),
                         builder.DenseOfArray(new float[] { 0f, vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { -1f, 0f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { -1f, 0f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { -1f, 0f, 0f, 1f });
                    cuboid.Triangles.Add(t);

                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { 0f, vectors[i, j][0], vectors[i, j][1], 1f }),
                         builder.DenseOfArray(new float[] { 0f, vectors[i + 1, j][0], vectors[i + 1, j][1], 1f }),
                         builder.DenseOfArray(new float[] { 0f, vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { -1f, 0f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { -1f, 0f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { -1f, 0f, 0f, 1f });
                    cuboid.Triangles.Add(t);


                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { xLength, vectors[i, j][0], vectors[i, j][1], 1f }),
                         builder.DenseOfArray(new float[] { xLength, vectors[i, j + 1][0], vectors[i, j + 1][1], 1f }),
                         builder.DenseOfArray(new float[] { xLength, vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 1f, 0f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 1f, 0f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 1f, 0f, 0f, 1f });
                    cuboid.Triangles.Add(t);

                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { xLength, vectors[i, j][0], vectors[i, j][1], 1f }),
                         builder.DenseOfArray(new float[] { xLength, vectors[i + 1, j][0], vectors[i + 1, j][1], 1f }),
                         builder.DenseOfArray(new float[] { xLength, vectors[i + 1, j + 1][0], vectors[i + 1, j + 1][1], 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 1f, 0f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 1f, 0f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 1f, 0f, 0f, 1f });
                    cuboid.Triangles.Add(t);
                }
            }

            //Plaszczyzna XZ:
            xJump = xLength / (float)n;
            yJump = 0;
            zJump = zLength / (float)m;

            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < m + 1; j++)
                {
                    vectors[i, j] = builder.DenseOfArray(new float[] { i * xJump, j * zJump });
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Triangle3D t = new Triangle3D(
                         builder.DenseOfArray(new float[] { vectors[i, j][0], 0f, vectors[i, j][1], 1f }),
                         builder.DenseOfArray(new float[] { vectors[i, j + 1][0], 0f, vectors[i, j + 1][1], 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], 0f, vectors[i + 1, j + 1][1], 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, -1f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, -1f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, -1f, 0f, 1f });
                    cuboid.Triangles.Add(t);

                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { vectors[i, j][0], 0f, vectors[i, j][1], 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j][0], 0f, vectors[i + 1, j][1], 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], 0f, vectors[i + 1, j + 1][1], 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, -1f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, -1f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, -1f, 0f, 1f });
                    cuboid.Triangles.Add(t);


                    t = new Triangle3D(
                          builder.DenseOfArray(new float[] { vectors[i, j][0], yLength, vectors[i, j][1], 1f }),
                          builder.DenseOfArray(new float[] { vectors[i, j + 1][0], yLength, vectors[i, j + 1][1], 1f }),
                          builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], yLength, vectors[i + 1, j + 1][1], 1f })
                          );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, 1f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, 1f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, 1f, 0f, 1f });
                    cuboid.Triangles.Add(t);

                    t = new Triangle3D(
                         builder.DenseOfArray(new float[] { vectors[i, j][0], yLength, vectors[i, j][1], 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j][0], yLength, vectors[i + 1, j][1], 1f }),
                         builder.DenseOfArray(new float[] { vectors[i + 1, j + 1][0], yLength, vectors[i + 1, j + 1][1], 1f })
                         );
                    t.NormalVectorA = builder.DenseOfArray(new float[] { 0f, 1f, 0f, 1f });
                    t.NormalVectorB = builder.DenseOfArray(new float[] { 0f, 1f, 0f, 1f });
                    t.NormalVectorC = builder.DenseOfArray(new float[] { 0f, 1f, 0f, 1f });
                    cuboid.Triangles.Add(t);
                }
            }

            return cuboid;
        }


        public static Model3D CreateSphere(int n, int m, float radius, Color color)
        {
            Model3D sphere = new Model3D() { Triangles = new List<Triangle3D>() };
            var builder = Vector<float>.Build;
            sphere.Color = color;
            sphere.MiddlePoint = builder.DenseOfArray(new float[] { 0f, 0f, 0f, 1f });
            Vector<float>[,] points = new Vector<float>[n + 1, m + 1];//points
            for (int i = 0; i < n + 1; i++)
            {
                float fi = (float)Math.PI * (float)i * 2.0f / (float)n;
                fi = (float)MathExtentions.DegreeToRadian((float)i / (float)n * 180.0f);
                for(int j = 0; j < m + 1; j++)
                {
                    float psi = 2.0f * (float)Math.PI * (float)j * 2.0f / (float)m;
                    psi = (float)MathExtentions.DegreeToRadian((float)j / (float)m * 360.0f);
                    float x = radius * (float)Math.Sin(psi) * (float)Math.Cos(fi);
                    float y = radius * (float)Math.Sin(psi) * (float)Math.Sin(fi);
                    float z = radius * (float)Math.Cos(psi);
                    points[i, j] = builder.DenseOfArray(new float[] { x, y, z, 1f });
                }
                //basePoints[i] = builder.DenseOfArray(new float[] { radius * (float)Math.Cos(deg), radius * (float)Math.Sin(deg), 0, 1 });
            }

            for(int i = 0; i < n + 1; i += 1)
            {
                for(int j = 0; j < m + 1; j +=1)
                {
                    var t1 = new Triangle3D(points[i, j], points[i % n, (j + 1) % (m + 1)], points[(i + 1) % (n + 1), j % m]);
                    t1.NormalVectorA = t1.A;
                    t1.NormalVectorB = t1.B;
                    t1.NormalVectorC = t1.C;
                    sphere.Triangles.Add(t1);

                     t1 = new Triangle3D(points[i, (j + 1) % (m + 1)], points[(i + 1) % (n + 1), j], points[(i + 1) % (n + 1), (j + 1) % (m + 1)]);
                    t1.NormalVectorA = t1.A;
                    t1.NormalVectorB = t1.B;
                    t1.NormalVectorC = t1.C;
                    sphere.Triangles.Add(t1);
                }
            }


            return sphere;
        }
    }
}
