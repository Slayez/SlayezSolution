using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Systems.UI
{
    public class Button : UserInterfaceElement
    {

        private Label TextLabel = new Label();
        public string LabelText { get => TextLabel.Text; set => TextLabel.Text = value; }
        public float LabelScale { get => TextLabel.Scale; set => TextLabel.Scale = value; }

        public EButtonState ButtonState { get; set; }

        private float rectwidth = 4;

        public new Vector2 GetBound { get => TextLabel.GetBound + new Vector2(rectwidth * 2); }
        public enum EButtonState { disabled, normal, mouseover, pressed }

        public Color OutlineColor { get => _outlineColor != new Color(0, 0, 0, 0) ? _outlineColor : ResourseManager.OutlineColor; }
        private Color _outlineColor;
        public Color DisabledColor { get => _disabledColor != new Color(0, 0, 0, 0) ? _disabledColor : ResourseManager.DisabledColor; }
        private Color _disabledColor;
        public Color NormalColor { get => _normalColor != new Color(0, 0, 0, 0) ? _normalColor : ResourseManager.NormalColor; }
        private Color _normalColor;
        public Color MouseOverColor { get => _mouseOverColor != new Color(0, 0, 0, 0) ? _mouseOverColor : ResourseManager.MouseOverColor; }
        private Color _mouseOverColor;
        public Color PressedColor { get => _pressedColor != new Color(0, 0, 0, 0) ? _pressedColor : ResourseManager.PressedColor; }
        private Color _pressedColor;

        public Action ClickAction { get; set; }

        private Color GetFillColor()
        {
            switch (ButtonState)
            {
                case EButtonState.disabled:
                    return DisabledColor;
                case EButtonState.mouseover:
                    return MouseOverColor;
                case EButtonState.normal:
                    return NormalColor;
                case EButtonState.pressed:
                    return PressedColor;
            }
            return DisabledColor;
        }

        public override void Draw()
        {
            Rectangle rectangle = new Rectangle((transform.Position.Add(Align.ToVector(GetBound)).Add(new Vector2(-rectwidth))).ToPoint(), (GetBound).ToPoint());
            RenderSystem.window.spriteBatch.DrawRectangleWithOutline(rectangle, GetFillColor(), OutlineColor, 2, DepthLayer);
            TextLabel.Draw(this.GetBound);
        }

        public override void Update()
        {
            TextLabel.transform = this.transform;
            TextLabel.DepthLayer = this.DepthLayer - 0.001f;
            TextLabel.Align = this.Align;
            TextLabel.Update();
            if (Active)
            {
                if (InFocus)
                {
                    if (Utilites.PointInRectangle(InputCore.mousePos.ToVector2(), new Rectangle((transform.Position.Add(Align.ToVector(GetBound)).Add(new Vector2(-rectwidth))).ToPoint(), (GetBound).ToPoint())))
                    {
                        if (InputCore.LMB_Pressed)
                            if (Utilites.PointInRectangle(InputCore.ClickPoint.ToVector2(), new Rectangle((transform.Position.Add(Align.ToVector(GetBound)).Add(new Vector2(-rectwidth))).ToPoint(), (GetBound).ToPoint())))
                            {
                                ButtonState = EButtonState.pressed;
                            }

                        if (ButtonState == EButtonState.pressed)
                        {
                            if (InputCore.LMB_Released)
                            {
                                ClickAction?.Invoke();
                            }
                        }

                        if (InputCore.LMB_Released)
                            ButtonState = EButtonState.mouseover;
                    }
                    else
                        ButtonState = EButtonState.normal;
                }
                else
                    ButtonState = EButtonState.normal;

            }
            else
            {
                ButtonState = EButtonState.disabled;
            }
        }
    }
}
