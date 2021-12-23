using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    internal class SSBO
    {
        public readonly int Handle;

        public SSBO(IntPtr data, IntPtr size)
        {
            Handle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, Handle);
            GL.BufferData(BufferTarget.ShaderStorageBuffer, size, data, BufferUsageHint.DynamicCopy);
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
        }

        public IntPtr Get()
        {
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, Handle);
            var p = GL.MapBuffer(BufferTarget.ShaderStorageBuffer, BufferAccess.WriteOnly);
            GL.UnmapBuffer(BufferTarget.ShaderStorageBuffer);
            return p;
        }

        public void Bind()
        {
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, Handle);
        }
    }
}
