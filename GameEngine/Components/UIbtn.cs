using System;

namespace GameEngine.Components
{
    public class UIbtn : Component
    {
        /// <summary>
        /// Действие
        /// </summary>
        public Action BtnAction;

        public UIbtn(Action btnAction)
        {
            BtnAction = btnAction;
        }

        public UIbtn()
        {
        }
    }
}
