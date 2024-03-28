using GameEngine.GameData;
using Microsoft.Xna.Framework;

namespace GameEngine.Generators
{
    public static class StructureInstruments
    {


        public static int FillVoid(Vector2 startPos, bool[,] structure)
        {
            int i = 0;

            for (int x = 0; x < structure.GetLength(0); x++)
                for (int y = 0; y < structure.GetLength(1); y++)
                {
                    if (!structure[x, y])
                    {
                        if (GameLocation.InsertVoid((startPos + new Vector2(x, y))))
                            i++;
                    }
                }
            return i;
        }

        public static int FillNotVoid(Vector2 startPos, bool[,] structure)
        {
            int i = 0;

            for (int x = 0; x < structure.GetLength(0); x++)
                for (int y = 0; y < structure.GetLength(1); y++)
                {
                    if (!structure[x, y])
                    {
                        if (GameLocation.InsertNotVoid((startPos + new Vector2(x, y))))
                            i++;
                    }
                }
            return i;
        }

        public static int FillTile(Vector2 startPos, bool[,] structure, int tileType)
        {
            for (int x = 0; x < structure.GetLength(0); x++)
                for (int y = 0; y < structure.GetLength(1); y++)
                {
                    if (!structure[x, y])
                    {
                        GameLocation.InsertTile((startPos + new Vector2(x, y)), new Tile(tileType));
                    }
                }
            return Count(structure);
        }

        public static int ReplaceTile(Vector2 startPos, bool[,] structure, int replaced, int newid)
        {
            int i = 0;
            for (int x = 0; x < structure.GetLength(0); x++)
                for (int y = 0; y < structure.GetLength(1); y++)
                {
                    if (!structure[x, y])
                    {
                        if (GameLocation.Get((startPos + new Vector2(x, y))).Id == replaced)
                        {
                            GameLocation.InsertTile((startPos + new Vector2(x, y)), new Tile(newid));
                            i++;
                        }
                    }
                }
            return i;
        }

        public static int Count(bool[,] structure)
        {
            int i = 0;
            for (int x = 0; x < structure.GetLength(0); x++)
                for (int y = 0; y < structure.GetLength(1); y++)
                {
                    if (!structure[x, y])
                    {
                        i++;
                    }
                }
            return i;
        }

        public static int FillNotVoidTile(Vector2 startPos, bool[,] structure, int tileType)
        {
            int i = 0;

            for (int x = 0; x < structure.GetLength(0); x++)
                for (int y = 0; y < structure.GetLength(1); y++)
                {
                    if (!structure[x, y])
                    {
                        if (GameLocation.IsVoid((startPos + new Vector2(x, y))))
                        {
                            GameLocation.InsertTile((startPos + new Vector2(x, y)), new Tile(tileType));
                            i++;
                        }
                    }
                }
            return i;
        }

        public static int FillWallTile(Vector2 startPos, bool[,] structure, int tileType)
        {
            int i = 0;

            for (int x = 0; x < structure.GetLength(0); x++)
                for (int y = 0; y < structure.GetLength(1); y++)
                {
                    if (!structure[x, y])
                    {
                        if (GameLocation.IsWall((startPos + new Vector2(x, y))))
                        {
                            GameLocation.InsertTile((startPos + new Vector2(x, y)), new Tile(tileType));
                            i++;
                        }
                    }
                }
            return i;
        }

        //public static int CreateOre(Vector2 startPos, bool[,] structure, string prefab)
        //{
        //    int i = 0;

        //    for (int x = 0; x < structure.GetLength(0); x++)
        //        for (int y = 0; y < structure.GetLength(1); y++)
        //        {
        //            if (!structure[x, y])
        //            {
        //                if (!GameLocation.IsSolid((startPos + new Vector2(x, y))))
        //                {
        //                    if (!GameObjectManager.InPoint(Utilites.Multiply(startPos + new Vector2(x, y), Tile.tileSize),new OreDeposit()))
        //                    {
        //                        GameObjectManager.Create(prefab).CreateInWorld(Utilites.Multiply((startPos + new Vector2(x, y)), Tile.tileSize));
        //                        i++;
        //                    }
        //                }
        //            }
        //        }
        //    return i;
        //}
    }
}
