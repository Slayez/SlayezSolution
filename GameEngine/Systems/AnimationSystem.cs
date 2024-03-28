using GameEngine.Components;
using GameEngine.GameObjects;
using GameEngine.Systems.Main;

namespace GameEngine.Systems
{
    class AnimationSystem : AbstractSystem
    {
        public override void Update()
        {
            foreach (GameObject obj in SystemManager.gameObjects)
                if (obj.HasComponentType(typeof(ObjSprite)))
                    if (obj.HasComponentType(typeof(Animation)))
                        if (obj.Component<Animation>().AnimationsStates.Count > 0)
                        {
                            if (obj.Component<Animation>()._currentFrame >= obj.Component<Animation>()._frameCount - 1)
                            {
                                obj.Component<Animation>()._currentFrame = 0;
                            }

                            if (obj.Component<Animation>().AnimationTimer <= 0)
                            {
                                obj.Component<Animation>().AnimationTimer = obj.Component<Animation>().FrameTime;
                                obj.Component<Animation>()._currentFrame++;
                                obj.Component<ObjSprite>().Texture = obj.Component<Animation>()._textureName;
                                obj.Component<ObjSprite>().sourceRectangle = obj.Component<Animation>().sourceRectangle;
                            }

                            obj.Component<Animation>().AnimationTimer -= SystemManager.deltaTime;
                        }

        }
    }
}
