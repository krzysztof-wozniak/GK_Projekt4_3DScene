using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using GK_Projekt4_3DScene.Extentions;

namespace GK_Projekt4_3DScene
{
    public class Camera
    {
        /// <summary>
        /// Position of the camera in the world coordinates
        /// </summary>
        public Vector<float> CameraPosition { get; set; }
        
        /// <summary>
        /// Camera's target's world coordinates
        /// </summary>
        public Vector<float> CameraTarget { get; set; }

        /// <summary>
        /// Normal vector to the camera
        /// </summary>
        public Vector<float> CameraUpVector { get; set; }

        public float FieldOfView { get; set; }

        public float AspectRatio { get; set; }

        public float NearPlaneDistance { get; set; }

        public float FarPlaneDistance { get; set; }

        public Camera(Vector<float> cameraPosition, Vector<float> cameraTarget, Vector<float> cameraUpVector, float fieldOfView = 45f, float nearPlaneDistance = 1f,
                      float farPlaneDistance = 100f, float aspectRatio = 1f)
        {
            CameraPosition = cameraPosition;
            CameraTarget = cameraTarget;
            CameraUpVector = cameraUpVector;
            FieldOfView = fieldOfView;
            AspectRatio = aspectRatio;
            NearPlaneDistance = nearPlaneDistance;
            FarPlaneDistance = farPlaneDistance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return ViewMatrix</returns>
        public Matrix<float> CreateLookAt()
        {
            //TODO: zwraca ViewMatrix
            return Matrix<float>.Build.DenseOfArray(new float[,] { { (float)-0.164398987, (float)0.986393924, (float)-3.5173E-18, (float)1.12554E-16 },
                                                                                     { (float)-0.160014224, (float)-0.026669037, (float)0.986754382, (float)1.12513E-16 },
                                                                                     { (float)0.973328527, (float)0.162221421, (float)0.162221421, (float)-3.082207001 },
                                                                                     { 0, 0, 0, 1 } });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns ProjectionMatrix</returns>
        public Matrix<float> CreatePerspectiveFieldOfView()
        {

            float e = 1f / (float)Math.Tan(MathExtentions.DegreeToRadian((double)FieldOfView / 2.0));
            //TODO: zwraca ProjectionMatrix
            //return Matrix<float>.Build.DenseOfArray(new float[,] { {(float)2.414213562, 0, 0, 0 },
            //                                                        { 0, (float)2.414213562, 0, 0 },
            //                                                        { 0, 0, (float)-1.02020202, (float)-2.02020202 },
            //                                                        { 0, 0, (float)-1, 0 } });
            float m22 = -(FarPlaneDistance + NearPlaneDistance) / (FarPlaneDistance - NearPlaneDistance);
            float m23 = -(2f * FarPlaneDistance * NearPlaneDistance) / (FarPlaneDistance - NearPlaneDistance);
            return Matrix<float>.Build.DenseOfArray(new float[,] {  { e, 0, 0, 0 },
                                                                    { 0, e / AspectRatio, 0, 0 },
                                                                    { 0, 0, m22, m23 },
                                                                    { 0, 0, -1f, 0 } });

        }
    }
}
