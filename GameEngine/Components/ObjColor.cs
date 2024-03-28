using GameEngine.Components.Main;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class ObjColor : Component
    {
        private Color _fillColor;

        private Color _outlineColor;

        private float _outlineThickness;
        public float OutlineThickness { get => _outlineThickness; set => _outlineThickness = value; }
        public Color FillColor { get => _fillColor; set => _fillColor = value; }
        public Color OutlineColor { get => _outlineColor; set => _outlineColor = value; }

        public ObjColor(Color fillColor, Color outlineColor)
        {
            _fillColor = fillColor;
            _outlineColor = outlineColor;
        }
        public ObjColor(Color fillColor)
        {
            _fillColor = fillColor;
            _outlineColor = Color.White;
        }

        public ObjColor()
        {
            _fillColor = Color.White;
            _outlineColor = Color.White;
        }

    }
}
