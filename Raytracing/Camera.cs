using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public class Camera
    {
      
        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            Rotation = Vector3.Zero;
            AspectRatio = aspectRatio;
            Fov = 60.0f;
        }

        public Vector3 Position { get; set; }
        public Vector3 Rotation = new Vector3(-90.0f, 0.0f, 0.0f);
        public Vector3 CameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        public Vector3 CameraUp = new Vector3(0.0f, 1.0f, 0.0f);
        public float Fov { get; set; }
        public float AspectRatio { private get; set; }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + CameraFront, CameraUp);
        }

        public Matrix4 GetProjectionMatrix()
        {
            var proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), AspectRatio, 0.01f, 100f);
            return proj;
        }

    }
}
