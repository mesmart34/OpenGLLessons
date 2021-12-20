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
        private Shader _shader;
        private Texture _texture;
        private Mesh _mesh;
        private Vector3 _rotation;
        private Vector3 _position; 
        private Vector3 _cameraPosition;
        private Vector3 _front = -Vector3.UnitZ;

        private Vector3 _up = Vector3.UnitY;

        private Vector3 _right = Vector3.UnitX;
        private Vector3 _scale;
        private int _elementBufferObject;
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _vertexTextures;
        private Matrix4 _perspective;

        private float[] _vertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, // top right
             0.5f, -0.5f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f  // top left
        };

        private uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

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
            _scale = Vector3.One * 1.0f;
            _position = new Vector3(0, 0, 0);
            _cameraPosition = new Vector3(0, 0, -10);


        }

        private void GLControl_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader("../../Shaders/vertex.hlsl", "../../Shaders/fragment.hlsl");
            _shader.Use();
            _texture = Texture.LoadFromFile("../../Textures/fox.png");
            _mesh = ObjLoader.Load("../../Models/fox.obj");
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);

            var vertices = _mesh.vertices.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float) * 3), vertices, BufferUsageHint.StaticDraw);
            
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 0, 0);

            var indices = _mesh.vertexIndices.ToArray();
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(uint)), indices, BufferUsageHint.StaticDraw);

            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            var uvs = _mesh.textureVertices.Select(v => new Vector2(v.X, v.Y)).ToArray();
            _vertexTextures = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexTextures);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(uvs.Length * sizeof(float) * 2), uvs, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
        }

        private void GLControl_Resize(object sender, EventArgs e)
        {
            _glControl.MakeCurrent();
            GL.Viewport(0, 0, _glControl.Width, _glControl.Height);

            _glControl.Invalidate();
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            _glControl.MakeCurrent();
            GL.ClearColor(0f, 0f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _shader.Use();
            Matrix4 view = Matrix4.LookAt(_cameraPosition, _cameraPosition + _front, _up);
            Matrix4.CreateTranslation(ref _cameraPosition, out view);
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)_glControl.Width / (float)_glControl.Height, 0.01f, 100.0f, out _perspective);
            Matrix4 model = Matrix4.Identity;
            model = model * Matrix4.CreateTranslation(_position);
            model = model * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(_rotation.X));
            model = model * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(_rotation.Y));
            model = model * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_rotation.Z));
            model = model * Matrix4.CreateScale(_scale);
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("projection", _perspective);
            _shader.SetMatrix4("view", view);

            _texture.Use(OpenTK.Graphics.TextureUnit.Texture0);

            GL.BindVertexArray(_vertexArrayObject);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.DrawElements(BeginMode.Triangles, _mesh.vertexIndices.Count, DrawElementsType.UnsignedInt, 0);

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
