using SFML.System;

namespace Destiny.GameModules
{
    public class GameLocation
    {
        public int width { get => data.GetLength(0); }
        public int height { get => data.GetLength(1); }

        public Tile[,] data = new Tile[50, 50];

        public Tile Get(Vector2i pos)
        {
            return data[pos.X, pos.Y];
        }

        public GameLocation(int width = 50,int height = 50)
        {
            data = new Tile[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    data[x, y] = new Tile();
        }

        public bool IsOutOffBound(Vector2i posi)
        {
            if ((posi.X >= 0 & posi.X < width) & (posi.Y >= 0 & posi.Y < height))
            {
                return false;
            }
            else return true;
        }

        internal bool IsWall(Vector2i pos)
        {
            return data[pos.X, pos.Y].IsSolid();
        }

        internal bool InsertVoid(Vector2i pos)
        {
            if (data[pos.X, pos.Y].Id != 0)
            {
                data[pos.X, pos.Y] = new Tile();
                return true;
            }
            else
                return false;
        }

        internal bool InsertNotVoid(Vector2i pos)
        {
            if (data[pos.X, pos.Y].Id == 0)
            {
                data[pos.X, pos.Y] = new Tile();
                return true;
            }
            else
                return false;
        }

        internal void InsertTile(Vector2i pos, Tile tile)
        {
            data[pos.X, pos.Y] = new Tile(tile);
        }

        internal bool IsVoid(Vector2i pos)
        {
            return (data[pos.X, pos.Y].Id != 0);
        }

        internal bool IsSolid(Vector2i pos)
        {
            if (!IsOutOffBound(pos))
                return data[pos.X, pos.Y].IsSolid();
            else return true;
        }
    }
}