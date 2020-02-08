using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using GK_Projekt4_3DScene;

namespace GK_Projekt4_3DScene
{
    public class Camera
    {
        /// <summary>
        /// Position of the camera in the world coordinates
        /// </summary>
        public Vector<float> CameraPosition { get; set; }
        
        /// <summary>
        /// Camera target's world coordinates
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
                      float farPlaneDistance = 30f, float aspectRatio = 1f)
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
        /// <returns>Returns View Matrix</returns>
        public Matrix<float> CreateLookAt()
        {
            Vector<float> zAxis = (CameraPosition - CameraTarget).Normalize(2);

            Vector<float> xAxis = CameraUpVector.CrossProduct(zAxis).Normalize(2);

            Vector<float> yAxis = zAxis.CrossProduct(xAxis);

            var viewMatrixInversed = Matrix<float>.Build.DenseOfArray(new float[,]
            {
                {xAxis[0], yAxis[0], zAxis[0], CameraPosition[0]},
                {xAxis[1], yAxis[1], zAxis[1], CameraPosition[1]},
                {xAxis[2], yAxis[2], zAxis[2], CameraPosition[2]},
                {0f,       0f,       0f,       1f }
            });
            return viewMatrixInversed.Inverse();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns Projection Matrix</returns>
        public Matrix<float> CreatePerspectiveFieldOfView()
        {
            float e = 1f / (float)Math.Tan(MathExtentions.DegreeToRadian((double)FieldOfView / 2.0));
            float m22 = -(FarPlaneDistance + NearPlaneDistance) / (FarPlaneDistance - NearPlaneDistance);
            float m23 = -(2f * FarPlaneDistance * NearPlaneDistance) / (FarPlaneDistance - NearPlaneDistance);

            return Matrix<float>.Build.DenseOfArray(new float[,] {  { e, 0, 0, 0 },
                                                                    { 0, e / AspectRatio, 0, 0 },
                                                                    { 0, 0, m22, m23 },
                                                                    { 0, 0, -1f, 0 } });
        }
    }
}
