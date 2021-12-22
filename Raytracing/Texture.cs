using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public class Texture
    {
        public readonly int Handle;
        public int Width, Height;

        public Texture(int width, int height)
        {
            Width = width;
            Height = height;
            /*if (Handle != 0)
            {
                GL.DeleteTexture(Handle);
                Handle = 0;
            }*/
            Handle = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba32f, Width, Height, 0, PixelFormat.Rgba, PixelType.Float, (IntPtr)0);

            GL.BindImageTexture(0, Handle, 0, false, 0, TextureAccess.WriteOnly, SizedInternalFormat.Rgba32f);
        }

        public void Bind()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

    }
}
