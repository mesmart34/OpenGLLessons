using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
	public class Mesh
	{
		public readonly List<Vector3> vertices;
		public readonly List<Vector2> textureVertices;
		public readonly List<Vector3> normals;
		public readonly List<uint> indices;

		private int vaoID = 0;
		private int verticesID;
		private int textureID;
		private int normalsID;
		private int indicesID;

		public Mesh(List<Vector3> vertices, List<Vector2> textureVertices, List<Vector3> normals,
					List<uint> indices)
		{
			this.vertices = vertices;
			this.textureVertices = textureVertices;
			this.normals = normals;
			this.indices = indices;

			Init();

		}

		private void Init()
        {
			vaoID = GL.GenVertexArray();
			GL.BindVertexArray(vaoID);

			verticesID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, verticesID);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Count * sizeof(float) * 3), vertices.ToArray(), BufferUsageHint.StaticDraw);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

			textureID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, textureID);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(textureVertices.Count * sizeof(float) * 2), textureVertices.ToArray(), BufferUsageHint.StaticDraw);
			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

			normalsID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, normalsID);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(normals.Count * sizeof(float) * 3), normals.ToArray(), BufferUsageHint.StaticDraw);
			GL.EnableVertexAttribArray(2);
			GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

			indicesID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indicesID);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Count * sizeof(uint)), indices.ToArray(), BufferUsageHint.StaticDraw);

			/*GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);*/
			GL.BindVertexArray(0);
		}


		public void Bind()
        {
			GL.BindVertexArray(vaoID);
			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);
        }

		public void Unbind()
        {
			GL.BindVertexArray(0);
			GL.DisableVertexAttribArray(0);
			GL.DisableVertexAttribArray(1);
			GL.DisableVertexAttribArray(2);
		}
	}
}
