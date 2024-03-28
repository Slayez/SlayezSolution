namespace GameEngine.Generators
{
    public static class CaveGenerator
    {
        /// <summary>
        /// is the upper neighbour limit at which cells start dying.
        /// </summary>
        public static byte overpopLimit = 3;
        /// <summary>
        /// is the number of neighbours that cause a dead cell to become alive.
        /// </summary>
        public static byte birthNumber = 5;
        /// <summary>
        /// is the lower neighbour limit at which cells start dying.
        /// </summary>
        public static byte starvationLimit = 0;

        public static bool[,] Generate(int width, int height, int minVoid, int maxVoid)
        {
            byte density = 48;

            bool[,] cave = Generate(width, height, density);
            /*
            while (StructureInstruments.Count(cave) < minVoid | StructureInstruments.Count(cave) > maxVoid)
            {
                cave = Generate(width, height, density);
                density++;
            }
            */
            return cave;
        }

        public static bool[,] Generate(int width, int height, byte density)
        {
            //int[,] map = new int[width, height];
            bool[,] cellularMap = new bool[width, height];
            cellularMap = FillRandom(cellularMap, density);
            cellularMap = FillBorder(cellularMap);
            cellularMap = CellularStep(cellularMap, 6);
            return cellularMap;
        }

        private static string[] BoolToStr(bool[,] cellularMap)
        {
            string[] str = new string[cellularMap.GetLength(1)];

            for (int x = 0; x < cellularMap.GetLength(0); x++)
                for (int y = 0; y < cellularMap.GetLength(1); y++)
                {
                    if (cellularMap[x, y])
                        str[y] += "■";
                    else
                        str[y] += "--";
                }
            return str;
        }

        private static bool[,] CellularStep(bool[,] cellularMap, int steps)
        {
            bool[,] newmap = cellularMap;

            for (int step = 0; step < steps; step++)
            {
                bool[,] oldmap = newmap;

                for (int x = 0; x < newmap.GetLength(0); x++)
                    for (int y = 0; y < newmap.GetLength(1); y++)
                    {
                        if (!(((x == 0) | (x == newmap.GetLength(0) - 1)) | ((y == 0) | (y == newmap.GetLength(1) - 1))))
                        {
                            if ((GetNeighbor(oldmap, x, y) >= starvationLimit) & (GetNeighbor(oldmap, x, y) <= overpopLimit))
                                newmap[x, y] = false;
                            if (GetNeighbor(oldmap, x, y) >= birthNumber)
                                newmap[x, y] = true;
                        }
                    }
            }
            return newmap;
        }

        private static byte GetNeighbor(bool[,] oldmap, int x, int y)
        {
            byte count = 0;

            if (oldmap[x - 1, y - 1])
                count++;
            if (oldmap[x, y - 1])
                count++;
            if (oldmap[x + 1, y - 1])
                count++;

            if (oldmap[x - 1, y])
                count++;
            if (oldmap[x + 1, y])
                count++;

            if (oldmap[x - 1, y + 1])
                count++;
            if (oldmap[x, y + 1])
                count++;
            if (oldmap[x + 1, y + 1])
                count++;

            return count;
        }

        private static bool[,] FillBorder(bool[,] cellularMap)
        {
            bool[,] newmap = cellularMap;

            for (int x = 0; x < newmap.GetLength(0); x++)
                for (int y = 0; y < newmap.GetLength(1); y++)
                {
                    if (((x == 0) | (x == newmap.GetLength(0) - 1)) | ((y == 0) | (y == newmap.GetLength(1) - 1)))
                        newmap[x, y] = true;
                }
            return newmap;
        }

        private static bool[,] FillRandom(bool[,] cellularMap, byte density)
        {
            bool[,] newmap = cellularMap;
            for (int x = 0; x < newmap.GetLength(0); x++)
                for (int y = 0; y < newmap.GetLength(1); y++)
                {
                    if (Utilites.Chance(density))
                        newmap[x, y] = true;
                }
            return newmap;
        }
    }
}
