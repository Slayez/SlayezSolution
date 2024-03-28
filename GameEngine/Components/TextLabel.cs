using GameEngine.Dictionarys;

namespace GameEngine.Components
{
    public class TextLabel : Component
    {
        private string _text;
        private float _characterSize;
        private string _font;
        public string Text { get => _text; set => _text = value; }
        public float CharacterSize { get => _characterSize; set => _characterSize = value; }
        public string Font { get => _font; set => _font = value; }

        public TextLabel(string text, float characterSize = 14, string font = ResourseManager.DEFAULT_FONT)
        {
            _text = text;
            _characterSize = characterSize;
            _font = font;
        }
    }
}
