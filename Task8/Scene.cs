using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Scene
    {
        private List<IShape> _shapes;
        public List<Light> _lights;
        public Vector3 BackgroundColor;

        public Scene()
        {
            _shapes = new List<IShape>();
            _lights = new List<Light>();
            BackgroundColor = new Vector3(0.25f, 0.52f, 0.95f);
        }

        public void AddShape(IShape shape)
        {
            _shapes.Add(shape);
        }

        public void AddLightSource(Light light)
        {
            _lights.Add(light);
        }

        public IEnumerable<IShape> GetShapes()
        {
            foreach(var shape in _shapes)
            {
                yield return shape;
            }
        }

        public IEnumerable<Light> GetLights()
        {
            foreach (var light in _lights)
            {
                yield return light;
            }
        }
    }
}
