using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Generators
{
    public static class HeightMap
    {
        public static float[,] NewMap(int size, float maxheight)
        {
            float[,] map = new float[size, size];

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    map[x, y] = Utilites.RandomFloat(0, (int)maxheight);
            return map;
        }
        public static bool[,,] To3d(float[,] map, int height)
        {
            map = Normilize(map);
            bool[,,] map3d = new bool[map.GetLength(0), height + 1, map.GetLength(1)];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int z = 0; z < map.GetLength(1); z++)
                    for (int y = 0; y < map3d.GetLength(1); y++)
                    {
                        if (y <= map[x, z] * height)
                        {
                            map3d[x, y, z] = true;
                        }
                        else
                            map3d[x, y, z] = false;
                    }
            return map3d;
        }

        public static float[,] GenParaboloidHM(int size, int hillSize, int hillsCount) // параболоидная карта высот
        {
            float[,] HeightMap = new float[size, size];

            for (int x = 0; x < size; x++)
                for (int z = 0; z < size; z++)
                    HeightMap[x, z] = 0;

            for (int i = 0; i < hillsCount; i++) // создаём равнины
            {
                int xh = Utilites.RandomInt(1, size - 2);
                int zh = Utilites.RandomInt(1, size - 2);
                float HillHeight = Utilites.RandomInt(1, hillSize * hillSize / 2);
                float r = (float)Math.Sqrt(HillHeight); // вытягивание холмов

                for (int x = 0; x < size; x++)
                    for (int z = 0; z < size; z++)
                        if (Utilites.Distance(new Vector3(xh, zh, 0), new Vector3(x, z, 0)) <= r)
                            HeightMap[x, z] += HelpHeight(x, z, xh, zh) + HillHeight;
            }
            return HeightMap;
        }

        private static float HelpHeight(int x1, int y1, int x2, int y2)
        {
            float b = (float)Math.Pow((x1 - x2), 2);
            float c = (float)Math.Pow((y1 - y2), 2);
            b += c;
            return (b * -1);
        }

        public static float[,] Normilize(this float[,] hm)
        {
            float min = FindMin(hm); // находим самые высокие и самые низкие точки
            float max = FindMax(hm);

            for (int x = 0; x < hm.GetLength(0); x++) // нормализуем высоты
                for (int z = 0; z < hm.GetLength(1); z++)
                    hm[x, z] = (hm[x, z] - min) / (max - min);
            return hm;
        }

        public static float[,] RoundHeightMap(this float[,] hm, int count)
        {
            for (int i = 0; i < count; i++)  // сглаживаем высоты
                for (int x = 0; x < hm.GetLength(0); x++)
                    for (int z = 0; z < hm.GetLength(1); z++)
                        hm[x, z] = (float)Math.Sqrt(hm[x, z]);
            return hm;
        }


        public static float FindMin(float[,] mas)
        {
            float a = mas[0, 0];
            for (int x = 0; x < mas.GetLength(0); x++)
                for (int z = 0; z < mas.GetLength(1); z++)
                {
                    if (mas[x, z] < a)
                        a = mas[x, z];
                }
            return a;
        }

        public static float[,] SetHeight(float[,] hm, float height)
        {
            for (int x = 0; x < hm.GetLength(0); x++)
                for (int y = 0; y < hm.GetLength(1); y++)
                {
                    hm[x, y] *= height;
                }
            return hm;
        }


        public static float[,] GenSimplexNoise(int size, float height)
        {
            SimplexNoise.Noise.Seed = Utilites.RandomInt(10000);
            return SimplexNoise.Noise.Calc2D(size, size, 0.8f);
            float[,] map = new float[size, size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    map[x, y] = SimplexNoise.Noise.CalcPixel2D(x, y, 0.10f);
                    map[x, y] *= height;
                }

            return map;
        }

        public static float FindMax(float[,] mas)
        {
            float a = mas[0, 0];
            for (int x = 0; x < mas.GetLength(0); x++)
                for (int z = 0; z < mas.GetLength(1); z++)
                {
                    if (mas[x, z] > a)
                        a = mas[x, z];
                }
            return a;
        }

        public static float[,] GenIsland(int size, float maxheight)
        {
            return DiamondGenerator.ConvertToIsland(DiamondGenerator.DiamondSquare(maxheight, 50, 0.5f), 5, 5);
        }

        public static float[,] MapFromCaveGenerator(int size, float maxheight)
        {
            float[,] map = new float[size, size];
            bool[,] bmap = CaveGenerator.Generate(size, size, 56);

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (bmap[x, y])
                        map[x, y] = maxheight * 0.5f;
                    else
                        map[x, y] = 0;

            /*
            bmap = CaveGenerator.Generate(size, size, 50);

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (bmap[x, y])
                        if (map[x, y]>0)
                            map[x, y] = maxheight*0.5f;

            bmap = CaveGenerator.Generate(size, size, 45);

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (bmap[x, y])
                        if (map[x, y] > 0)
                            map[x, y] = maxheight * 0.75f;*/

            return map;
        }

    }
}
