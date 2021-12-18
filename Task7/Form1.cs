using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Task7
{
    public partial class Form1 : Form
    {
        private GLControl _glControl;
        private bool _loaded;
        private Shader _shader;
        private Vector3 _rotation;
        private Vector3 _position; 
        private Vector3 _scale;
        private Vector3 frustumCenter;
        private const float nearPlane = 1f;
        private const float verticalFoV = 72f * (float)Math.PI / 180f;
        private const float farPlane = 1000f;
        private Texture _texture;
        private Mesh _mesh;
        private int _elementBufferObject;
        private int _vertexBufferObject;
        private int _vertexArrayObject;



        public Form1()
        {
            InitializeComponent();
            _glControl = new GLControl();
            _glControl.Name = "gLControl";
            _glControl.Size = new System.Drawing.Size(panel1.Width, panel1.Height);
            _glControl.TabIndex = 0;
            _glControl.VSync = false;
            _glControl.AutoSize = true;
            _glControl.Load += new System.EventHandler(this.GLControl_Load);
            _glControl.Resize += new System.EventHandler(this.GLControl_Resize);
            _glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLControl_Paint);
            panel1.Controls.Add(_glControl);
            _rotation = Vector3.Zero;
            _scale = Vector3.One;
            _position = new Vector3(0, 0, -100);
            _mesh = ObjLoader.Load("../../Models/low-poly-fox-by-pixelmannen.obj");
            
        }

        private void GLControl_Load(object sender, EventArgs e)
        {
            _shader = new Shader("../../Shaders/vertex.hlsl", "../../Shaders/fragment.hlsl");
            //_shader.Use();
            _loaded = true;
            frustumCenter = new Vector3();
            GLControl_Resize(sender, e);
            var _vertices = _mesh.vertices.ToArray();
            var _indices = _mesh.vertexIndices.ToArray();

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            // The shaders have been modified to include the texture coordinates, check them out after finishing the OnLoad function.
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

            // Because there's now 5 floats between the start of the first vertex and the start of the second,
            // we modify the stride from 3 * sizeof(float) to 5 * sizeof(float).
            // This will now pass the new vertex array to the buffer.
            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            // Next, we also setup texture coordinates. It works in much the same way.
            // We add an offset of 3, since the texture coordinates comes after the position data.
            // We also change the amount of data to 2 because there's only 2 floats for texture coordinates.
            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture = Texture.LoadFromFile("../../Textures/texture.png");
        }

        private void GLControl_Resize(object sender, EventArgs e)
        {
            if (!_loaded)
            {
                return;
            }

            _glControl.MakeCurrent();
            GL.Viewport(0, 0, _glControl.Width, _glControl.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            float tangent = (float)Math.Tan(verticalFoV / 2f);
            float aspect = (float)_glControl.Width / _glControl.Height;

            float height = nearPlane * tangent;
            float width = height * aspect;

            GL.Frustum(-width, width, -height, height, nearPlane, farPlane);

            frustumCenter.Z = -(farPlane - nearPlane) / 2f - nearPlane;

            _glControl.Invalidate();
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            _glControl.MakeCurrent();
            GL.ClearColor(0f, 0f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // transformations
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Scale(_scale);
            GL.Translate(0, 0, _scale.X);

            GL.Translate(frustumCenter);
            //GL.Translate(mesh.boundingBox.center);

            GL.Rotate(_rotation.X, new Vector3(1, 0, 0));
            GL.Rotate(_rotation.Y, new Vector3(0, 1, 0));
            GL.Rotate(_rotation.Z, new Vector3(0, 0, 1));

            GL.EnableClientState(ArrayCap.VertexArray);
            _texture.Use((OpenTK.Graphics.TextureUnit)TextureUnit.Texture0);
            // draw mesh
            var texture = _mesh.textureVertices.ToArray();
            GL.VertexPointer(2, VertexPointerType.Float, 0, texture);

            
            GL.Vertex(3, VertexPointerType.Float, 0, meshVertices);

            var meshVertexIndices = _mesh.vertexIndices.ToArray();
            GL.DrawElements(BeginMode.Triangles, _mesh.vertexIndices.Count, DrawElementsType.UnsignedInt, meshVertexIndices);

           /* // draw bounding box
            var bbVertices = _mesh.boundingBox.vertices.ToArray();
            GL.VertexPointer(3, VertexPointerType.Float, 0, bbVertices);
            GL.Color4(BoundingBox.drawColor);

            var bbVertexIndices = _mesh.vertexIndices.ToArray();
            GL.DrawElements(PrimitiveType.Lines, _mesh.boundingBox.indices.Count, DrawElementsType.UnsignedInt, bbVertexIndices);*/

            // cleanup
            GL.DisableClientState(ArrayCap.VertexArray);

            // we're double-buffered
            _glControl.SwapBuffers();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var track = (TrackBar)sender;
            if (track.Tag.Equals("x"))
            {
                _rotation.X = track.Value;
            }
            else if (track.Tag.Equals("y"))
            {
                _rotation.Y = track.Value;
            }
            else if (track.Tag.Equals("z"))
            {
                _rotation.Z = track.Value;
            }
            _glControl.Invalidate();
        }
    }
}
