using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt4_3DScene
{
    public class PlayerCamera : Camera
    {
        public Model3D PlayerToFollow { get; set; }

        public PlayerCamera(Vector<float> cameraPosition, Vector<float> cameraTarget, Vector<float> cameraUpVector, 
                            float fieldOfView = 45f, float nearPlaneDistance = 1f, float farPlaneDistance = 30f, float aspectRatio = 1f) 
                            : base(cameraPosition, cameraTarget, cameraUpVector, fieldOfView,
                                   nearPlaneDistance, farPlaneDistance, aspectRatio)
        {

        }

        public override Matrix<float> CreateLookAt()
        {
            Vector<float> CameraPosition = PlayerToFollow.Position.Clone();
            CameraPosition[0] -= 5f * (float)Math.Cos(MathExtentions.DegreeToRadian(PlayerToFollow.Rotation[2]));
            CameraPosition[1] -= 5f * (float)Math.Sin(MathExtentions.DegreeToRadian(PlayerToFollow.Rotation[2]));
            CameraPosition[2] += 4f;
            Vector<float> CameraTarget = PlayerToFollow.Position.Clone();
            CameraTarget[2] += 2f;
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
    }
}
