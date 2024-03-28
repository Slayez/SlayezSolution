using GameEngine.Components;
using GameEngine.Components.Main;
using GameEngine.Dictionarys;
using GameEngine.GameData;
using GameEngine.GameObjects;
using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static GameEngine.Dictionarys.Factions;

namespace GameEngine.Systems
{
    public class MapControlSystem : AbstractSystem
    {

        public static Tile[,,] _currentMap = new Tile[35, 35, 15];

        public static Vector3 WorldSize { get => new Vector3(_currentMap.GetLength(0), _currentMap.GetLength(1), _currentMap.GetLength(2)); }

        public static Point WorldOrigin = new Point((int)(WorldSize.X / 2), 5);
        static public Tile? GetTile(int x, int y, int z)
        {
            if (x < _currentMap.GetLength(0) & x >= 0)
                if (y < _currentMap.GetLength(1) & y >= 0)
                    if (z < _currentMap.GetLength(2) & z >= 0)
                        return (_currentMap[x, y, z]);
            return null;
        }

        static public Tile? GetTile(Vector3 p)
        {
            return GetTile((int)p.X, (int)p.Y, (int)p.Z);
        }

        public static Vector3 GetSelTile()
        {
            Vector3 selTile = new Vector3(-1, -1, -1);
            Vector3 step = new Vector3(Utilites.GetIsoPos(InputCore.mousePos + Camera.position.ToPoint()).ToVector2(), 0);

            for (int z = 0; z <= Camera.Ydepth; z++)
            {
                if (MapControlSystem.GetTile((int)step.X, (int)step.Y, (int)step.Z) != null)
                    selTile = step;
                step += new Vector3(1, 1, 1);
            }

            return selTile;
        }

        public static int GetMaxHight()
        {
            int maxhight = 0;
            for (int x = 0; x < _currentMap.GetLength(0); x++)
                for (int y = 0; y < _currentMap.GetLength(1); y++)
                    for (int z = 0; z < _currentMap.GetLength(2); z++)
                        if (_currentMap[x, y, z] != null)
                        {
                            if (z > maxhight)
                            maxhight = z;
                        }
            return maxhight;
        }

        public static void BakeIllumination(int? maxhight = null)
        {
            float lightlost = 0.20f;

            if (maxhight == null)
                maxhight = GetMaxHight();

            for (int x = 0; x < _currentMap.GetLength(0); x++)
                for (int y = 0; y < _currentMap.GetLength(1); y++)
                    for (int z = 0; z < _currentMap.GetLength(2); z++)
                        if (_currentMap[x, y, z] != null)
                        {
                            /*
                            lightlost = 0.05f;
                            lightlost += (float)Utilites.RandomInt(0, 50) / 100;
                            */
                            int ill = (int)(255 - ((maxhight - z) * lightlost * 255));
                            if (ill > 255)
                                ill = 255;
                            if (ill < 0)
                                ill = 0;
                            _currentMap[x, y, z].illumination = new Color((byte)ill, (byte)ill, (byte)ill, (byte)255);
                        }
        }

        public static float GetLayer(Vector3 v3)
        {
            return GetLayer(v3.X, v3.Y, v3.Z);
        }

        public static float GetLayer(float x, float y, float z)
        { 
        return 0.5f - y * 0.001f - x * 0.001f - z * 0.002f;
        }

        public override void Initialize()
        {
            GenTestMap();
            GameObject go = new GameObject(
            "test_player",
            new List<Component>
            {
                    new Transform(new Vector3(0, 0, 0)),
                    new PlayerController(true),
                    new Stats(),
                    new ObjSprite("Human_Idle"),
                    new Animation("Human", new Dictionary<string, int>{{"Idle",8},{"Run",8}})
            }

            );
            go.Component<ObjSprite>().offset = new Vector2(-43, -25);
            //go.Component<Animation>()._currentState = "Run";
            go.CreateInWorld();

        }

        public static void GenTestMap()
        {
            _currentMap = new Tile[35, 35, 15];

            //bool[,] _temp = Generators.CaveGenerator.Generate(_currentMap.GetLength(0), _currentMap.GetLength(1), 50);


            float[,] _temp2 = Generators.HeightMap.GenIsland(_currentMap.GetLength(0), 5);



            for (int x = 0; x < _currentMap.GetLength(0); x++)
                for (int z = 0; z < _currentMap.GetLength(1); z++)
                    for (int y = 0; y < _currentMap.GetLength(2); y++)
                    {

                        //_currentMap[x, z, y] = new Tile(0);

                        int id = 1;

                        if (y > 0)
                            id = 0;

                        if (_temp2[x, z] >= y)
                            _currentMap[x, z, y] = new Tile(id);
                    }
            Camera.Ydepth = GetMaxHight();
            BakeIllumination();
            UpdateTilesPostfix();
        }

        public static void UpdateTilesPostfix()
        {
            for (int x = 0; x < _currentMap.GetLength(0); x++)
                for (int z = 0; z < _currentMap.GetLength(1); z++)
                    for (int y = 0; y < _currentMap.GetLength(2); y++)
                        if (GetTile(new Vector3(x, z, y)) != null)
                            _currentMap[x, z, y].postfix = GetTilePostfix(new Vector3(x, z, y));
        }

        public static string? GetTilePostfix(Vector3 pos)
        {
            string postfix = "000";
            if (GetTile(pos + new Vector3(0, 0, 1)) != null)
                postfix = $"1{postfix[1]}{postfix[2]}";
            if (GetTile(pos + new Vector3(0, 1, 0)) != null)
                postfix = $"{postfix[0]}1{postfix[2]}";
            if (GetTile(pos + new Vector3(1, 0, 0)) != null)
                postfix = $"{postfix[0]}{postfix[1]}1";

            if (postfix == "000")
                return null;
            return "_"+postfix;
        }
    }
}
