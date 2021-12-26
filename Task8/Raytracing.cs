using OpenTK;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using OpenTK.Input;

namespace Task8
{

    public class Raytracing : GameWindow
    {
        private ComputeShader shader;
        private CSTexture texture;
        private Camera camera;
        private Vector2 lastMouseMove;
        private Vector2 mouseMove;
        private Vector2 delta;

        public Raytracing() : base()
        {
            WindowState = WindowState.Maximized;
            CursorVisible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            shader = new ComputeShader("../../cs.glsl");
            camera = new Camera(Vector3.Zero, (float)Width / Height);
            camera.Position = new Vector3(0, 0, -0.3f);
            OnResize(e);
            CursorGrabbed = true;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            texture = new CSTexture(Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            
          
            const float cameraSpeed = 1.5f;
            var input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (input.IsKeyDown(Key.W))
            {
                camera.Position += camera.Front * cameraSpeed * (float)e.Time; 
            }

            if (input.IsKeyDown(Key.S))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Key.A))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)e.Time; 
            }
            if (input.IsKeyDown(Key.D))
            {
                camera.Position += camera.Right * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Key.Space))
            {
                camera.Position += camera.Up * cameraSpeed * (float)e.Time; 
            }
            if (input.IsKeyDown(Key.LShift))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)e.Time; 
            }
            var mouseState = Mouse.GetState();
            mouseMove.X = mouseState.X;
            mouseMove.Y = mouseState.Y;
            delta = mouseMove - lastMouseMove;
            lastMouseMove = mouseMove;
            const float sensitivity = 0.2f;
            delta *= sensitivity;
           
            camera.Yaw += delta.X;
            camera.Pitch -= delta.Y;
           
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
