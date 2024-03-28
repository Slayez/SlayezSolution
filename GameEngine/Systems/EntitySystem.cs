using GameEngine.Components;
using GameEngine.Components.Main;
using GameEngine.GameObjects;
using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using static GameEngine.Utilites;

namespace GameEngine.Systems
{
    public class EntitySystem : AbstractSystem
    {
        public static float PushForse = 1;
        public static float physicUpdate = 0.05f;
        private float timer = physicUpdate;

        public FloatRect objRect = new FloatRect();
        public FloatRect otherRect = new FloatRect();

        public void SetObjRect(GameObject obj)
        {
            objRect.Left = obj.Component<Components.Main.Transform>().Position.X;
            objRect.Top = obj.Component<Components.Main.Transform>().Position.Y;
            objRect.Width = obj.Component<Entity>().Size;
            objRect.Height = obj.Component<Entity>().Size;
        }

        public override void Update()
        {
            foreach (GameObject obj in SystemManager.gameObjects)
                if (obj.HasComponentType(typeof(Transform)))
                    if (obj.HasComponentType(typeof(ObjSprite)))
                        if (obj.HasComponentType(typeof(PlayerController)))
                        if (obj.HasComponentType(typeof(Animation)))
                            if (obj.Component<PlayerController>()._target != null)
                    {
                            float objSpeed = 5 * SystemManager.deltaTime;

                                if (Utilites.Distance(obj.Component<PlayerController>()._target.Value, obj.Component<Transform>().Position) <= objSpeed)
                                {
                                    obj.Component<Transform>().Position = obj.Component<PlayerController>()._target.Value;
                                    obj.Component<PlayerController>()._target = null;
                                    obj.Component<Animation>()._currentState = "Idle";
                                }
                                else
                                {
                                    Vector3 forward = obj.Component<PlayerController>()._target.Value - obj.Component<Transform>().Position;
                                    forward.Normalize();
                                    obj.Component<Transform>().Position += forward * objSpeed;
                                    obj.Component<Animation>()._currentState = "Run";

                                    if (forward.X < forward.Y)
                                    {
                                            obj.Component<ObjSprite>().spriteEffect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                                    }
                                    else
                                            obj.Component<ObjSprite>().spriteEffect = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
                                    }
                    }
        }

        /*
        public override void Update()
        {
            if (timer <= 0)
            {
                foreach (GameObject obj in SystemManager.System<GameObjectManager>().gameObjects)
                {
                    if (obj.HasComponentType(new Entity(Factions.GetFaction(0))))
                        if (!obj.Component<Entity>().Ghost)
                        {
                            // Столкновение с другими сущностями
                            foreach (GameObject other in SystemManager.System<GameObjectManager>().gameObjects)
                                if (other != obj)
                                    if (other.HasComponentType(new Entity(Factions.GetFaction(0))))
                                        if (!other.Component<Entity>().Ghost)
                                        {
                                            if (obj.HasComponentType(new BasicAI()) & other.HasComponentType(new BasicAI()) & obj.Component<Entity>().Faction == other.Component<Entity>().Faction)
                                                if (obj.Component<BasicAI>().HasTarget & other.Component<BasicAI>().HasTarget)
                                                    continue;
                                            if (Utilites.Distance(obj.Component<Components.Main.Transform>().Position, other.Component<Components.Main.Transform>().Position) <= obj.Component<Entity>().Size)
                                            {
                                                SetRectsObj(obj, other);

                                                while (Utilites.Intersect(objRect, otherRect))
                                                {
                                                    PushObj(obj, other);
                                                    SetRectsObj(obj, other);
                                                    //if (Engine.frameTime >= 0.05f)
                                                    //    break;
                                                }
                                            }
                                        }
                            if (obj.HasComponentType(new BasicAI()))
                                if (obj.Component<BasicAI>().HasTarget)
                                    continue;
                            // Столкновение с блоками
                            SetObjRect(obj);
                            Vector2i tilePos = Utilites.GetTilePos(objRect.Center());
                            CheckCollision(obj, tilePos);
                            CheckCollision(obj, tilePos + Utilites.Bottom);
                            CheckCollision(obj, tilePos + Utilites.Left);
                            CheckCollision(obj, tilePos + Utilites.Top);
                            CheckCollision(obj, tilePos + Utilites.Right);
                            CheckCollision(obj, tilePos + Utilites.BottomRight);
                            CheckCollision(obj, tilePos + Utilites.TopRight);
                            CheckCollision(obj, tilePos + Utilites.BottomLeft);
                            CheckCollision(obj, tilePos + Utilites.TopLeft);
                        }
                    //if (Engine.frameTime >= 0.05f)
                    //    break;
                }
                timer = physicUpdate;
            }
            else
            {
                //timer -= Engine.deltaTime;
            }
        }
        /*
        private bool CheckCollision(GameObject obj, Vector2i tilePos)
        {
            if (GameCore.map.IsSolid(tilePos))
            {
                SetRectsObjTile(obj, tilePos);
                while (Utilites.Intersect(objRect, otherRect))
                {
                    if (!GameCore.map.IsSolid(tilePos + Utilites.Left))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.Left.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else
                    if (!GameCore.map.IsSolid(tilePos + Utilites.Right))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.Right.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else
                    if (!GameCore.map.IsSolid(tilePos + Utilites.Top))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.Top.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else
                    if (!GameCore.map.IsSolid(tilePos + Utilites.Bottom))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.Bottom.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else
                    if (!GameCore.map.IsSolid(tilePos + Utilites.TopLeft))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.TopLeft.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else
                    if (!GameCore.map.IsSolid(tilePos + Utilites.TopRight))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.TopRight.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else
                    if (!GameCore.map.IsSolid(tilePos + Utilites.BottomRight))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.BottomRight.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else
                    if (!GameCore.map.IsSolid(tilePos + Utilites.BottomLeft))
                    {
                        obj.Component<Components.Transform>().Position += Utilites.BottomLeft.Multiply(PushForse).Multiply(Engine.deltaTime);
                    }
                    else return true; // Stacked


                    SetRectsObjTile(obj, tilePos);

                    if (Engine.frameTime >= 0.05f)
                        break;
                }
            }
            return false;
        }

        private void SetRectsObjTile(GameObject obj, Vector2i tilePos)
        {
            objRect.Left = obj.Component<Components.Transform>().Position.X;
            objRect.Top = obj.Component<Components.Transform>().Position.Y;
            objRect.Width = obj.Component<Entity>().Size;
            objRect.Height = obj.Component<Entity>().Size;

            otherRect.Left = tilePos.X * Tile.width;
            otherRect.Top = tilePos.Y * Tile.height;
            otherRect.Width = Tile.width;
            otherRect.Height = Tile.height;
        }

        private void SetRectsObj(GameObject obj, GameObject other)
        {
            objRect.Left = obj.Component<Components.Transform>().Position.X;
            objRect.Top = obj.Component<Components.Transform>().Position.Y;
            objRect.Width = obj.Component<Entity>().Size;
            objRect.Height = obj.Component<Entity>().Size;

            otherRect.Left = other.Component<Components.Transform>().Position.X;
            otherRect.Top = other.Component<Components.Transform>().Position.Y;
            otherRect.Width = other.Component<Entity>().Size;
            otherRect.Height = other.Component<Entity>().Size;
        }

        private void PushObj(GameObject obj, GameObject other)
        {
            obj.Component<Components.Transform>().Position += Utilites.Normalize(obj.Component<Components.Transform>().Position - other.Component<Components.Transform>().Position).Multiply(PushForse).Multiply(Engine.deltaTime);
            other.Component<Components.Transform>().Position += Utilites.Normalize(other.Component<Components.Transform>().Position - obj.Component<Components.Transform>().Position).Multiply(PushForse).Multiply(Engine.deltaTime);
        }

    */
    }
}
