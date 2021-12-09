using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace Task8
{
    internal class Raytracing : GameWindow
    {
        private List<IShape> _shapes;
        private float fieldOfView = 60.0f;
        private Vector3 _cameraPos;
        private uint[] _data;
        private readonly Vector3 BackgroundColor = new Vector3(50, 50, 50);

        public Raytracing() : base(800, 600, GraphicsMode.Default, "Raytracing")
        {
            _shapes = new List<IShape>();
            _cameraPos = new Vector3();
            _data = new uint[Width * Height];
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var sphere = new Sphere();
            sphere.Position = new Vector3(0, 0, -4);
            sphere.Radius = 5.0f;
            sphere.Color = new Vector3(255, 255, 255);
            _shapes.Add(sphere);
            var plane = new Plane();
            plane.Size = 10;
            _shapes.Add(plane);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            RenderToPlane(_data);
            GL.DrawPixels(Width, Height, PixelFormat.Rgba, PixelType.UnsignedInt8888, _data);

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            _data = new uint[Width * Height];
            base.OnResize(e);
        }

        public void RenderToPlane(uint[] data)
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var dirX = (x + 0.5f) - Width / 2;
                    var dirY = -(y + 0.5f) + Height / 2;
                    var dirZ = -Height / (2.0f * (float)Math.Tan(fieldOfView / 2));
                    var dir = new Vector3(dirX, dirY, dirZ).Normalized();
                    var color = CastRay(_cameraPos, dir);
                    data[x + y * Width] = color;
                }
            }
        }

        public uint CastRay(Vector3 start, Vector3 direction)
        {
            var color = BackgroundColor;
            var hitDistance = float.MaxValue;
            foreach(var shape in _shapes)
            {
                RayHit hit;
                if(shape.Intersects(start, direction, out hit))
                {
                    color = hit.Color;
                    hitDistance = hit.Distance;
                }
            }
            var r = (byte)color.X;
            var g = (byte)color.Y;
            var b = (byte)color.Z;
            var _color = (uint)(r << 24 | g << 16 | b << 8 | 1);
            return _color;
        }
    }
}
