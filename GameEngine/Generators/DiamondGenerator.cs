using System;

namespace GameEngine.Generators
{
    public static class DiamondGenerator
    {
        public static float[,] GenCircleGradient(int size, float gradientP, float radius)
        {
            float[,] a = new float[size, size];
            int midPoint = (int)(a.GetLength(0) - 1) / 2;
            float circleSize = size / radius;
            for (int x = 0; x < a.GetLength(0); x++)
                for (int y = 0; y < a.GetLength(0); y++)
                {
                    if ((Utilites.Distance(midPoint, midPoint, x, y)) >= circleSize)
                        a[x, y] = circleSize / (Utilites.Distance(midPoint, midPoint, x, y));
                    else
                    if ((Utilites.Distance(midPoint, midPoint, x, y)) <= gradientP)
                        a[x, y] = 2;
                    else
                    if ((Utilites.Distance(midPoint, midPoint, x, y)) > gradientP)
                        a[x, y] = 1 - ((Utilites.Distance(midPoint, midPoint, x, y)) - gradientP) / (circleSize - gradientP) + 1;
                }
            return a;
        }

        // чем больше число islandSize тем меньше остров
        public static float[,] ConvertToIsland(float[,] hm, float highArea, float islandSize)
        {
            float[,] a = GenCircleGradient(hm.GetLength(0), highArea, islandSize);

            for (int x = 0; x < hm.GetLength(0); x++)
                for (int y = 0; y < hm.GetLength(0); y++)
                {

                    hm[x, y] *= a[x, y];
                }
            return hm;
        }




        /*
         * the array where the terrain values are stored
         */


        /*
         * heuristic variables.  This are the values that effect the various aspects of the generatred terrain
         * this are public vars thus are visible in the inspector and the inspector values will override the values here
         */

        /*
         * this will create a map of size*size
         * size must be a value of 2^n+1 (power of 2 plus 1) to use with the diamond-square algorithm
         * thus valid values or size are 3,9,17,33,65,129,257,513,1025,4097 etc
         */

        /*
         * This method uses the "Diamond-square" approach to randomly generate a terrain of size*size
         */
        public static float[,] DiamondSquare(float maxHeight, int Iterations, float Smoothing)
        {
            int size = 65;


            float seedValue = maxHeight; //200    // higher values will generate terrain with higher maximum heights
            float delta;                // how much the terrain height can change when interpolating. generally setting this value to 1/2 of seed value is good. lower than 1/2 will result in spikier terrain
            int iterations = Iterations;    //50     // the number of smoothing passes to make over the terrain. higher numbers will result in smoother (and flatter) terrain
            float smoothing = Smoothing;  //0.5f   // the 'bumpiness' and smoothness from tile to tile. good ranges are from 0 to 1.0f with 0.5f being a good middle ground. higher values create significantly spikier terrain.

            float[,] terrain = new float[size, size];
            /*
             * generally a delta value of 1/2 the seed value produces good results.
             * delta < 1/2 of seed will produce flatter terrain and visa versa
             */
            delta = seedValue / 2;


            terrain[0, 0] = 0;
            terrain[0, size - 1] = 0;
            terrain[size - 1, 0] = 0;
            terrain[size - 1, size - 1] = 0;

            /*
             * Set the corners of our terrain based on a random range of our initial seed value
             */
            terrain[0, 0] = Utilites.RandomFloat(0, seedValue);
            terrain[0, size - 1] = Utilites.RandomFloat(0, seedValue);
            terrain[size - 1, 0] = Utilites.RandomFloat(0, seedValue);
            terrain[size - 1, size - 1] = Utilites.RandomFloat(0, seedValue);

            int length = size - 1;

            /*
             * make multiple passes over the entire terrain
             */
            for (int i = 1; i <= iterations; i++)
            {
                int x = length / 2;
                int z = length / 2;
                int d = length / 2;
                int pass = 1;

                /*
                 * square pass
                 */
                while (z < size)
                {
                    while (x < size)
                    {
                        float one = 0;
                        float two = 0;
                        float three = 0;
                        float four = 0;

                        bool mod1 = false;
                        bool mod2 = false;
                        bool mod3 = false;
                        bool mod4 = false;

                        /*
                         * we need to be careful at the map edges to we don't walk off the ends of the array
                         */
                        if (x - length / 2 < 0 && z - length / 2 < 0)
                        {
                            one = terrain[size - x - length / 2, size - z - length / 2];

                            mod1 = true;
                        }
                        else if (x - length / 2 < 0 && z + length / 2 >= size)
                        {
                            four = terrain[size - x - length / 2, z + length / 2 - size];

                            mod4 = true;
                        }
                        else if (x + length / 2 >= size && z + length / 2 >= size)
                        {
                            three = terrain[x + length / 2 - size, z + length / 2 - size];

                            mod3 = true;
                        }
                        else if (x + length / 2 >= size && z - length / 2 < 0)
                        {
                            two = terrain[x + length / 2 - size, size - z - length / 2];

                            mod2 = true;
                        }
                        else if (x - length / 2 < 0)
                        {
                            one = terrain[size - x - length / 2, z - length / 2];
                            four = terrain[size - x - length / 2, z + length / 2];

                            mod1 = true;
                            mod4 = true;
                        }
                        else if (x + length / 2 >= size)
                        {
                            two = terrain[x + length / 2 - size, z - length / 2];
                            three = terrain[x + length / 2 - size, z + length / 2];

                            mod2 = true;
                            mod3 = true;
                        }
                        else if (z - length / 2 < 0)
                        {
                            one = terrain[x - length / 2, size - z - length / 2];
                            two = terrain[x + length / 2, size - z - length / 2];

                            mod1 = true;
                            mod2 = true;
                        }
                        else if (z + length / 2 >= size)
                        {
                            three = terrain[x + length / 2, z + length / 2 - size];
                            four = terrain[x - length / 2, z + length / 2 - size];

                            mod3 = true;
                            mod4 = true;
                        }
                        /* 
                         * we are not at the edge of the map
                         */
                        else
                        {
                            one = terrain[x - length / 2, z - length / 2];
                            two = terrain[x + length / 2, z - length / 2];
                            three = terrain[x + length / 2, z + length / 2];
                            four = terrain[x - length / 2, z + length / 2];
                        }

                        if (!mod1)
                            one = terrain[x - length / 2, z - length / 2];
                        if (!mod2)
                            two = terrain[x + length / 2, z - length / 2];
                        if (!mod3)
                            three = terrain[x + length / 2, z + length / 2];
                        if (!mod4)
                            four = terrain[x - length / 2, z + length / 2];

                        terrain[x, z] = (one + two + three + four) / 4 + Utilites.RandomFloat(-delta, delta);

                        x += length;
                    }
                    z += length;
                    x = length / 2;

                    pass++;
                }

                x = length / 2;
                z = 0;
                pass = 1;

                /*
                 * diamond pass
                 */
                while (z < size)
                {
                    while (x < size)
                    {
                        {
                            terrain[x, z] = (terrain[x, Math.Abs(z - length / 2)] +
                                        terrain[Math.Abs(x - length / 2), z] +
                                        terrain[(x + length / 2) % size, z] +
                                        terrain[x, (z + length / 2) % size]) / 4
                                        + Utilites.RandomFloat(-delta, delta);
                        }
                        x += length;
                    }
                    z += length / 2;
                    if (pass % 2 == 1)
                        x = 0;
                    else
                        x = length / 2;

                    pass++;
                }

                length /= 2;
                d /= 2;
                if (length <= 1)
                    length = 2;
                if (d < 1)
                    d = 1;

                delta *= smoothing;
            }
            return terrain;
        }

        /*
         * Writes the contents of the newly generated map array out to disk
         * Storage requirements are size*size*4 bytes
         * Thus a size=1029 map will consume approx. 4 mb of disk space (1029x1029x4 = 4,235,364 bytes)
         * The file will be stored in the root directory of the unity project
         */
    }

}