using SFML.Graphics;
using SFML.System;

namespace GameEngine.Components
{
    public class UIpanel : Component
    {
        private FloatRect _rect;
        public FloatRect Reactangle { get => _rect; set => _rect = value; }
        public UIpanel()
        {
            _rect = new FloatRect(0, 0, 100, 50);
        }
        public UIpanel(FloatRect rect)
        {
            _rect = rect;
        }

        public UIpanel(Vector2f pos, Vector2f size)
        {
            _rect = new FloatRect(pos, size);
        }

        public UIpanel(float x, float y, float width, float height)
        {
            _rect = new FloatRect(x, y, width, height);
        }
    }
}
