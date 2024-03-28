

using Microsoft.Xna.Framework;

namespace GameEngine.GameData
{
    public static class GameLocation
    {
        public static int width { get => data.GetLength(0); }
        public static int height { get => data.GetLength(1); }

        public static Tile[,] data = new Tile[50, 50];

        public static Tile Get(Vector2 pos)
        {
            return data[(int)pos.X, (int)pos.Y];
        }
        /*
        public static GameLocation(int width = 50,int height = 50)
        {
            data = new Tile[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    data[x, y] = new Tile();
        }*/

        public static bool IsOutOffBound(Vector2 posi)
        {
            if ((posi.X >= 0 & posi.X < width) & (posi.Y >= 0 & posi.Y < height))
            {
                return false;
            }
            else return true;
        }

        internal static bool IsWall(Vector2 pos)
        {
            //return data[(int)pos.X, (int)pos.Y].IsSolid();
            return true;
        }

        internal static bool InsertVoid(Vector2 pos)
        {
            if (data[(int)pos.X, (int)pos.Y].Id != 0)
            {
                data[(int)pos.X, (int)pos.Y] = new Tile();
                return true;
            }
            else
                return false;
        }

        internal static bool InsertNotVoid(Vector2 pos)
        {
            if (data[(int)pos.X, (int)pos.Y].Id == 0)
            {
                data[(int)pos.X, (int)pos.Y] = new Tile();
                return true;
            }
            else
                return false;
        }

        internal static void InsertTile(Vector2 pos, Tile tile)
        {
            data[(int)pos.X, (int)pos.Y] = new Tile(tile);
        }

        internal static bool IsVoid(Vector2 pos)
        {
            return (data[(int)pos.X, (int)pos.Y].Id != 0);
        }

        internal static bool IsSolid(Vector2 pos)
        {
            return true;
            /*
            if (!IsOutOffBound(pos))
                return data[(int)pos.X, (int)pos.Y].IsSolid();
            else return true;*/
        }
    }
}