namespace GameEngine.Generators
{
    public static class OreGenerator
    {

        public static bool[,] GenerateOreCluster(int width, int height, byte density, int minOre, int maxOre)
        {
            bool[,] cluster;
            cluster = CaveGenerator.Generate(width, height, density);

            while ((StructureInstruments.Count(cluster) > maxOre) | (StructureInstruments.Count(cluster) < minOre))
            {
                cluster = CaveGenerator.Generate(width, height, density);
                density--;
            }
            return cluster;
        }

        //public static int SpawnOre(Vector2 pos, int minOre, int maxOre, string prefab)
        //{
        //   return StructureInstruments.CreateOre(pos, GenerateOreCluster((int)(maxOre), (int)(maxOre), 55, minOre, maxOre), prefab);
        //}
    }
}
