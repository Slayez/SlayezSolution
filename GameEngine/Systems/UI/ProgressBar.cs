using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems.UI
{
    public class ProgressBar : UserInterfaceElement
    {

        public float Value { get => Link == null ? 0 : (float)((Link.GetType().GetProperty(ValueParam)).GetValue(Link)); }
        public float maxValue { get => Link == null ? 0 : (float)((Link.GetType().GetProperty(maxValueParam)).GetValue(Link)); }

        public new Vector2 GetBound { get => _size; }
        public float Progress { get => Value / maxValue; }

        public Vector2 Size { get => _size; set => _size = value; }
        private Vector2 _size = new Vector2(250, 25);

        public int OutlineThickness { get => _outlineThickness; set => _outlineThickness = value; }
        private int _outlineThickness = 6;

        private string ValueParam;
        private string maxValueParam;
        private object Link;
        public void SetValue(string val, string maxVal, object link)
        {
            Link = link;
            ValueParam = val;
            maxValueParam = maxVal;
        }

        public override void Draw()
        {
            // фон аутлайн
            RenderSystem.window.spriteBatch.DrawRectangle(new Rectangle((transform.Position.To2d() + Align.ToVector(GetBound)).ToPoint(), Size.ToPoint()), new Color(150, 150, 150), 0, DepthLayer);
            // не заполненная
            RenderSystem.window.spriteBatch.DrawRectangle(new Rectangle((transform.Position.To2d() + new Vector2(_outlineThickness / 4) + Align.ToVector(GetBound)).ToPoint(), (Size.Add(_outlineThickness / 2 * -1)).ToPoint()), new Color(200, 200, 200), 0, DepthLayer);
            // заполненная
            RenderSystem.window.spriteBatch.DrawRectangle(new Rectangle((transform.Position.To2d() + new Vector2(_outlineThickness / 4) + Align.ToVector(GetBound)).ToPoint(), (new Vector2(Size.Add(_outlineThickness / 2 * -1).X * Progress, Size.Add(_outlineThickness / 2 * -1).Y)).ToPoint()), new Color(200, 50, 50), 0, DepthLayer);
        }

        public override void Update()
        {
            //throw new NotImplementedException();
            //Вся логика при вызове параметров
        }
    }
}
