using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
	public static class ModelLoader
	{
		public static Mesh Load(string path)
		{
			List<Vector3> vertices = new List<Vector3>();
			List<Vector2> textureVertices = new List<Vector2>();
			List<Vector3> normals = new List<Vector3>();
			List<uint> indices = new List<uint>();

			Assimp.Scene model;
			Assimp.AssimpContext importer = new Assimp.AssimpContext();
			importer.SetConfig(new Assimp.Configs.NormalSmoothingAngleConfig(66.0f));
			model = importer.ImportFile(path, Assimp.PostProcessPreset.TargetRealTimeMaximumQuality);

			var mesh = model.Meshes[0];
			for (var v = 0; v < mesh.VertexCount; v++)
			{
				var vertex = new Vector3(mesh.Vertices[v].X, mesh.Vertices[v].Y, mesh.Vertices[v].Z);
				vertices.Add(vertex);

				if (mesh.HasNormals)
				{
					var normal = new Vector3(mesh.Normals[v].X, mesh.Normals[v].Y, mesh.Normals[v].Z);
					normals.Add(normal);
				}

				if(mesh.HasTextureCoords(0))
                {
					var uv = new Vector2(mesh.TextureCoordinateChannels[0][v].X, mesh.TextureCoordinateChannels[0][v].Y);
					textureVertices.Add(uv);
				}
			}

			for(var f = 0; f < mesh.FaceCount; f++)
            {
				indices.Add((uint)mesh.Faces[f].Indices[0]);
				indices.Add((uint)mesh.Faces[f].Indices[1]);
				indices.Add((uint)mesh.Faces[f].Indices[2]);
            }

			return new Mesh(vertices, textureVertices, normals, indices);
		}
	}
}
