using GameEngine.Components.Main;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Components
{
    class Animation : Component
    {

        public string TexturePrefix;

        public String _currentState;

        public int _currentFrame = 0;

        public float AnimationTimer;

        public float FrameTime;

        public string _textureName { get => $"{TexturePrefix}_{_currentState}"; }

        // название стейта и кол-во кадров
        public Dictionary<String, int> AnimationsStates = new Dictionary<String, int>();

        public int _frameCount { get => AnimationsStates[_currentState]; }

        public Rectangle sourceRectangle { get => new Rectangle(new Point(FrameSize.X * _currentFrame, 0), FrameSize); }

        public Animation(string texturePrefix, Dictionary<string, int> animationsStates, float frameTime = 0.08f)
        {
            TexturePrefix = texturePrefix;
            AnimationsStates = animationsStates;
            _currentState = animationsStates.Keys.First();
            FrameTime = frameTime;
            AnimationTimer = FrameTime;
            Point p = new Point(1200, 50);
            //ResourseManager.Texture(_textureName).Bounds.Size;
            FrameSize = new Point(p.X / animationsStates.Values.First(), p.Y);
        }

        private Point FrameSize;
    }
}
