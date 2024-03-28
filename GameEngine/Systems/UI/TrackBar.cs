using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems.UI
{
    public class TrackBar : UserInterfaceElement
    {

        public new Vector2 GetBound { get => new Vector2(_width + _trackSize.X, Utilites.Max(_trackSize.Y, _height)); }
        public float Value { get => _value; set => _value = (value - (value % _step)) / _step; }
        private float _value = 10;
        public float MinValue { get => _minValue; set => _minValue = value < _maxValue ? value : _maxValue - _step; }
        private float _minValue = 0;
        public float MaxValue { get => _maxValue; set => _maxValue = value > _minValue ? value : _maxValue + _step; }
        private float _maxValue = 50;
        public uint Step { get => _step; set => _step = value; }
        public uint Steps { get => (uint)((_maxValue - _minValue) / Step); }
        private uint _step = 1;

        public Color StepColor { get => _stepColor != new Color(0, 0, 0, 0) ? _stepColor : Color.Black; }
        private Color _stepColor;
        public Color TrackColor { get => _trackColor != new Color(0, 0, 0, 0) ? _trackColor : ResourseManager.DisabledColor; }
        private Color _trackColor;
        public Color BarColor { get => _barColor != new Color(0, 0, 0, 0) ? _barColor : ResourseManager.NormalColor; }
        private Color _barColor;

        public Vector2 TrackSize { get => _trackSize; set => _trackSize = value; }
        private Vector2 _trackSize = new Vector2(8, 32);
        public Vector2 Size { get => new Vector2(_width, _height); set => value.SetFromVector(out _width, out _height); }
        public float _height = 12;
        public float _width = 300;
        private void CheckValues()
        {
            Value = _value;
            MinValue = _minValue;
            MaxValue = _maxValue;
            _value = _value < _minValue ? _minValue : _value;
            _value = _value > _maxValue ? _maxValue : _value;
        }

        private Rectangle track
        {
            get =>
            new Rectangle
            ((new Vector2((Align.ToVector(GetBound).X - (_trackSize.X / 2)) + ((_value - _minValue) / (_maxValue - _minValue) * _width),
            Align.ToVector(GetBound).Y + Align.ToVector(GetBound).Y / 2 /*+ _trackSize.Y / 2*/) + transform.Position.To2d()).ToPoint(), _trackSize.ToPoint());
        }

        public override void Draw()
        {
            Vector2 zeropos = Align.ToVector(GetBound) + transform.Position.To2d();
            Vector2 barpos = new Vector2(zeropos.X, zeropos.Y + GetBound.Y / 2 - _height / 2);
            Rectangle bar = new Rectangle(barpos.ToPoint(), Size.ToPoint());
            batch.DrawRectangleWithOutline(bar, BarColor, StepColor, 1, DepthLayer);
            for (int i = 1; i < Steps; i++)
            {
                Vector2 linepos = barpos + new Vector2((float)i / (float)Steps * _width, 2);
                batch.DrawRectangle(new Rectangle(linepos.ToPoint(), new Point(1, (int)_height - 4)), StepColor, 0, DepthLayer - 0.0004f);
                //batch.DrawLine(linepos, linepos + new Vector2(0,_height), StepColor, 1, DepthLayer - 0.0004f);
            }
            batch.DrawRectangleWithOutline(track, TrackColor, StepColor, 1, DepthLayer - 0.0006f);

        }

        private bool beginDrag = false;
        public override void Update()
        {
            if (InputCore.activeInput)
                if (InFocus)
                {
                    if (Active)
                    {
                        if (Utilites.PointInRectangle(InputCore.mousePos.ToVector2(), track))
                        {
                            if (InputCore.LMB_Clicked)
                                beginDrag = true;
                        }
                        if (beginDrag)
                        {
                            Value = ((InputCore.mousePos.X - (Align.ToVector(GetBound).X + transform.Position.X)) / _width) * (_maxValue - _minValue);

                        }
                        if (InputCore.LMB_Released)
                            beginDrag = false;
                    }
                }
            CheckValues();
        }
    }
}
