using GameEngine.Systems.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Systems.Main
{
    public class UISystem : AbstractSystem
    {
        private static SpriteBatch batch { get => RenderSystem.window.spriteBatch; }

        public static List<UserInterfaceElement> elements = new List<UserInterfaceElement>();

        public static readonly float PanelsLayerUI01 = 0.9f;
        public static readonly float PanelsLayerUI02 = 0.8f;
        public static readonly float PanelsLayerUI03 = 0.7f;

        public override void Initialize()
        {

            elements.Add(new Label());
            elements[0].transform.Position = new Vector3(0, 0, 0);
            ((Label)elements[0]).SetValue("RAM <$! 250,0,0>{0}<$=> Mb\nFPS {1}", "MemoryUse;FramePerSecond", RenderSystem.window);
            ((Label)elements[0]).Scale = 0.5f;
            ((Label)elements[0]).Align = UserInterfaceElement.EAlignUI.leftTop;

            /*
            elements.Add(new TrackBar());
            elements[1].transform.Position = new Vector3(0, 250, 0);
            ((TrackBar)elements[1]).Align = UserInterfaceElement.EAlignUI.leftTop;
            */
        }

        public override void Update()
        {
            foreach (UserInterfaceElement element in elements)
            {
                element.Update();
            }
        }

        internal void Draw()
        {
            RenderSystem.window.BeginMyDraw();

            foreach (UserInterfaceElement element in elements)
            {
                element.Draw();
            }

            //batch.DrawString(ResourseManager.DEFAULT_FONT, ((TrackBar)elements[1]).Value.ToString(), new Vector2(0, 150), new Color(255, 0, 0, 150), 0, new Vector2(), 0.5f, SpriteEffects.None, 0);
            //batch.DrawString(ResourseManager.DEFAULT_FONT, "Game objects " + SystemManager.gameObjects.Count.ToString(), new Vector2(0, 25), Color.White, 0, new Vector2(), 0.5f, SpriteEffects.None, 0);
            //batch.DrawString(ResourseManager.DEFAULT_FONT, $"11 % 2 = { ((11 - (11 % 2)) / 2)}", new Vector2(0, 50), Color.White, 0, new Vector2(), 0.5f, SpriteEffects.None, 0);

            RenderSystem.window.EndMyDraw();
        }
    }
}
