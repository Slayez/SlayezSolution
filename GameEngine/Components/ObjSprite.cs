using GameEngine.Components.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class ObjSprite : Component
    {
        private string _texture;
        public string Texture { get => _texture; set => _texture = value; }

        public Rectangle? sourceRectangle { get => (_sourceRectangle == null ? ResourseManager.Texture(_texture).Bounds : _sourceRectangle); set => _sourceRectangle = value; }

        private Rectangle? _sourceRectangle = null;

        public SpriteEffects spriteEffect = SpriteEffects.None;

        public Vector2 offset = new Vector2();

        public ObjSprite(string texture, Rectangle? sourceRectangle = null)
        {
            _texture = texture;
            _sourceRectangle = sourceRectangle;
        }

        public ObjSprite()
        {

        }
    }
}
