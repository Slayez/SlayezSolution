using GameEngine.GameObjects;
using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;

namespace GameEngine.GameData
{
    /// <summary>
    /// Камера
    /// </summary>
    public static class Camera
    {
        /// <summary>
        /// Позиция камеры
        /// </summary>
        public static Vector2 position = new Vector2(0, 0);
        public static int Ydepth = 0;
        public static float Speed = 360;

        public static Rectangle DisplayArea { get => new Rectangle(position.ToPoint(), RenderSystem.window.Size.ToPoint()); }
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

        }

        /// <summary>
        /// Приближение камеры
        /// </summary>
        public static float scale = 1;
    }
}
