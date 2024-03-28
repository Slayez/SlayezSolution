using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine
{
    public class Tile
    {
        private int _id = 0;
        private int _subId = 0;

        internal static uint width = 64;
        internal static uint height = 32;

        public string? postfix = null;

        public int Id { get => _id; set => _id = value; }
        public int SubId { get => _subId; set => _subId = value; }

        public Tile(int id = 0)
        {
            _id = id;
            _subId = Utilites.RandomInt(0, TilesLibrary[id].SubPool - 1);
        }

        public static Vector2 isosize = new Vector2(64, 32);

        public Color illumination = Color.White;

        public struct STileInfo
        {
            public int SubPool;
            public string TextureName;
            public Point offset;
            public ETilePhysicsType physicsType;

            public STileInfo(int subPool, string textureName, Point offset, ETilePhysicsType physicsType)
            {
                SubPool = subPool;
                TextureName = textureName;
                this.offset = offset;
                this.physicsType = physicsType;
            }
        }

        public enum ETilePhysicsType { none, solid, water }

        public Point _offset { get => TilesLibrary[_id].offset; }

        public static List<STileInfo> TilesLibrary = new List<STileInfo>
        {
            new STileInfo(2,"stone",new Point(),ETilePhysicsType.solid),
            new STileInfo(3,"water",new Point(),ETilePhysicsType.water)
        };

        public static float tileSize = 32;

        public Tile(Tile copy)
        {
            _id = copy._id;
            _subId = copy._subId;
        }

        /*
        public static int GetIdFromByteString(string bytestr)
        {
            return CheckTileByteString(bytestr.Substring(0, 4), bytestr.Substring(4, 4)).ToInt(2);
        }
        */
        /*
        public static int GetNormalId(int id)
        {
            return ResourseManager.TileIdDictionary[id];
        }
        */
        /*
        public static int GetTileId2D(bool[,] table, Point pos)
        {
            int i = 0, i2 = 0;
            string bytestr = "";
            string bytestr2 = "";

            if (table[pos.X, pos.Y])
            {
                /*     
                     
                 0--1--2
                 |     |
                 3  X  4
                 |     |
                 5--6--7 

                0---=---1
                |       |
                =       =
                |       |
                2---=---3

                *---0---*
                |       |
                1       2
                |       |
                *---3---*                
                
                */
        /*
                ;
                for (int y = pos.Y - 1; y < pos.Y + 2; y++)
                    for (int x = pos.X - 1; x < pos.X + 2; x++)
                    {
                        if (x == pos.X | y == pos.Y)
                        {
                            if (x != pos.X | y != pos.Y)
                                if (((x >= 0) & (x < table.GetLength(0))) & ((y >= 0) & (y < table.GetLength(1))))
                                {
                                    bytestr2 =  (table[x, y]).ToBit() + bytestr2;
                                }
                                else
                                    bytestr2 =  "0" + bytestr2;
                        }
                        else
                        {
                            if (x != pos.X | y != pos.Y)
                                if (((x >= 0) & (x < table.GetLength(0))) & ((y >= 0) & (y < table.GetLength(1))))
                                {
                                    bytestr = (table[x, y]).ToBit() + bytestr;
                                }
                                else
                                    bytestr = "0" + bytestr;
                        }
                    }
                bytestr = RevertByteString(bytestr);
                bytestr2 = RevertByteString(bytestr2);
                bytestr = CheckTileByteString(bytestr2, bytestr);
                i = GetIdFromByteString(bytestr);
            }
            return i;
        }
*/
        /*
        public static string RevertByteString(string str)
        {
            char[] c = str.ToCharArray();

            for (int i = 0; i < c.Length; i++)
                c[i] = c[i] == '1' ? '0' : '1';
            return new string(c);
        }

        public static string CheckTileByteString(string line, string dot)
        {
            char[] cdot = dot.ToCharArray();
            char[] cline = line.ToCharArray();
            if (cline[0] == '1')
            {
                cdot[0] = '0';
                cdot[1] = '0';
            }
            if (cline[1] == '1')
            {
                cdot[0] = '0';
                cdot[2] = '0';
            }
            if (cline[2] == '1')
            {
                cdot[1] = '0';
                cdot[3] = '0';
            }
            if (cline[3] == '1')
            {
                cdot[3] = '0';
                cdot[2] = '0';
            }
            return new string(cline) + new string(cdot);
        }
        */
        public Texture2D Texture { get => ResourseManager.Texture(TilesLibrary[this._id].TextureName + this._subId.ToString() + this.postfix); }

    }
}
