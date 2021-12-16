using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Task8
{
    public struct Sphere
    {
        public Vector3 position;
        public float radius;
        public Vector3 albedo;
        public Vector3 specular;
        public float speed;
        public float amplitude;
    };

    public struct Ray
    {
        public Vector3 origin;
        public Vector3 direction;
        public Vector3 energy;
    };

    public struct RayHit
    {
        public Vector3 position;
        public float distance;
        public Vector3 normal;
        public Vector3 albedo;
        public Vector3 specular;
    };

    public partial class Form1 : Form
    {
        private List<Sphere> spheres;
        private Canvas canvas;
        private Vector3 light;
        private Vector3 backgroundColor;
        private Vector3 groundColor;
        private const float minBright = 0.1f;

        public Form1()
        {
            InitializeComponent();
            light = new Vector3(90, 90, 0);
            backgroundColor = new Vector3(0.25f, 0.52f, 0.95f);
            groundColor = new Vector3(0.5f, 0.5f, 0.5f);
            spheres = new List<Sphere>();
            var s = new Sphere();
            s.albedo = Vector3.One * 0.5f;
            s.radius = 1.0f;
            s.position = new Vector3(0, 0, -5);
            spheres.Add(s);
            s.position = new Vector3(-4, 0, -5);
            spheres.Add(s);
            canvas = new Canvas(pictureBox.Width, pictureBox.Height);
            for (var x = 0; x < canvas.Width; x++)
            {
                for (var y = 0; y < canvas.Height; y++)
                {
                    var dirX = (float)((x + 0.5) - canvas.Width / 2);
                    var dirY = (float)(-(y + 0.5) + canvas.Height / 2);
                    var dirZ = (float)(-canvas.Height / (2.0f * Math.Tan(90.0 / 2)));
                    var dir = new Vector3(dirX, dirY, dirZ).Normalized();
                    var ray = CreateRay(new Vector3(0, 1, 0), dir);

                    var color = Vector3.Zero;
                    for (int i = 0; i < 3; i++)
                    {
                        var hit = Trace(ray);
                        color += ray.energy * Shade(ref hit, ref ray);
                        if (!Any(ray.energy))
                            break;
                    }
                    canvas.PutPixel(x, y, color);

                }
            }
            pictureBox.Image = canvas.GetImage();
        }

        bool Any(Vector3 v)
        {
            return v.X != 0 || v.Y != 0 || v.Z != 0;
        }

        Vector3 Shade(ref RayHit hit, ref Ray ray)
        {
            if (hit.distance < float.PositiveInfinity)
            {
                ray.origin = hit.position + hit.normal * 0.001f;
                ray.direction = Reflect(ray.direction, hit.normal);
                ray.energy *= hit.specular;

                var shadowRay = CreateRay(hit.position + hit.normal * 0.001f, -1 * light);
                var shadowHit = Trace(shadowRay);
                if (shadowHit.distance != float.PositiveInfinity)
                {
                    return new Vector3(0.0f, 0.0f, 0.0f);
                }

                return Saturate(Vector3.Dot(hit.normal, light) * -1) * 1.0f * hit.albedo;
            }
            ray.energy = Vector3.Zero;
            return backgroundColor;
        }

        private float Saturate(float value)
        {
            if (value >= 1.0f)
                return 1.0f;
            if (value <= minBright)
                return minBright;
            return value;
        }

        private Vector3 Reflect(Vector3 i, Vector3 n)
        {
            return i - 2 * n * Vector3.Dot(i, n);
        }

        RayHit Trace(Ray ray)
        {
            RayHit bestHit = CreateRayHit();

            // Trace ground plane
            IntersectGroundPlane(ray, ref bestHit);

            // Trace spheres
            foreach (var sphere in spheres)
            {
                IntersectSphere(ray, ref bestHit, sphere);
            }
            return bestHit;
        }

        void IntersectSphere(Ray ray, ref RayHit bestHit, Sphere sphere)
        {
            // Calculate distance along the ray where the sphere is intersected
            var pos = sphere.position;
            var d = ray.origin - pos;// sphere.position;
            float p1 = -Vector3.Dot(ray.direction, d);
            float p2sqr = p1 * p1 - Vector3.Dot(d, d) + sphere.radius * sphere.radius;
            if (p2sqr < 0)
            {
                return;
            }
            float p2 = (float)Math.Sqrt(p2sqr);
            float t = p1 - p2;
            if (t <= 0)
            {
                t = p1 + p2;
            }
            if (t > 0 && t < bestHit.distance)
            {
                bestHit.distance = t;
                bestHit.position = ray.origin + t * ray.direction;
                bestHit.normal = Vector3.Normalize(bestHit.position - pos);
                bestHit.albedo = sphere.albedo;
                bestHit.specular = sphere.specular;
            }
        }


        void IntersectGroundPlane(Ray ray, ref RayHit bestHit)
        {
            // Calculate distance along the ray where the ground plane is intersected
            float t = -ray.origin.Y / ray.direction.Y;
            if (t > 0 && t < bestHit.distance)
            {
                bestHit.distance = t;
                bestHit.position = ray.origin + t * ray.direction;
                bestHit.normal = new Vector3(0.0f, 1.0f, 0.0f);
                bestHit.albedo = groundColor;
                bestHit.specular = Vector3.One * 0.1f;
            }
        }

        static Ray CreateRay(Vector3 origin, Vector3 direction)
        {
            Ray ray;
            ray.origin = origin;
            ray.direction = direction;
            ray.energy = new Vector3(1.0f, 1.0f, 1.0f);
            return ray;
        }

        static RayHit CreateRayHit()
        {
            RayHit hit;
            hit.position = new Vector3(0.0f, 0.0f, 0.0f);
            hit.distance = float.PositiveInfinity;
            hit.normal = new Vector3(0.0f, 0.0f, 0.0f);
            hit.albedo = new Vector3(0.0f, 0.0f, 0.0f);
            hit.specular = new Vector3(0.0f, 0.0f, 0.0f);
            return hit;
        }

    }
}
