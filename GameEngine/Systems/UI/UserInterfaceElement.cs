using GameEngine.Components.Main;
using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems.UI
{
    public abstract class UserInterfaceElement : IUserInterfaceElement
    {
        public Transform transform { get => _position; set => _position = value; }
        public EAlignUI Align { get; set; }

        public float DepthLayer { get => _depthLayer; set => _depthLayer = value; }
        private float _depthLayer = 0.5f;
        public bool InFocus { get => _inFocus; set => _inFocus = value; }
        private bool _inFocus = true;
        public bool Active { get => _active; set => _active = value; }
        private bool _active = true;

        public Vector2 GetBound;
        internal SpriteBatch batch { get => RenderSystem.window.spriteBatch; }

        private Transform _position = new Transform(true);
        public enum EAlignUI { none, top, leftTop, rightTop, left, center, right, leftBottom, bottom, rightBottom }
        public abstract void Draw();
        public abstract void Update();
    }
}
