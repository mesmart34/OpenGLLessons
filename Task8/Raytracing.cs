using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;

namespace Task8
{
    internal class Raytracing
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private List<IShape> _shapes;
        private float fieldOfView = 45.0f;
        private Vector3 _cameraPos;
        private readonly Vector3 BackgroundColor = new Vector3(50, 50, 50);
        private Vector3 _light;
        private float _lightIntense = 1.0f;

        public Raytracing(int width, int height, List<IShape> shapes)
        {
            _cameraPos = new Vector3();
            _shapes = shapes;
            Width = width;
            Height = height;
            _light = new Vector3(-0.5f, 0.5f, -0.5f);
        }

        public uint[,] RenderToPlane()
        {
            var data = new uint[Width, Height];
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var dirX = (x + 0.5f) - Width / 2;
                    var dirY = -(y + 0.5f) + Height / 2;
                    var dirZ = -Height / (2.0f * (float)Math.Tan(fieldOfView / 2));
                    var dir = new Vector3(dirX, dirY, dirZ).Normalized();
                    var color = CastRay(_cameraPos, dir);
                    data[x, y] = color;
                }
            }
            return data;
        }

        private bool NonZero(Vector3 v)
        {
            return v.X != 0 && v.Y != 0 && v.Z != 0;
        }

        public uint CastRay(Vector3 origin, Vector3 direction)
        {
            var color = new Vector3(0, 0, 0);

            var ray = Ray.Create(origin, direction);
            for (var i = 0; i < 1; i++)
            {
                RayHit hit = Trace(ref ray);
                color += ray.Energy * Shade(ref ray, ref hit);
                if (!NonZero(ray.Energy))
                    break;
            }

            var r = (byte)color.X * 255;
            var g = (byte)color.Y * 255;
            var b = (byte)color.Z * 255;
            var _color = (uint)(r << 24 | g << 16 | b << 8 | 1);
            return _color;
        }

        Vector3 Reflect(Vector3 n, Vector3 i)
        {
            return i - 2 * n * Vector3.Dot(i, n);
        }

        Vector3 Shade(ref Ray ray, ref RayHit hit)
        {
            if (hit.Distance < float.MaxValue)
            {
                // Reflect the ray and multiply energy with specular reflection
                /*ray.Origin = hit.Position + hit.Normal * 0.001f;
                ray.Direction = Reflect(ray.Direction, hit.Normal);
                ray.Energy *= hit.Specular;*/
                return hit.Albedo;
            }
            ray.Energy = Vector3.Zero;
            return new Vector3(0.1f, 0.1f, 0.1f);
            /*if (hit.Distance < float.MaxValue)
        {

            ray.Origin = hit.Position + hit.Normal * 0.001f;
            ray.Direction = Reflect(ray.Direction, hit.Normal);
            ray.Energy *= hit.Specular;

            *//*bool shadow = false;
            Ray shadowRay = Ray.Create(hit.Position + hit.Normal * 0.001f, -1 * _light);
            RayHit shadowHit = Trace(ref shadowRay);
            if (shadowHit.Distance != float.MaxValue)
            {
                return new Vector3(0.0f, 0.0f, 0.0f);
            }*//*
            return Saturate(Vector3.Dot(hit.Normal, _light) * -1) * _lightIntense * hit.Albedo;

        }
        ray.Energy = Vector3.Zero;
        return new Vector3(0.80f, 0.68f, 1.0f);*/
        }

        public float Saturate(float v)
        {
            if (v < 0)
                return 0;
            if (v >= 1.0f)
                return 1.0f;
            return v;
        }

        public RayHit Trace(ref Ray ray)
        {
            var rayHit = RayHit.Create();

            foreach (var shape in _shapes)
            {
                if (shape.Intersects(ray, ref rayHit))
                {
                    // Console.WriteLine();
                    var a = 0;
                }
            }

            return rayHit;
        }
    }
}
