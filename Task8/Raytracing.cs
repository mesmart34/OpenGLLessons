using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Raytracing
    {
        public Scene _scene;
        private Bitmap _backgroundTexture;
        private Vector3 _cameraPostition;
        private float _fov = 45.0f;

        public Raytracing()
        {
            _scene = new Scene();
            _scene.AddLightSource(new Light(new Vector3(0, 10, 9), 1.0f));
            _backgroundTexture = new Bitmap("../../Background.jpg");
            _cameraPostition = new Vector3(0, 1, 9);
            _scene.AddShape(new Sphere(new Vector3(0, 1, 3), 0.8f, new Material(new Vector3(0.9f, 0.1f, 0.1f), Vector3.One, 1.0f)));
            _scene.AddShape(new Sphere(new Vector3(5, 5, -5), 5.0f, new Material(new Vector3(0.1f, 0.1f, 0.8f), Vector3.One, 1.0f)));
            _scene.AddShape(new Plane(new Vector3(0, 5, 0), new Material(Vector3.One * 0.7f, Vector3.One, 0.0f)));
        }

        public Image RenderToCanvas(Canvas canvas)
        {
            for (var x = 0; x < canvas.Width; x++)
            {
                for (var y = 0; y < canvas.Height; y++)
                {
                    var dirX = (float)((x + 0.5) - canvas.Width / 2);
                    var dirY = (float)(-(y + 0.5) + canvas.Height / 2);
                    var dirZ = (float)(-canvas.Height / (2.0f * Math.Tan(_fov / 2.0f)));
                    var dir = new Vector3(dirX, dirY, dirZ).Normalized();
                    var ray = new Ray(_cameraPostition, dir);

                    /*var color = Vector3.Zero;
                    for (var i = 0; i < 3; i++)
                    {
                        var hit = Trace(ray);
                        color += ray.Energy * Shade(ref hit, ref ray);
                        if (!MathHelper.Any(ray.Energy))
                            break;
                    }*/
                    canvas.PutPixel(x, y, CastRay(ray.Origin, ray.Direction));

                }
            }
            return canvas.GetImage();
        }

        //https://github.com/ssloy/tinyraytracer/blob/master/tinyraytracer.cpp
        //https://github.com/gaitavr/raytracing/blob/master/Assets/Shaders/Raytracing.compute
        private Vector3 CastRay(Vector3 origin, Vector3 direction, int depth = 0)
        {
            var bestHit = new RayHit();
            foreach (var shape in _scene.GetShapes())
            {
                shape.Intersects(origin, direction, ref bestHit);
            }
            if(bestHit.Distance == float.PositiveInfinity)
            {
                var theta = (float)((Math.Acos(direction.Y) / -Math.PI) + 0.5f);
                var phi = (float)((Math.Atan2(direction.X, -direction.Z) / -Math.PI * 0.5f) + 0.5f);
                theta = MathHelper.Saturate(theta) * _backgroundTexture.Width;
                phi = MathHelper.Saturate(phi) * _backgroundTexture.Height;
                var color = _backgroundTexture.GetPixel((int)(phi), _backgroundTexture.Height - (int)(theta) - 1);
                return new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
            }
            if (depth > 4)
            {
                return new Vector3(0.1f, 0.1f, 0.1f);
            }
            Vector3 reflectDir = MathHelper.Reflect(direction, bestHit.Normal).Normalized();
            Vector3 reflectOrig = Vector3.Dot(reflectDir, bestHit.Normal) < 0
                ? bestHit.Position - (1e-3f * bestHit.Normal) : bestHit.Position + (1e-3f * bestHit.Normal);
            Vector3 reflectColor = CastRay(reflectOrig, reflectDir, depth + 1);

            float diffuse_light_intensity = 0, specular_light_intensity = 0;
            foreach (var light in _scene.GetLights())
            {
                var lightDir = (light.Position - bestHit.Position).Normalized();
                diffuse_light_intensity += light.Intensity * Math.Max(0, Vector3.Dot(lightDir, bestHit.Normal));
                //specular_light_intensity += powf(std::max(0.f, -reflect(-light_dir, N) * dir), material.specular_exponent) * lights[i].intensity;
            }
            return bestHit.Color * diffuse_light_intensity;
        }

        /* private Vector3 Shade(ref RayHit hit, ref Ray ray)
         {
             if (hit.Distance < float.PositiveInfinity)
             {
                 ray.Origin = hit.Position + hit.Normal * 0.001f;
                 if(hit.Transparency == 1)
                 {

                 }
                 ray.Direction = MathHelper.Reflect(ray.Direction, hit.Normal);

                 ray.Energy *= hit.Specular;

                 var light = _scene.DirectionalLights;
                 var shadowRay = new Ray(hit.Position + hit.Normal * 0.001f, -1 * light.Position);
                 var shadowHit = Trace(shadowRay);
                 if (shadowHit.Distance != float.PositiveInfinity)
                 {
                     return new Vector3(0.0f, 0.0f, 0.0f);
                 }
                 return MathHelper.Saturate(Vector3.Dot(hit.Normal, light.Position) * -1) * light.Intensivity * hit.Albedo;
             }
             ray.Energy = Vector3.Zero;

             var theta = (float)((Math.Acos(ray.Direction.Y) / -Math.PI) + 0.5f);
             var phi = (float)((Math.Atan2(ray.Direction.X, -ray.Direction.Z) / -Math.PI * 0.5f) + 0.5f);
             theta = MathHelper.Saturate(theta) * _backgroundTexture.Width;
             phi = MathHelper.Saturate(phi) * _backgroundTexture.Height;
             var color = _backgroundTexture.GetPixel((int)(phi), _backgroundTexture.Height - (int)(theta) - 1);
             return new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
         }

         private RayHit Trace(Ray ray)
         {
             var bestHit = new RayHit();

             foreach (var shape in _scene.GetShapes())
             {
                 shape.Intersects(ray, ref bestHit);
             }
             return bestHit;
         }*/
    }
}
