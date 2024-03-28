using Destiny.CoreModules;
using Destiny.CoreModules.ECS.Systems;
using Destiny.CoreModules.ECS.GameObjects;
using SFML.System;
using Destiny.CoreModules.ECS.Components;

namespace GameEngine
{
    /// <summary>
    /// Камера
    /// </summary>
    public static class Camera
    {
        /// <summary>
        /// Позиция камеры
        /// </summary>
        public static Vector2f position = new Vector2f(0,0);
        public static float Speed = 20;
        //public static Vector2i TilePos {get => new Vector2i((int)(position.X / Tile.tileSize), (int)(position.Y / Tile.tileSize)) ; }

        public static void SetFocus(GameObject entity)
        {/*
            if (entity != null)
            if (entity.HasComponentType(new Transform()))
            {
                position.X = (int)(entity.Component<Transform>().Position.X - ((RenderSystem.window.Size.X / Tile.tileSize) - 1) / 2) * Tile.tileSize;
                position.Y = (int)(entity.Component<Transform>().Position.Y - ((RenderSystem.window.Size.Y / Tile.tileSize) - 3) / 2) * Tile.tileSize;
            }*/
        }

        public static void Update()
        {          
            /*
            if (position.X > ((GameCore.map.width + 1) * Tile.tileSize) - RenderSystem.window.Size.X)
                position.X = ((GameCore.map.width) * Tile.tileSize) - RenderSystem.window.Size.X;
            if (position.X < 0)
                position.X = 0;


            if (position.Y > ((GameCore.map.height + 1) * Tile.tileSize) - RenderSystem.window.Size.Y)
                position.Y = ((GameCore.map.height) * Tile.tileSize) - RenderSystem.window.Size.Y;
            if (position.Y < Tile.tileSize)
                position.Y = 0;
                */
        }

        /// <summary>
        /// Приближение камеры
        /// </summary>
        public static float scale = 1;
    }
}
