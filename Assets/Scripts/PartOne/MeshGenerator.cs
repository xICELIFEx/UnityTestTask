using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PartOne
{
    public class MeshGenerator : IMeshGenerator
    {
        private const string DEFAULT_MESH_NAME = "default_mesh_name";
        
        public Mesh GenerateMesh(int length, int height, float step, string name)
        {
            if (length <= 0 || height <= 0)
            {
                Debug.LogError("incorrect params length and height must greater then zero");
                return null;
            }
            Mesh mesh = new Mesh();

            List<Vector3> vertexes = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> normales = new List<Vector3>();
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    vertexes.Add(new Vector3(i*step, j*step, 0));
                    uvs.Add(new Vector2((float)i/(length - 1), (float)j/(height - 1)));
                    normales.Add(Vector3.back);
                }
            }
            mesh.vertices = vertexes.ToArray();
            mesh.normals = normales.ToArray();
            
            List<int> triangles = new List<int>();
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (j == 0)
                    {
                        
                        triangles.Add(i*height + j);
                        triangles.Add(i*height + j + 1);
                        triangles.Add((i+1)*height + j);
                    }
                    else
                    {
                        if (j < height - 1)
                        {
                            triangles.Add(i*height + j);
                            triangles.Add(i*height + j + 1);
                            triangles.Add((i+1)*height + j);
                        }
                        
                        triangles.Add(i*height + j);
                        triangles.Add((i+1)*height + j);
                        triangles.Add((i+1)*height + j - 1);
                    }
                }
            }
            mesh.triangles = triangles.ToArray();

            mesh.uv = uvs.ToArray();
            // mesh.uv = new Vector2[]
            // {
            //     Vector2.zero, Vector2.up, Vector2.right, Vector2.one, 
            // };
            
            mesh.name = String.IsNullOrEmpty(name) ? DEFAULT_MESH_NAME : name;

            return mesh;
        }
    }
}
