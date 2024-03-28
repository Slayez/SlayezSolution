using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameEngine.Systems.Main
{
    public static class InputCore
    {

        public static bool activeInput = true;
        public static List<Keys> OldPressedKeys = new List<Keys>() { };
        public static MouseState OldMouseState = new MouseState();
        public static List<Keys> PressedKeys { get => new List<Keys>(Keyboard.GetState().GetPressedKeys()); }
        public static MouseState MouseState { get => Mouse.GetState(); }

        public static Point mousePos { get => MouseState.Position; }
        public static Point oldMousePos;

        public static Point ClickPoint { get => _clickPoint; }
        private static Point _clickPoint = new Point(-1, -1);

        public static bool mouseMoved { get => mousePos != oldMousePos ? true : false; }
        public static float mouseMoveDistance
        {
            get => Utilites.Distance(oldMousePos, mousePos);
        }

        public static string GetChar(this Keys key)
        {
            string charr = "";
            bool caps = false;

            if (key == Keys.Add)
                return "+";
            if (key == Keys.Subtract)
                return "-";
            if (key == Keys.Multiply)
                return "*";
            if (key == Keys.Divide)
                return "/";

            if (key.ToString().Length == 1)
                charr = key.ToString();

            if (key.ToString().Length == 2 & key != Keys.Up)
                charr = key.ToString()[1].ToString();

            if (key.ToString().Contains("NumPad"))
                charr = key.ToString().Substring(6);

            if (Keyboard.GetState().CapsLock)
                caps = !caps;
            if (PressedKeys.Contains(Keys.RightShift) | PressedKeys.Contains(Keys.LeftShift))
                caps = !caps;

            return caps ? charr : charr.ToLower();
        }

        public static float DragTime = 0;
        public const float MinDragTime = 0.15f;
        public static bool Drag { get => ((DragTime > MinDragTime) & (MouseState.LeftButton == ButtonState.Pressed)); }

        public static void refreshState()
        {
            if (OldMouseState.LeftButton == ButtonState.Released)
                DragTime = 0;

            if (OldMouseState.LeftButton == ButtonState.Pressed)
                if (MouseState.LeftButton == ButtonState.Pressed)
                {
                    DragTime += SystemManager.deltaTime;
                }

            if (OldMouseState.LeftButton == ButtonState.Released)
                if (MouseState.LeftButton == ButtonState.Pressed)
                {
                    _LMB_Clicked = true;
                    _clickPoint = mousePos;
                }
                else
                    _LMB_Clicked = false;

            if (OldMouseState.RightButton == ButtonState.Released)
                if (MouseState.RightButton == ButtonState.Pressed)
                {
                    _RMB_Clicked = true;
                    _clickPoint = mousePos;
                }
                else
                    _RMB_Clicked = false;

            OldPressedKeys = new List<Keys>(PressedKeys);
            OldMouseState = MouseState;
            oldMousePos = mousePos;
        }

        public static bool LMB_Pressed { get => MouseState.LeftButton == ButtonState.Pressed; }
        public static bool LMB_Clicked { get => _LMB_Clicked; }
        private static bool _LMB_Clicked = false;
        public static bool LMB_Released { get => MouseState.LeftButton == ButtonState.Released; }


        public static bool RMB_Pressed { get => MouseState.RightButton == ButtonState.Pressed; }
        public static bool RMB_Clicked { get => _RMB_Clicked; }
        private static bool _RMB_Clicked = false;
        public static bool RMB_Released { get => MouseState.RightButton == ButtonState.Released; }

        public static List<Keys> tapKeys()
        {
            List<Keys> a = new List<Keys>();
            foreach (Keys key in PressedKeys)
            {
                if (isKeyOverride(key))
                    a.Add(key);
            }
            return a;
        }

        public static bool isKeyDown(Keys key)
        {
            if (!activeInput)
                return false;
            return PressedKeys.Contains(key);
        }

        public static bool isKeyDownOverride(Keys key)
        {
            return PressedKeys.Contains(key);
        }

        public static bool isKeyOverride(Keys key)
        {
            if (PressedKeys.Contains(key) &&
                !OldPressedKeys.Contains(key))
                return true;
            return false;
        }

        public static bool isKeyTap(Keys key)
        {
            if (!activeInput)
                return false;
            if (PressedKeys.Contains(key) &&
                !OldPressedKeys.Contains(key))
                return true;
            return false;
        }

        public static bool isKeyUp(Keys key)
        {
            if (!activeInput)
                return false;
            return !PressedKeys.Contains(key);
        }
        public static void InputUpdate()
        {
            refreshState();
        }

    }
}
