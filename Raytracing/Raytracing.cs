using OpenTK;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using OpenTK.Input;

namespace Raytracing
{
    public struct DirectionalLight
    {
        public Vector3 Position;
        public float Intensivity;

        public static int GetSize()
        {
            return sizeof(float) * 4;
        }
    }

    public struct Sphere
    {
        Vector4 Position;
        float Radius;

        public static int GetSize()
        {
            return sizeof(float) * 5;
        }
    }

    public class Raytracing : GameWindow
    {
        private ComputeShader shader;
        private CSTexture texture;
        private Texture skybox;
        private SSBO buffer;
        private Camera camera;
        private Vector2 lastMouseMove;
        private Vector2 mouseMove;
        private Vector2 delta;
        private bool _firstMove = true;

        public Raytracing() : base(1024, 768)
        {
           //CursorVisible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            shader = new ComputeShader("../../cs.glsl");
            camera = new Camera(Vector3.Zero, (float)Width / Height);
            OnResize(e);
            skybox = Texture.LoadFromFile("../../skybox.png");
            /*texture = new Texture(Width, Height);
            texture.Unbind();*/

            var light = new DirectionalLight();
            light.Intensivity = 1.0f;
            light.Position = new Vector3(0.1f, 0.5f, -0.5f);
            var lightPtr = Marshal.AllocHGlobal(Marshal.SizeOf(light));
            Marshal.StructureToPtr(light, lightPtr, false);
            buffer = new SSBO(lightPtr, (IntPtr)DirectionalLight.GetSize());
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            texture = new CSTexture(Width, Height);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            
            delta = mouseMove - lastMouseMove;
            mouseMove.X = Mouse.XDelta;
            mouseMove.Y = Mouse.YDelta;
            lastMouseMove = mouseMove;
           /* delta.X /= (float)Math.PI * 180.0f;
            delta.Y /= (float)Math.PI * 180.0f;*/
            camera.Rotation = camera.Rotation + new Vector3(delta.X, delta.Y, 0) * 0.5f; 
            mouseMove = Vector2.Zero;
            /* mouseMove.X = 0;
             mouseMove.Y = 0;*/
            KeyboardState state = OpenTK.Input.Keyboard.GetState();
            Vector3 move = new Vector3();
            float speedX = 0.0f;
            float speedY = 0.0f;
            if (state.IsKeyDown(Key.A))
            {
                speedY = -1.0f;
            }
            if (state.IsKeyDown(Key.D))
            {
                speedY = 1.0f;
            }
            if (state.IsKeyDown(Key.S))
            {
                speedX = -1.0f;
            }
            if (state.IsKeyDown(Key.W))
            {
                speedX = 1.0f;
            }
            if (state.IsKeyDown(Key.Q))
            {
                move.Y  = -0.1f;
            }
            if (state.IsKeyDown(Key.E))
            {
                move.Y = 0.1f;
            }
            var yaw = MathHelper.DegreesToRadians(camera.Rotation.X);
            var pitch = MathHelper.DegreesToRadians(camera.Rotation.Y);
            if (pitch > Math.PI / 2)
                pitch = (float)(Math.PI / 2);
            if (pitch < -Math.PI / 2)
                pitch = (float)(-Math.PI / 2);
            
            //camera.Position += move * speed;
            var direction = new Vector3();
            direction.X = (float)Math.Cos(yaw) * (float)Math.Cos(pitch);
            direction.Y = (float)Math.Sin(pitch);
            direction.Z = (float)Math.Sin(yaw) * (float)Math.Cos(pitch);
            //camera.CameraFront = direction.Normalized();
            Title = camera.CameraFront.ToString();

            camera.Position += new Vector3(0, move.Y, 0);
            camera.Position -= camera.CameraFront * speedX * 0.1f;
            camera.Position -= Vector3.Cross(camera.CameraFront, camera.CameraUp) * speedY * 0.1f;
            //camera.Position += new Vector3(cameraFront.Z, cameraFront.Y, cameraFront.X) * speedY * 0.1f;

        }

        public void RenderQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex3(-1f, 1f, 0);
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(1f, 1f, 0);
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(1f, -1f, 0);
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(-1f, -1f, 0);
            GL.End();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            var error = GL.GetError();
            shader.Bind();
            //buffer.Bind()
            skybox.Use(OpenTK.Graphics.TextureUnit.Texture1);
            shader.LoadInt("skybox", 1);
            shader.LoadMatrix4("projection", camera.GetProjectionMatrix());
            shader.LoadMatrix4("view", camera.GetViewMatrix());
            
            shader.Execute(Width / 8, Height / 8, 1);
            shader.Unbind();


            texture.Bind();
            RenderQuad();
            texture.Unbind();

           
            SwapBuffers();

        }
    }
}
