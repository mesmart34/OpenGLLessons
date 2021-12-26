using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace Task8
{
    public class ComputeShader
    {
        public readonly int Handle;
        
        public ComputeShader(string path)
        {
            var shader = GL.CreateShader(ShaderType.ComputeShader);
            var shaderSource = File.ReadAllText(path);
            GL.ShaderSource(shader, shaderSource);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, shader);
            GL.LinkProgram(Handle);
        }

        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public void Execute(int x, int y, int z)
        {
            GL.DispatchCompute(x, y, z);
            GL.MemoryBarrier(MemoryBarrierFlags.ShaderImageAccessBarrierBit);
        }

        public void LoadMatrix4(string name, Matrix4 value)
        {
            GL.UniformMatrix4(GL.GetUniformLocation(Handle, name), false, ref value);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public void LoadInt(string name, int value)
        {
            GL.Uniform1(GL.GetUniformLocation(Handle, name), value);
        }
    }
}
