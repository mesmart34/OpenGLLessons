using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK;

namespace Task6
{
    internal class Model
    {
        private List<Vector3> _vertices;
        private List<Vector3> _colors;
        private List<int> _indices;
        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _scale;

        public Model(List<Vector3> vertices, List<int> indices)
        {
            _position = Vector3.Zero;
            _vertices = vertices;
            _indices = indices;
            _rotation = Vector3.Zero;
            _scale = Vector3.One;
            _colors = new List<Vector3>();
        }

        public void SetColors(List<Vector3> colors)
        {
            _colors = colors;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
        }

        public void SetRotation(Vector3 rotation)
        {
            _rotation = rotation;
        }

        public void Rotate(Vector3 _rotate)
        {
            _rotation += _rotate;
        }

        public void SetScale(Vector3 scale)
        {
            _scale = scale;
        }

        public void Draw(BeginMode mode)
        {
            GL.LoadIdentity();
            GL.Translate(_position);
            GL.Rotate(_rotation.X, new Vector3(1, 0, 0));
            GL.Rotate(_rotation.Y, new Vector3(0, 1, 0));
            GL.Rotate(_rotation.Z, new Vector3(0, 0, 1));
            GL.Scale(_scale);

            GL.Begin(mode);

            if (_indices.Count > 0)
            {

                foreach (var ind in _indices)
                {
                    if (_colors.Count != 0)
                        GL.Color3(_colors[ind]);
                    GL.Vertex3(_vertices[ind]);
                }
            }
            else
            {
                foreach (var v in _vertices)
                    GL.Vertex3(v);
            }

            GL.End();
        }

    }
}
