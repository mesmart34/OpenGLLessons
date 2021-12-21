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
        private Vector3 _rotation;
        private Vector3 _position; 
        private Vector3 _scale;
        private Texture _texture;
        private Texture _grassTexture;
        private Texture _treeTexture;
        private Mesh _mesh;
        private Mesh _ground;
        private Mesh _tree;
        private Camera _camera;



        public Form1()
        {
            InitializeComponent();
            _glControl = new GLControl();
            _glControl.Name = "gLControl";
            _glControl.Size = new Size(panel1.Width, panel1.Height);
            _glControl.TabIndex = 0;
            _glControl.VSync = false;
            _glControl.AutoSize = true;
            _glControl.Load += new EventHandler(GLControl_Load);
            _glControl.Resize += new EventHandler(GLControl_Resize);
            _glControl.Paint += new PaintEventHandler(GLControl_Paint);
            panel1.Controls.Add(_glControl);
            _rotation = Vector3.Zero;
            _scale = Vector3.One;
            _position = new Vector3(0, 0, -5);
            
            
        }

        private void GLControl_Load(object sender, EventArgs e)
        {
            GLControl_Resize(sender, e);
            GL.Enable(EnableCap.DepthTest);
            _shader = new Shader("../../Shaders/vertex.hlsl", "../../Shaders/fragment.hlsl");
            _mesh = ModelLoader.Load("../../Models/house.obj");
            _tree = ModelLoader.Load("../../Models/tree.obj");
            _ground = ModelLoader.Load("../../Models/ground.obj");
            _texture = Texture.LoadFromFile("../../Textures/house.jpg");
            _grassTexture = Texture.LoadFromFile("../../Textures/grass.jpg");
            _treeTexture = Texture.LoadFromFile("../../Textures/Wood.jpg");
            _camera = new Camera(new Vector3(0, 0, 30), (float)_glControl.Width / _glControl.Height);
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
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            RenderMesh(_mesh, _texture, new Vector3(0, 0, 0), Vector3.One);
            RenderMesh(_ground, _grassTexture, new Vector3(0, -0.5f, 0), Vector3.One * 2 );
            RenderMesh(_tree, _treeTexture, new Vector3(-8, 0, 0), Vector3.One * 2);
           
            _glControl.SwapBuffers();
        }

        private void RenderMesh(Mesh mesh, Texture texture, Vector3 position, Vector3 scale)
        {
            mesh.Bind();
            _shader.Use();
            texture.Use(0);
            var projection = _camera.GetProjectionMatrix();
            var view = _camera.GetViewMatrix();
            var model = Matrix4.Identity;
            model *= Matrix4.CreateTranslation(position);
            model *= Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_rotation.X));
            model *= Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_rotation.Y));
            model *= Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(_rotation.Z));
            model *= Matrix4.Scale(scale);
            _shader.SetMatrix4("projection", projection);
            _shader.SetMatrix4("view", view);
            _shader.SetMatrix4("model", model);

            GL.DrawElements(BeginMode.Triangles, _mesh.indices.Count, DrawElementsType.UnsignedInt, 0);
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
