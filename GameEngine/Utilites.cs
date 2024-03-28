using GameEngine.Systems;
using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static GameEngine.Systems.UI.Label;
using static GameEngine.Systems.UI.UserInterfaceElement;

namespace GameEngine
{
    public static class Utilites
    {
        private static Random random = new Random();



        public static Point ToPoint(this Vector3 v3)
        {
            return new Point((int)v3.X, (int)v3.Y);
        }
        public static int ToInt(this string a, int _base)
        {
            int x = int.Parse(a);
            int res = 0;
            //В цикле проходим по всем цифрам числа 
            for (int i = 0; x != 0; i++)
            {
                //Увеличиваем искомое число на (цифру на i-ом месте слева, умноженную на (3 в степени i))
                res += (x % 10) * (int)Math.Pow(_base, i);
                //"Убиваем" текущую последнюю цифру введенного числа, чтобы получить следующую слева цифру
                x /= 10;
            }
            return res;
        }
        public static List<string> ToStringList(this List<SMyText> lines)
        {
            List<string> newlines = new List<string>();
            string text = "";

            foreach (SMyText line in lines)
            {
                if (line.newLine)
                {
                    newlines.Add(text);
                    text = "";
                    text += line.text;
                }
                else
                    text += line.text;
            }
            if (text != "")
                newlines.Add(text);

            return newlines;
        }
        public static Vector2 ToVector(this EAlignUI alignUI)
        {
            return alignUI.ToVector(new Vector2(0, 0));
        }

        public static Vector2 ToVector(this EAlignUI alignUI, Vector2 bounds)
        {
            switch (alignUI)
            {
                case EAlignUI.leftTop:
                    return new Vector2(0, 0);
                case EAlignUI.top:
                    return new Vector2(RenderSystem.window.Size.X / 2 - bounds.X / 2, 0);
                case EAlignUI.rightTop:
                    return new Vector2(RenderSystem.window.Size.X - bounds.X, 0);
                case EAlignUI.left:
                    return new Vector2(0, RenderSystem.window.Size.Y / 2 - bounds.Y / 2);
                case EAlignUI.center:
                    return EAlignUI.left.ToVector(bounds) + EAlignUI.top.ToVector(bounds);
                case EAlignUI.right:
                    return EAlignUI.left.ToVector(bounds) + EAlignUI.rightTop.ToVector(bounds);
                case EAlignUI.leftBottom:
                    return new Vector2(0, RenderSystem.window.Size.Y - bounds.Y);
                case EAlignUI.bottom:
                    return EAlignUI.leftBottom.ToVector(bounds) + EAlignUI.top.ToVector(bounds);
                case EAlignUI.rightBottom:
                    return EAlignUI.leftBottom.ToVector(bounds) + EAlignUI.rightTop.ToVector(bounds);
            }
            return new Vector2(0, 0);
        }

        public static float ToRadians(this float val)
        {
            return (val * (float)Math.PI / 180);
        }

        public static Vector2 Add(this Vector2 v, float val)
        {
            return new Vector2(v.X + val, v.Y + val);
        }

        public static Vector3 Add(this Vector3 v3, Vector2 v2)
        {
            return new Vector3(v3.X + v2.X, v3.Y + v2.Y, v3.Z);
        }

        public static Vector2 ToSpriteIsoTilePos(Point pos)
        {
            Vector2 isosize = Tile.isosize;
            Point worldorigin = MapControlSystem.WorldOrigin;
            Vector2 p = new Vector2
                ((pos.X - pos.Y) * isosize.X / 2,
                  (pos.X + pos.Y) * isosize.Y / 2);
            p += worldorigin.ToVector2() * isosize;
            return p;
        }

        public static Vector2 ToSpriteIsoTilePos(Vector3 pos, Vector2 isosize, Point worldorigin)
        {
            Vector2 p = new Vector2
                ((pos.X - pos.Y) * isosize.X / 2,
                  (pos.X + pos.Y) * isosize.Y / 2);
            p += worldorigin.ToVector2() * isosize;
            p.Y -= isosize.Y * pos.Z;
            return p;
        }

        public static Vector2 ToViewportFromIso(Vector3 pos)
        {
            Vector2 isosize = Tile.isosize;
            Point worldorigin = MapControlSystem.WorldOrigin;
            Vector2 p = new Vector2
            ((pos.X - pos.Y) * isosize.X / 2,
            (pos.X + pos.Y) * isosize.Y / 2);
            p += worldorigin.ToVector2() * isosize;
            p.Y -= isosize.Y * pos.Z;
            return p;
        }

        public static Vector3 toVector3(this Vector2 v, float z)
        {
            return new Vector3(v, z);
        }

        public static bool IsoIsVisible(Tile[,,] map, int x, int y, int z)
        {
            if (x == map.GetLength(0) - 1)
                return true;
            if (y == map.GetLength(1) - 1)
                return true;
            try
            {
                if (map[x, y, z + 1] == null)
                    return true;
            }
            catch { return true; }

            try
            {
                if (map[x + 1, y, z] == null)
                    return true;
            }
            catch { return true; }

            try
            {
                if (map[x, y + 1, z] == null)
                    return true;
            }
            catch { return true; }
            return false;
        }

        public static Point GetIsoPos(Point pos)
        {
            Vector2 isosize = Tile.isosize;
            Point worldorigin = MapControlSystem.WorldOrigin;

            Point vCell = pos / isosize.ToPoint();


            Point p = new Point
                ((vCell.Y - worldorigin.Y) + (vCell.X - worldorigin.X),
                  (vCell.Y - worldorigin.Y) - (vCell.X - worldorigin.X));

            //RenderSystem.DrawLine(new List<Vector2>() { new Vector2(), new Vector2(0, isosize.Y), new Vector2(isosize.X, isosize.Y), new Vector2(isosize.X, 0) }, worldorigin.ToVector2() * isosize, 2, Color.Green);

            Vector2 vOffset = new Vector2(pos.ToVector2().X % isosize.X, pos.ToVector2().Y % isosize.Y);
            //RenderSystem.batch.DrawString(ResourseManager.DEFAULT_FONT, vOffset.ToString(), new Vector2(50, 50), new Color(255, 0, 0, 150), 0, new Vector2(), 0.5f, SpriteEffects.None, 0);

            Vector2 vpos = new Vector2(100, 50);
            Point vp = new Point(0, 0);

            if (vOffset.X <= isosize.X / 2)
            {
                if (PointInTriangle(vOffset, new Vector2(), new Vector2(0, isosize.Y / 2), new Vector2(isosize.X / 2, 0)))
                {
                    p.X--;
                    //RenderSystem.DrawLine(new List<Vector2>() { new Vector2(), new Vector2(0, isosize.Y / 2), new Vector2(isosize.X / 2, 0) }, vpos, 2, Color.Green);
                }
                else
                if (PointInTriangle(vOffset, new Vector2(0, isosize.Y), new Vector2(0, isosize.Y / 2), new Vector2(isosize.X / 2, isosize.Y)))
                {
                    p.Y++;
                    //RenderSystem.DrawLine(new List<Vector2>() { new Vector2(0, isosize.Y), new Vector2(0, isosize.Y / 2), new Vector2(isosize.X / 2, isosize.Y) }, vpos, 2, Color.Green);
                }
            }
            else
            if (PointInTriangle(vOffset, new Vector2(isosize.X / 2, 0), new Vector2(isosize.X, isosize.Y / 2), new Vector2(isosize.X, 0)))
            {
                p.Y--;
                //RenderSystem.DrawLine(new List<Vector2>() { new Vector2(isosize.X / 2, 0), new Vector2(isosize.X, isosize.Y / 2), new Vector2(isosize.X, 0) }, vpos, 2, Color.Green);
            }
            else
            if (PointInTriangle(vOffset, new Vector2(isosize.X, isosize.Y), new Vector2(isosize.X, isosize.Y / 2), new Vector2(isosize.X / 2, isosize.Y)))
            {
                p.X++;
                //RenderSystem.DrawLine(new List<Vector2>() { new Vector2(isosize.X, isosize.Y), new Vector2(isosize.X, isosize.Y / 2), new Vector2(isosize.X / 2, isosize.Y) }, vpos, 2, Color.Green);
            }

            return p;
        }

        public static string GenerateGUID()
        {
            string guid = ($"[{RandomString(4, false)}-{RandomString(4, false)}-{RandomString(4, false)}-{RandomString(4, false)}]").ToUpper();
            while (SystemManager.gameObjects.Contains(guid))
            {
                guid = ($"[{RandomString(4, false)}-{RandomString(4, false)}-{RandomString(4, false)}-{RandomString(4, false)}]").ToUpper();
            }
            return guid;
        }

        public static bool Intersect(this Rectangle rect, Vector2 point)
        {
            return ((point.X >= rect.X) & (point.X <= rect.X + rect.Width) & (point.Y >= rect.Y) & (point.Y <= rect.Y + rect.Height));
        }

        public static bool Intersect(this Rectangle rect, Point point)
        {
            return ((point.X >= rect.X) & (point.X <= rect.X + rect.Width) & (point.Y >= rect.Y) & (point.Y <= rect.Y + rect.Height));
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, int thicknes = 2, float layer = 0)
        {
            float angle = GetRotation(point2 - point1).ToRadians();
            float distance = Distance(point1, point2) + thicknes;
            spriteBatch.DrawRectangle(new Rectangle((point1 + new Vector2(0, (thicknes / 2) * -1)).ToPoint(), new Point((int)distance, thicknes)), color, angle, layer);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float rotate = 0, float layer = 0)
        {
            spriteBatch.Draw(ResourseManager.Texture("pixel"), rect, null, color, rotate, new Vector2(0, 0), SpriteEffects.None, layer);
        }

        public static void DrawRectangleWithOutline(this SpriteBatch spriteBatch, Rectangle rect, Color fillcolor, Color outline, float thicknes = 2, float layer = 0)
        {
            spriteBatch.Draw(ResourseManager.Texture("pixel"), rect, null, outline, 0, new Vector2(), SpriteEffects.None, layer);
            spriteBatch.Draw(ResourseManager.Texture("pixel"), new Rectangle((int)(rect.X + thicknes), (int)(rect.Y + thicknes), (int)(rect.Width - thicknes * 2), (int)(rect.Height - thicknes * 2)), null, fillcolor, 0, new Vector2(), SpriteEffects.None, layer - 0.0001f);
        }

        public static void SetFromVector(this Vector2 vector, out float x, out float y)
        {
            x = vector.X;
            y = vector.Y;
        }

        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }

        public static Color Random(this Color c)
        {
            return new Color(RandomInt(0, 255), RandomInt(0, 255), RandomInt(0, 255));
        }
        public struct VertexPositionColorNormal
        {
            public Vector3 Position;
            public Color Color;
            public Vector3 Normal;
            private void CalculateNormals()
            {
                Normal = new Vector3(0, 0, 0);
            }
            public VertexPositionColorNormal(Vector3 position, Color color)
            {
                Position = position;
                Color = color;
                Normal = new Vector3(1, 1, 1);
            }

            public VertexPositionColorNormal(Vector3 position, Color color, Vector3 normal)
            {
                Position = position;
                Color = color;
                Normal = normal;
            }

            public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
            (
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
            );
        }

        public static VertexPositionColorNormal[] Move(this VertexPositionColorNormal[] vertices, Vector3 pos)
        {
            VertexPositionColorNormal[] newvert = new VertexPositionColorNormal[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new VertexPositionColorNormal(vertices[i].Position + pos, vertices[i].Color);
            }
            return newvert;
        }

        public static VertexPositionColorNormal[] RenderHeightMap(float[,] map, float voxelsize, float height)
        {


            List<VertexPositionColorNormal> vertices = new List<VertexPositionColorNormal>();
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Color color = Color.Blue;
                    if (map[x, y] < height * 0.15f)
                    {
                        map[x, y] = height * 0.05f;
                    }
                    if (map[x, y] >= height * 0.80f)
                    {
                        color = Color.White;
                        map[x, y] = height * 0.81f;
                    }
                    else
                    if (map[x, y] >= height * 0.65f)
                    {
                        color = Color.DarkGray;
                        map[x, y] = height * 0.66f;
                    }
                    else
                    if (map[x, y] >= height * 0.30f)
                    {
                        color = Color.Green;
                        map[x, y] = height * 0.31f;
                    }
                    else
                    if (map[x, y] >= height * 0.15f)
                    {
                        color = Color.Yellow;
                        map[x, y] = height * 0.16f;
                    }




                    vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x, map[x, y], voxelsize * -y), color));
                    continue;
                    try
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x, map[x, y + 1], voxelsize * y + voxelsize), Color.White));
                    }
                    catch
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x, 0, voxelsize * y + voxelsize), Color.White));
                    }
                    try
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x + voxelsize, map[x + 1, y], voxelsize * y), Color.White));
                    }
                    catch
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x + voxelsize, 0, voxelsize * y), Color.White));
                    }
                    // --------------------------------------------------------
                    try
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x, map[x, y + 1], voxelsize * y + voxelsize), Color.Gray));
                    }
                    catch
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x, 0, voxelsize * y + voxelsize), Color.Gray));
                    }
                    try
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x + voxelsize, map[x + 1, y + 1], voxelsize * y + voxelsize), Color.Gray));
                    }
                    catch
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x + voxelsize, 0, voxelsize * y + voxelsize), Color.Gray));
                    }
                    try
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x + voxelsize, map[x + 1, y], voxelsize * y), Color.Gray));
                    }
                    catch
                    {
                        vertices.Add(new VertexPositionColorNormal(new Vector3(voxelsize * x + voxelsize, 0, voxelsize * y), Color.Gray));
                    }
                }
            return vertices.ToArray();
        }

        public static VertexPositionColor[] RemoveNormals(this VertexPositionColorNormal[] vertices)
        {
            VertexPositionColor[] newvert = new VertexPositionColor[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new VertexPositionColor(vertices[i].Position, vertices[i].Color);
            }
            return newvert;
        }

        public static VertexPositionColorNormal[] AddNormals(this VertexPositionColor[] vertices)
        {
            VertexPositionColorNormal[] newvert = new VertexPositionColorNormal[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new VertexPositionColorNormal(vertices[i].Position, vertices[i].Color);
            }
            return newvert;
        }

        public static VertexPositionColor[] Move(this VertexPositionColor[] vertices, Vector3 pos)
        {
            VertexPositionColor[] newvert = new VertexPositionColor[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new VertexPositionColor(vertices[i].Position + pos, vertices[i].Color);
            }
            return newvert;
        }

        public static VertexPositionColor[] Add(this VertexPositionColor[] vertices, VertexPositionColor[] addverices)
        {
            VertexPositionColor[] newvert = new VertexPositionColor[vertices.Length + addverices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newvert[i] = new VertexPositionColor(vertices[i].Position, vertices[i].Color);
            }
            for (int i = vertices.Length; i < newvert.Length; i++)
            {
                newvert[i] = new VertexPositionColor(addverices[i - vertices.Length].Position, addverices[i - vertices.Length].Color);
            }
            //vertices = newvert;
            return newvert;
        }

        public static void Position(this FloatRect rect, Vector2 pos)
        {
            rect.Left = pos.X;
            rect.Top = pos.Y;
        }

        public static void Size(this FloatRect rect, Vector2 size)
        {
            rect.Width = size.X;
            rect.Height = size.Y;
        }

        public static void Size(this FloatRect rect, float size)
        {
            rect.Width = size;
            rect.Height = size;
        }

        internal static bool Intersect(this FloatRect r1, FloatRect r2)
        {
            return
                (
                ((r1.Left + r1.Width >= r2.Left) && (r1.Left <= r2.Left + r2.Width)) &&
                ((r1.Top + r1.Height >= r2.Top) && (r1.Top <= r2.Top + r2.Height))
                );
        }
        public struct FloatRect
        {
            public float Left, Top, Width, Height;

            public FloatRect(Vector2 pos, Vector2 size)
            {
                this.Top = pos.Y;
                this.Left = pos.X;
                this.Height = size.Y;
                this.Width = size.X;
            }

            public FloatRect(Vector3 pos, Vector2 size)
            {
                this.Top = pos.Y;
                this.Left = pos.X;
                this.Height = size.Y;
                this.Width = size.X;
            }

            public Vector2 Center { get => new Vector2(Left + Width / 2, Top + Height / 2); }

            public FloatRect(float x, float y, float width, float height) : this()
            {
                this.Left = x;
                this.Top = y;
                Width = width;
                Height = height;
            }
        }

        public static Color RandomColor()
        {
            return new Color(RandomInt(0, 255), RandomInt(0, 255), RandomInt(0, 255));
        }

        public static string RandomString(int count, bool useSymbols = true)
        {
            string a, b;
            a = null;
            if (useSymbols)
                b = "123456789qwertyuiop[]asdfghjkl;zxcvbnm,.!@#$%^&*()!№?" + "qwertyuiopasdfghjklzxcvbnm".ToUpper();
            else
                b = "123456789qwertyuiopasdfghjklzxcvbnm" + "qwertyuiopasdfghjklzxcvbnm".ToUpper();
            while (count > 0)
            {
                a += b[RandomInt(0, b.Length - 1)];
                count--;
            }
            return a;
        }

        public static int RandomInt(int max)
        {
            return random.Next(0, max);
        }

        public static Vector2 RandomVector2(int max)
        {
            return new Vector2(RandomInt(max), RandomInt(max));
        }

        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max + 1);
        }

        public static float RandomFloat(float min, float max)
        {
            float a = (random.Next(0, (int)(max * 100000 + Math.Abs(min * 100000))) - (int)Math.Abs(min * 100000)) / 100000;
            return a;
        }

        /// <summary>
        /// Точка между двумя точками
        /// </summary>
        public static float MidPoint(float a, float b)
        {
            return (a + b) / 2;
        }

        public static bool Chance(int ch)
        {
            return (RandomInt(0, 101) <= ch) ? true : false;
        }

        /// <summary>
        /// Расстояние между точками
        /// </summary>        
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float d = 0;
            d = (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return d;
        }

        /// <summary>
        /// Расстояние между точками
        /// </summary>        
        public static float Distance(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            float d = 0;
            d = (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));
            return d;
        }

        /// <summary>
        /// Расстояние между точками
        /// </summary>
        public static float Distance(Vector2 p1, Vector2 p2)
        {
            return Distance(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// Расстояние между точками
        /// </summary>
        public static float Distance(Vector3 p1, Vector3 p2)
        {
            return Distance(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z);
        }


        /// <summary>
        /// Расстояние между точками
        /// </summary>
        public static float Distance(Point p1, Point p2)
        {
            return Distance(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static float RandomValue()
        {
            return (Chance(50)) ? 1 : -1;
        }

        public static bool MaxMinChanceRandom(float minvalue, float maxvalue, float value)
        {
            if ((minvalue == maxvalue) & (maxvalue == value))
                return true;
            if ((minvalue == maxvalue) & (maxvalue != value))
                return false;

            if (maxvalue > minvalue)
                if (value >= maxvalue)
                    return true;
                else
                if (value <= minvalue)
                    return false;
                else
                    return !ChanceRandom100((Math.Abs(value) - Math.Abs(minvalue)) / (Math.Abs(maxvalue) - Math.Abs(minvalue)));

            if (maxvalue < minvalue)
                if (value <= maxvalue)
                    return true;
                else
                if (value <= maxvalue)
                    return false;
                else
                    return !ChanceRandom100((Math.Abs(value) - Math.Abs(maxvalue)) / (Math.Abs(minvalue) - Math.Abs(maxvalue)));

            return false;
        }

        public static Vector2 Multiply(this Vector2 vec2, float val)
        {
            return new Vector2(vec2.X * val, vec2.Y * val);
        }

        public static Vector3 Multiply(this Vector3 vec3, float val)
        {
            return new Vector3(vec3.X * val, vec3.Y * val, vec3.Z * val);
        }

        public static Vector4 Multiply(this Vector4 vec4, float val)
        {
            return new Vector4(vec4.X * val, vec4.Y * val, vec4.Z * val, vec4.W * val);
        }

        public static Vector2 To2d(this Vector3 vec3)
        {
            return new Vector2(vec3.X, vec3.Y);
        }

        public static Vector3 To3d(this Vector4 vec4)
        {
            return new Vector3(vec4.X, vec4.Y, vec4.Z);
        }

        public static Vector2 To2d(this Vector4 vec4)
        {
            return new Vector2(vec4.X, vec4.Y);
        }

        /// <summary>
        /// от 0 до 1
        /// </summary>
        public static bool ChanceRandom100(float chance)
        {
            if (chance >= 1)
                return true;
            if (chance <= 0)
                return false;

            if (RandomInt(1, 100) >= chance * 100)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Пример шанс 1 из 1000 Chance(1, 1000), пример шанс 20% Chance(2, 10)
        /// </summary>
        public static bool Chance(int chance, int MaxValue)
        {
            return (RandomInt(1, MaxValue) <= chance) ? true : false;
        }

        /// <summary>
        /// Отрезок
        /// </summary>
        public struct Segment
        {
            public Vector2 A;
            public Vector2 B;

            public Segment(Vector2 a, Vector2 b)
            {
                A = a;
                B = b;
            }

            public Vector2 ToVector2()
            {
                return (new Vector2(B.X, B.Y) - new Vector2(A.X, A.Y));
            }
        }

        /*
        /// <summary>
        /// Проверка есть ли пересечения у прямоугольника с отрезком
        /// </summary>
        public static Vector2f GetIntersectPoint(Segment seg, Rectangle rectangle)
        {
            return (GetIntersectPoint(seg,IntersectedSegments(seg, rectangle.ToSegmentList())));
        }
        */
        /// <summary>
        /// Райкаст по фронтальным тайлам
        /// </summary>
        /// 
        /*
        public static Vector2f Raycast(Vector2f start, Vector2f end)
        {
            Vector2f oldp = start;
            Vector2f step = (end - start);
            step = Normalize(step);
            step *= (Tile.tileSize * 1f);
            start += step;
            while (Distance(start, end) >= Tile.tileSize)
            {

                if (GameCore.map.FrontTiles.Get(start, false) != null)
                {
                    return GetIntersectPoint(new Segment(oldp, end), new Rectangle(GetTilePos(start), Tile.tileSize));
                }
                start += step;
            }
            return end;
        }
        */
        /// <summary>
        /// Отрезки с которыми есть пересечение
        /// </summary>
        public static List<Segment> IntersectedSegments(Segment seg, List<Segment> list)
        {
            List<Segment> a = new List<Segment>();
            foreach (Segment segment in list)
            {
                if (CanIntersect(seg, segment))
                    if (IsIntersect(seg, segment))
                        a.Add(segment);
            }
            return a;
        }

        /// <summary>
        /// Ближайшая точка пересечения с отрезками
        /// </summary>
        public static Vector2 GetIntersectPoint(Segment seg, List<Segment> list)
        {
            Vector2 min = Intersect(seg, list[0]);
            int mini = 0;
            float mind = Distance(seg.A, Intersect(seg, list[0]));

            for (int i = 0; i < list.Count; i++)
            {
                if (mind > Distance(seg.A, Intersect(seg, list[i])))
                {
                    mind = Distance(seg.A, Intersect(seg, list[i]));
                    min = Intersect(seg, list[i]);
                    mini = i;

                }
            }
            //RenderCore.AddToDelayDraw(min);
            /*
            RenderCore.AddToDelayDraw(seg.A, min, Color.Yellow);
            RenderCore.AddToDelayDraw(min, seg.B, new Color(100, 100, 100));
            */
            //RenderCore.AddToDelayDraw(list[mini]);
            return min;
        }

        /// <summary>
        /// Нормализация вектора
        /// </summary>
        public static Vector2 Normalize(Vector2 p)
        {
            return new Vector2((float)(p.X / Math.Sqrt(p.X * p.X + p.Y * p.Y)), (float)(p.Y / Math.Sqrt(p.X * p.X + p.Y * p.Y)));
        }

        /// <summary>
        /// Могут ли пересечься прямые
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool CanIntersect(Segment s1, Segment s2)
        {
            return (Normalize(s1.ToVector2()) == Normalize(s2.ToVector2())) ? false : true;
        }


        /// <summary>
        /// Пересекаются ли отрезки
        /// </summary>
        public static bool IsIntersect(Segment s1, Segment s2)
        {
            bool result;
            if (CanIntersect(s1, s2))
            {
                float a = (s1.B.X - s1.A.X);
                //    x2 - x1; 
                float b = (s2.A.X - s2.B.X);
                //    x3 - x4; 
                float c = (s2.A.X - s1.A.X);
                //    x3 - x1; 
                float d = (s1.B.Y - s1.A.Y);
                //    y2 - y1; 
                float e = (s2.A.Y - s2.B.Y);
                //    y3 - y4; 
                float f = (s2.A.Y - s1.A.Y);
                //    y3 - y1; 
                float det = (a * e - b * d);
                //    вычислим определитель матрицы
                if (det == 0) result = false;
                // прямые не пересекаются
                else
                {
                    float dt = (c * e - f * b);
                    float ds = (a * f - c * d);
                    float t = (dt / det);
                    float s = (ds / det);
                    result = ((s >= 0) & (s <= 1)// принадлежит 1
                        & (0 <= t) & (t <= 1));// принадлежит 2
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// Точка пересечения отрезков
        /// </summary>
        public static Vector2 Intersect(Segment s1, Segment s2)
        {
            Segment h = s1;

            s1 = s2;
            s2 = h;

            Vector2 pt;

            pt.X = -((s1.A.X * s1.B.Y - s1.B.X * s1.A.Y) * (s2.B.X - s2.A.X) - (s2.A.X * s2.B.Y - s2.B.X * s2.A.Y) * (s1.B.X - s1.A.X)) / ((s1.A.Y - s1.B.Y) * (s2.B.X - s2.A.X) - (s2.A.Y - s2.B.Y) * (s1.B.X - s1.A.X));
            pt.Y = ((s2.A.Y - s2.B.Y) * (-pt.X) - (s2.A.X * s2.B.Y - s2.B.X * s2.A.Y)) / (s2.B.X - s2.A.X);

            h = s1;

            s1 = s2;
            s2 = h;
            //RenderCore.AddToDelayDraw(pt);
            //RenderCore.AddToDelayDraw(s2);
            //RenderCore.AddToDelayDraw(s1);            
            return pt;
        }

        /// <summary>
        /// Ближайшая точка на отрезке к точке
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="a">Точки отрезка</param>
        /// <param name="b">Точки отрезка</param>
        public static Vector2 NearestToSegment(Vector2 point, Vector2 a, Vector2 b)
        {
            Vector2 vecAB = a - point;
            Vector2 vecAC = b - point;

            // квадрат длины AB
            float lenAB2 = (float)Math.Pow((float)Math.Pow(vecAB.X, 2) + (float)Math.Pow(vecAB.Y, 2), 2);

            if (lenAB2 < Double.Epsilon) return (point + a) / 2;

            // относительная длина проекции AC на AB
            float relativeD = DotProduct(vecAB, vecAC) / lenAB2;

            // относительная длина < 0, ближайшая A
            if (relativeD < 0) return point;

            // относительная длина > 1, ближайшая B
            if (relativeD > 1) return a;

            // возвращаем точку проекции
            return point + vecAB * relativeD;
        }

        public static float DotProduct(Vector2 a, Vector2 b)
        {
            return (a.X * b.X + a.Y * b.Y);
        }

        public static float VectorModule(Vector2 a)
        {
            return (float)Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));
        }

        public static float GetRotation(Vector2 moveVector)
        {
            moveVector = Normalize(moveVector);
            /*
            if (moveVector == Down.ToVector2f())
                return 0;
            if (moveVector == Up.ToVector2f())
                return 180;
            if (moveVector == Right.ToVector2f())
                return 270;
            if (moveVector == Left.ToVector2f())
                return 90;

            if (moveVector == LeftUp.ToVector2f())
                return 45;
            if (moveVector == LeftDown.ToVector2f())
                return 305;
            if (moveVector == RightUp.ToVector2f())
                return 135;
            if (moveVector == RightDown.ToVector2f())
                return 225;*/
            //Vector2f zero = new Vector2f(0,0);
            float angle = 0;
            //angle = (float)Math.Cos(DotProduct(moveVector, Up.ToVector2f()) / (VectorModule(moveVector) * VectorModule(Up.ToVector2f())));
            //angle = (float)(Math.Acos(angle) * 180 / Math.PI);
            angle = (float)(Math.Atan2(moveVector.Y, moveVector.X) * 180 / Math.PI);
            //if (moveVector.X > 0) angle += 180;
            return angle;
        }

        /*
        /// <summary>
        /// Перевод точки в тайл-координату (для quadtree), координата остаётся глобальной.
        /// </summary>
        internal static Vector2 GetTilePosGlobal(Vector2 point)
        {
            Vector2 a = new Vector2
            {
                X = ((int)(point.X / Tile.tileSize)),
                Y = ((int)(point.Y / Tile.tileSize))
            };
            Vector2 k = a * Tile.tileSize;
            return k;
        }

        /// <summary>
        /// Перевод точки в тайл-координату
        /// </summary>
        internal static Vector2 GetTilePos(Vector2 point)
        {
            Vector2 a = new Vector2
            {
                X = ((int)(point.X / Tile.tileSize)),
                Y = ((int)(point.Y / Tile.tileSize))
            };
            return a;
        }*/
        /*
        /// <summary>
        /// Позиция мышки на карте
        /// </summary>
        /// <returns></returns>        
        public static Vector2 GetMousePositionInWorld()
        {
            Vector2 a = new Vector2
            {
                X = Mouse.GetPosition(RenderSystem.window).X + Camera.position.X,
                Y = Mouse.GetPosition(RenderSystem.window).Y + Camera.position.Y
            };
            return a;
        }*/
        /*
        public static bool PointOnScreen(Vector2 point)
        {
            return PointInRectangle(point, new FloatRect(Camera.position, RenderSystem.window.Size.ToVector2f()));
        }*/
        public static bool PointInRectangle(Vector2 point, Rectangle rect)
        {
            return PointInRectangle(point, new FloatRect(rect.Location.ToVector2(), rect.Size.ToVector2()));
        }
        public static bool PointInRectangle(Vector2 point, FloatRect rect)
        {
            return (point.X >= rect.Left & point.X <= rect.Left + rect.Width & point.Y >= rect.Top & point.Y <= rect.Top + rect.Height);
        }
        /*
        /// <summary>
        /// Позиция мышки на карте в тайл-координате
        /// </summary>
        /// <returns></returns>       
        public static Vector2 GetMouseTilePos()
        {
            Vector2 a = new Vector2
            {
                X = ((int)(GetMousePositionInWorld().X / Tile.tileSize)),
                Y = ((int)(GetMousePositionInWorld().Y / Tile.tileSize)) - 1
            };
            return a;
        }*/

        private static float sign(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        public static bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
        {
            float d1, d2, d3;
            bool has_neg, has_pos;

            d1 = sign(pt, v1, v2);
            d2 = sign(pt, v2, v3);
            d3 = sign(pt, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }


        public static string ToOnOff(this bool boolean)
        {
            return boolean ? "On" : "Off";
        }

        public static Vector2 Left { get => new Vector2(-1, 0); }
        public static Vector2 Right { get => new Vector2(1, 0); }
        public static Vector2 Top { get => new Vector2(0, -1); }
        public static Vector2 Bottom { get => new Vector2(0, 1); }

        public static Vector2 TopLeft { get => Top + Left; }
        public static Vector2 TopRight { get => Top + Right; }
        public static Vector2 BottomLeft { get => Bottom + Left; }
        public static Vector2 BottomRight { get => Bottom + Right; }
        public static float Hypotenuse(float a, float b)
        {
            return (float)Math.Sqrt(((float)Math.Pow(a, 2) + (float)Math.Pow(b, 2)));
        }
        /*
        public static FloatRect GetBound(GameObject obj)
        {
            FloatRect fr = new FloatRect();

            if (obj.HasComponentType(new Components.Main.Transform()))
            {
                if (obj.HasComponentType(new TextLabel(" ")))
                {
                    //RenderSystem.textBatch.Font = ResourseManager.FontLib[obj.Component<TextLabel>().Font];
                    RenderSystem.textBatch.DisplayedString = obj.Component<TextLabel>().Text;
                    RenderSystem.textBatch.CharacterSize = (uint)obj.Component<TextLabel>().CharacterSize;
                    RenderSystem.textBatch.Position = obj.Component<Components.Main.Transform>().Position.ToVector2i().ToVector2f();
                    fr = RenderSystem.textBatch.GetLocalBounds();
                }
            }

            return fr;
        }*/
        /*
        public static List<Vector2> ConvertToWorldCoord(List<Vector2> old)
        {
            List<Vector2> neww = new List<Vector2>();
            if (old != null)
            if (old.Count>0)
            foreach (Vector2 pos in old)
                neww.Add(pos.Multiply(Tile.tileSize));
            return neww;
        }*/
    }
}
