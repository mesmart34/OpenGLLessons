using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
	public class Mesh
	{
		public readonly List<Vector4> vertices;
		public readonly List<Vector3> textureVertices;
		public readonly List<Vector3> normals;
		public readonly List<uint> vertexIndices;
		public readonly List<uint> textureIndices;
		public readonly List<uint> normalIndices;

		public Mesh(List<Vector4> vertices, List<Vector3> textureVertices, List<Vector3> normals,
					List<uint> vertexIndices, List<uint> textureIndices, List<uint> normalIndices)
		{
			this.vertices = vertices;
			this.textureVertices = textureVertices;
			this.normals = normals;
			this.vertexIndices = vertexIndices;
			this.textureIndices = textureIndices;
			this.normalIndices = normalIndices;

		}
	}
}
