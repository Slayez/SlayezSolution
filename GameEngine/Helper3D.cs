using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static GameEngine.Utilites;

namespace GameEngine
{
    public static class Helper3D
    {
        public static float Radian(float val)
        {
            return (float)(val * Math.PI / 180);
        }

        public static int MyFindIndex(this List<Vector3> vectors, Vector3 vector)
        {
            for (int i = 0; i < vectors.Count; i++)
            {
                if (vectors[i] == vector)
                    return i;
            }
            return -1;
        }

        public static List<Vector3> RotateY(this List<Vector3> vertices, Vector3 rotateCenter, float angle)
        {
            return new List<Vector3>(vertices.ToArray().RotateY(rotateCenter, angle));
        }

        public static Vector3[] RotateY(this Vector3[] vertices, Vector3 rotateCenter, float angle)
        {
            angle = Radian(angle);
            Vector3[] newvert = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new Vector3
                {
                    X = (float)(vertices[i].X * Math.Cos(angle) - vertices[i].Z * Math.Sin(angle)),
                    Y = vertices[i].Y,
                    Z = (float)(vertices[i].X * Math.Sin(angle) + vertices[i].Z * Math.Cos(angle))
                };
            }
            return newvert;
        }

        public static List<Vector3> RotateZ(this List<Vector3> vertices, Vector3 rotateCenter, float angle)
        {
            return new List<Vector3>(vertices.ToArray().RotateZ(rotateCenter, angle));
        }

        public static Vector3[] RotateZ(this Vector3[] vertices, Vector3 rotateCenter, float angle)
        {
            angle = Radian(angle);
            Vector3[] newvert = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new Vector3
                {
                    X = (float)(vertices[i].X * Math.Cos(angle) - vertices[i].Y * Math.Sin(angle)),
                    Z = vertices[i].Z,
                    Y = (float)(vertices[i].X * Math.Sin(angle) + vertices[i].Y * Math.Cos(angle))
                };
            }
            return newvert;
        }

        public static List<Vector3> RotateX(this List<Vector3> vertices, Vector3 rotateCenter, float angle)
        {
            return new List<Vector3>(vertices.ToArray().RotateX(rotateCenter, angle));
        }

        public static Vector3[] RotateX(this Vector3[] vertices, Vector3 rotateCenter, float angle)
        {
            angle = Radian(angle);
            Vector3[] newvert = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new Vector3
                {
                    Z = (float)(vertices[i].Z * Math.Cos(angle) - vertices[i].Y * Math.Sin(angle)),
                    X = vertices[i].X,
                    Y = (float)(vertices[i].Z * Math.Sin(angle) + vertices[i].Y * Math.Cos(angle))
                };
            }
            return newvert;
        }

        public static My3DModel BasicTransform(Vector3 pos, float size)
        {
            My3DModel model = new My3DModel();
            model.AddTriangle(new Vector3[] {
                pos,pos + new Vector3(0, size, 0),pos + new Vector3(size / 4, 0, 0)
            }, Color.Red);

            model.AddTriangle(new Vector3[] {
            pos,
            pos + new Vector3(size, 0, 0),
            pos + new Vector3(0, size / 4, 0)
            }, Color.Blue);

            model.AddTriangle(new Vector3[] {
            pos,
            pos + new Vector3(0, 0, size),
            pos + new Vector3(size / 4, 0, 0)
            }, Color.Green);

            return model;
        }

        public static Vector3[] BasicSquare(Vector3 pos, float size)
        {
            Vector3[] vertices = new Vector3[6];
            vertices[0] = new Vector3(pos.X + size / 2, pos.Y - size / 2, pos.Z);
            vertices[1] = new Vector3(pos.X + size / 2, pos.Y + size / 2, pos.Z);
            vertices[2] = new Vector3(pos.X - size / 2, pos.Y + size / 2, pos.Z);
            vertices[3] = vertices[0];
            vertices[4] = vertices[2];
            vertices[5] = new Vector3(pos.X - size / 2, pos.Y - size / 2, pos.Z);

            return vertices;
        }

        public static List<Vector3> AddVert(this List<Vector3> list, Vector3[] array)
        {
            if (array != null & list != null)
            {
                for (int i = 0; i < array.Length; i++)
                    list.Add(array[i]);
            }
            return list;
        }

        public static Vector3[] BasicCube(Vector3 pos, float size)
        {
            List<Vector3> vertices = new List<Vector3>();
            vertices.AddVert(BasicSquare(new Vector3(pos.X, pos.Y, pos.Z - size / 2), size));
            vertices.AddVert(BasicSquare(new Vector3(pos.X, pos.Y, pos.Z + size / 2), size));
            vertices.AddVert(BasicSquare(new Vector3(0, 0, 0), size).RotateY(new Vector3(), 90).Move(new Vector3(pos.X, pos.Y, pos.Z)).Move(new Vector3(size / 2, 0, 0)));
            vertices.AddVert(BasicSquare(new Vector3(0, 0, 0), size).RotateY(new Vector3(), -90).Move(new Vector3(pos.X, pos.Y, pos.Z)).Move(new Vector3(-size / 2, 0, 0)));
            vertices.AddVert(BasicSquare(new Vector3(0, 0, 0), size).RotateX(new Vector3(), 90).Move(new Vector3(pos.X, pos.Y, pos.Z)).Move(new Vector3(0, size / 2, 0)));
            vertices.AddVert(BasicSquare(new Vector3(0, 0, 0), size).RotateX(new Vector3(), -90).Move(new Vector3(pos.X, pos.Y, pos.Z)).Move(new Vector3(0, -size / 2, 0)));
            return vertices.ToArray();
        }
        /*
        string btn = "1001";
        int _int = Convert.ToInt32(btn, 2);
        string str = Convert.ToString(125, 2);*/
        public static string ToBit(this bool val)
        {
            return val ? "1" : "0";
        }

        public static int GetVoxelType(bool[,,] voxels, Vector3 voxelpos)
        {
            int x = (int)voxelpos.X, y = (int)voxelpos.Y, z = (int)voxelpos.Z;
            int type = -1;
            string b = "";
            if (voxels != null)
            {

                try
                {
                    b += voxels[x, y, z].ToBit();
                }
                catch
                { b += "0"; }
                try
                {
                    b += voxels[x + 1, y, z].ToBit();
                }
                catch
                { b += "0"; }
                try
                {
                    b += voxels[x, y, z + 1].ToBit();
                }
                catch
                { b += "0"; }
                try
                {
                    b += voxels[x + 1, y, z + 1].ToBit();
                }
                catch
                { b += "0"; }
                try
                {
                    b += voxels[x, y + 1, z].ToBit();
                }
                catch
                { b += "0"; }
                try
                {
                    b += voxels[x + 1, y + 1, z].ToBit();
                }
                catch
                { b += "0"; }
                try
                {
                    b += voxels[x, y + 1, z + 1].ToBit();
                }
                catch
                { b += "0"; }
                try
                {
                    b += voxels[x + 1, y + 1, z + 1].ToBit();
                }
                catch
                { b += "0"; }

                type = Convert.ToInt32(b, 2);
            }
            return type;
        }

        public static float VoxelSize = 32;

        internal static Vector3[] GetVoxelModel(bool[,,] voxels, Vector3 voxelpos)
        {
            int x = (int)voxelpos.X, y = (int)voxelpos.Y, z = (int)voxelpos.Z;
            int vtype = GetVoxelType(voxels, voxelpos);
            ;
            //if (!voxels[x, y, z])
            {
                if (MarchingCubes.ContainsKey(vtype))
                {
                    if (vtype == 128)
                        ;
                    Vector3[] vertex = MarchingCubes[vtype].Scale(new Vector3(VoxelSize));
                    vertex = vertex.Move(new Vector3(x * VoxelSize, y * VoxelSize, z * VoxelSize));
                    return vertex;
                }
                else
                {
                    ;
                }
            }

            return null;
        }

        public static Vector3[] Scale(this Vector3[] points, Vector3 scale)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X *= scale.X;
                points[i].Y *= scale.Y;
                points[i].Z *= scale.Z;
            }
            return points;
        }

        public static Vector3[] Move(this Vector3[] points, Vector3 movedir)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X += movedir.X;
                points[i].Y += movedir.Y;
                points[i].Z += movedir.Z;
            }
            return points;
        }

        public static Dictionary<int, Vector3[]> MarchingCubes = new Dictionary<int, Vector3[]>
        {
            {
                1,
                new Vector3[]
                    {
                        new Vector3(1,1,0.5f),
                        new Vector3(1,0.5f,1),
                        new Vector3(0.5f,1,1),

                    }
            },
                        {
                2,
                new Vector3[]
                    {
                        new Vector3(0.5f,1,1),
                        new Vector3(0,0.5f,1),
                        new Vector3(0,1,0.5f),
                    }
            },
                        {
                4,
                new Vector3[]
                    {
                        new Vector3(0.5f,1,0),
                        new Vector3(1,1,0.5f),
                        new Vector3(1,0.5f,0),

                    }
            },
            {
                8,
                new Vector3[]
                    {
                        new Vector3(0,1,0.5f),
                        new Vector3(0.5f,1,0),
                        new Vector3(0,0.5f,0),
                    }
            },
                        {
                16,
                new Vector3[]
                    {
                        new Vector3(1,0,0.5f),
                        new Vector3(1,0.5f,1),
                        new Vector3(0.5f,0,1),
                    }
            },
                                    {
                32,
                new Vector3[]
                    {
                        new Vector3(0.5f,0,1),
                        new Vector3(0,0.5f,1),
                        new Vector3(0,0,0.5f),
                    }
            },
                                    {
                128,
                new Vector3[]
                    {
                        new Vector3(0,0,0.5f),
                        new Vector3(0,0.5f,0),
                        new Vector3(0.5f,0,0),
                    }
            },
                        {
                64,
                new Vector3[]
                    {
                        new Vector3(0.5f,0,0),
                        new Vector3(1,0.5f,0),
                        new Vector3(1,0,0.5f),
                    }
            },
            {
                15,
                BasicSquare(new Vector3(),1).RotateX(new Vector3(),90).Move(new Vector3(0.5f,0.5f,0.5f))
            },
            {
                240,
                BasicSquare(new Vector3(),1).RotateX(new Vector3(),-90).Move(new Vector3(0.5f,0.5f,0.5f))
            }
        };

        public static Vector3[] ToSquareVertex(this Vector3[] vectors)
        {
            Vector3[] nv = new Vector3[6];
            nv[0] = vectors[0];
            nv[1] = vectors[1];
            nv[2] = vectors[2];
            nv[3] = vectors[0];
            nv[4] = vectors[2];
            nv[5] = vectors[3];
            return nv;
        }

        public struct My3DModel
        {
            public VertexPositionColorNormal[] Vertexes => ToVertexes();

            private VertexPositionColorNormal[] ToVertexes()
            {
                List<VertexPositionColorNormal> a = new List<VertexPositionColorNormal>();
                if (_vertices != null)
                    for (int i = 0; i < _vertices.Count; i++)
                    {
                        a.Add(new VertexPositionColorNormal(_vertices[i], _colors[i], _normals[i]));
                    }

                return a.ToArray();
            }

            public void Move(Vector3 movedir)
            {
                for (int i = 0; i < _vertices.Count; i++)
                {
                    _vertices[i] += movedir;
                }
            }

            public void Rotate(Vector3 center = new Vector3(), float yaw = 0, float pitch = 0, float roll = 0)
            {
                if (yaw != 0)
                {
                    this._vertices = _vertices.RotateZ(new Vector3(), yaw);
                }
                if (pitch != 0)
                {
                    this._vertices = _vertices.RotateX(new Vector3(), pitch);
                }
                if (roll != 0)
                {
                    this._vertices.RotateY(new Vector3(), roll);
                }
            }

            public int[] Indices { get => _indices.ToArray(); }
            public Vector3[] Normals { get => _normals.ToArray(); }

            private List<Vector3> _vertices;
            private List<Color> _colors;
            private List<int> _indices;
            public List<Vector3> _normals;
            public void CalculateNormals()
            {
                _normals = new List<Vector3>();

                for (int i = 0; i < _vertices.Count; i++)
                {
                    _normals.Add(new Vector3(0, 0, 0));
                }

                for (int i = 0; i < _indices.Count / 3; i++)
                {
                    int index1 = _indices[i * 3];
                    int index2 = _indices[i * 3 + 1];
                    int index3 = _indices[i * 3 + 2];

                    Vector3 side1 = _vertices[index1] - _vertices[index3];
                    Vector3 side2 = _vertices[index1] - _vertices[index2];
                    Vector3 normal = Vector3.Cross(side1, side2);

                    _normals[index1] += normal;
                    _normals[index2] += normal;
                    _normals[index3] += normal;
                }

                for (int i = 0; i < _normals.Count; i++)
                    _normals[i].Normalize();
            }

            public My3DModel(int val = 0)
            {
                this._colors = new List<Color>();
                this._vertices = new List<Vector3>();
                this._indices = new List<int>();
                this._normals = new List<Vector3>();
            }

            public void Reset()
            {
                this._colors = new List<Color>();
                this._vertices = new List<Vector3>();
                this._indices = new List<int>();
                this._normals = new List<Vector3>();
            }

            public bool AddSquare(Vector3[] verts, Color color)
            {
                if (verts != null)
                {
                    for (int i = 0; i < verts.GetLength(0); i++)
                    {
                        if (_vertices != null)
                        {
                            if (!_vertices.Contains(verts[i]))
                            {
                                _vertices.Add(verts[i]);
                                _colors.Add(color);
                            }
                        }
                        else
                        {
                            this._colors = new List<Color>();
                            this._vertices = new List<Vector3>();
                            this._indices = new List<int>();
                            _vertices.Add(verts[i]);
                            _colors.Add(color);
                        }
                    }
                    // Добавляем индексы
                    {
                        for (int i = 0; i < verts.GetLength(0) / 4; i++)
                        {
                            if (_vertices.MyFindIndex(verts[i]) >= 0)
                            {
                                _indices.Add(_vertices.MyFindIndex(verts[i]));
                                _indices.Add(_vertices.MyFindIndex(verts[i + 1]));
                                _indices.Add(_vertices.MyFindIndex(verts[i + 2]));

                                _indices.Add(_vertices.MyFindIndex(verts[i + 3]));
                                _indices.Add(_vertices.MyFindIndex(verts[i + 4]));
                                _indices.Add(_vertices.MyFindIndex(verts[i + 5]));
                            }
                        }
                    }
                    return true;
                }
                else
                    return false;
            }

            public bool AddSquare(List<Vector3> verts, Color color)
            {
                if (verts != null)
                    return AddSquare(verts.ToArray(), color);
                else
                    return false;
            }

            public bool AddTriangle(Vector3[] verts, Color color)
            {
                if (verts != null)
                {
                    for (int i = 0; i < verts.GetLength(0); i++)
                    {
                        if (_vertices != null)
                        {
                            if (!_vertices.Contains(verts[i]))
                            {
                                _vertices.Add(verts[i]);
                                _colors.Add(color);
                            }
                        }
                        else
                        {
                            this._colors = new List<Color>();
                            this._vertices = new List<Vector3>();
                            this._indices = new List<int>();
                            _vertices.Add(verts[i]);
                            _colors.Add(color);
                        }
                    }
                    // Добавляем индексы
                    {
                        for (int i = 0; i < verts.GetLength(0); i++)
                        {
                            if (_vertices.MyFindIndex(verts[i]) >= 0)
                                _indices.Add(_vertices.MyFindIndex(verts[i]));
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            public bool AddTriangle(List<Vector3> verts, Color color)
            {
                if (verts != null)
                    return AddTriangle(verts.ToArray(), color);
                else
                    return false;
            }
        }
    }
}
