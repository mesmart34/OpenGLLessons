using OpenTK;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using OpenTK.Input;

namespace Raytracing
{
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
        private Texture texture;
        private SSBO buffer;
        private Camera camera;
        private Vector2 lastMouseMove;
        private Vector2 mouseMove;

        public Raytracing() : base(800, 600)
        {
           //CursorVisible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            shader = new ComputeShader("../../cs.glsl");
            camera = new Camera(Vector3.Zero, (float)Width / Height);
            OnResize(e);
            /*texture = new Texture(Width, Height);
            texture.Unbind();*/

            /*var sphere = new Sphere();
            var sphPtr = Marshal.AllocHGlobal(Marshal.SizeOf(sphere));
            Marshal.StructureToPtr(sphere, sphPtr, false);
            buffer = new SSBO(sphPtr, (IntPtr)Sphere.GetSize());*/


        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            texture = new Texture(Width, Height);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            mouseMove.X += e.XDelta;
            mouseMove.Y += e.YDelta;
           
            Title = e.XDelta.ToString();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            camera.Rotation = camera.Rotation + new Vector3(-mouseMove.Y, mouseMove.X, 0) * 0.01f;
            mouseMove = Vector2.Zero;
            /* mouseMove.X = 0;
             mouseMove.Y = 0;*/
            KeyboardState state = OpenTK.Input.Keyboard.GetState();
            Vector3 move = new Vector3();
            if (state.IsKeyDown(Key.A))
            {
                move.X -= 0.1f;
            }
            if (state.IsKeyDown(Key.D))
            {
                move.X += 0.1f;
            }
            if (state.IsKeyDown(Key.S))
            {
                move.Z -= 0.1f;
            }
            if (state.IsKeyDown(Key.W))
            {
                move.Z += 0.1f;
            }
            if (state.IsKeyDown(Key.Q))
            {
                move.Y -= 0.1f;
            }
            if (state.IsKeyDown(Key.E))
            {
                move.Y += 0.1f;
            }


            var pos = new Vector3();
            pos.X = (float)(Math.Sin(MathHelper.DegreesToRadians(camera.Rotation.X))
                * Math.Cos(MathHelper.DegreesToRadians(camera.Rotation.Y)));

            pos.Y = (float)(Math.Sin(MathHelper.DegreesToRadians(camera.Rotation.X)));

            pos.Z = (float)(Math.Cos(MathHelper.DegreesToRadians(camera.Rotation.X)) 
                * Math.Cos(MathHelper.DegreesToRadians(camera.Rotation.Y)));


            camera.Position += move;

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
