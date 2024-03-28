using GameEngine.Components;
using GameEngine.GameObjects;
using GameEngine.Systems.Main;
using static GameEngine.Utilites;

namespace GameEngine.Systems
{
    public class AISystem : AbstractSystem
    {
        public override void Update()
        {

            foreach (GameObject obj in SystemManager.gameObjects)
                if (obj.HasComponentType(typeof(BasicAI)))
                    if (obj.Component<BasicAI>().HasTarget)
                    {
                        FloatRect objBody = new FloatRect(obj.Component<Components.Main.Transform>().Position, obj.Component<Entity>().GetBodySize);
                        /*objBody.Left = +objBody.Width / 2;
                        objBody.Top = +objBody.Height / 2;
                        objBody.Width /= 2;
                        objBody.Height /= 2;*/
                        //if (Utilites.Distance(obj.Component<BasicAI>().TargetPoint(), objBody.Center) < objBody.Width)
                        //{
                        //    if (Utilites.Distance(obj.Component<Components.Main.Transform>().Position.To2d(), obj.Component<BasicAI>().TargetPoint()) < objBody.Width / 2)
                        //        obj.Component<Components.Main.Transform>().Position = new Vector3(obj.Component<BasicAI>().TargetPoint(), obj.Component<Components.Main.Transform>().Position.Z);
                        //    obj.Component<BasicAI>().NextTarget();
                        //}
                        //if (Utilites.Distance(obj.Component<Components.Main.Transform>().Position, obj.Component<Components.Main.Transform>().Position +
                        //    Utilites.Normalize(obj.Component<BasicAI>().TargetPoint() - objBody.Center).Multiply(obj.Component<BasicAI>().Speed).Multiply(SystemManager.deltaTime)) < obj.Component<BasicAI>().Speed * 0.35f)
                        //    obj.Component<Components.Main.Transform>().Position +=
                        //        Utilites.Normalize(obj.Component<BasicAI>().TargetPoint() - objBody.Center).Multiply(obj.Component<BasicAI>().Speed).Multiply(SystemManager.deltaTime);

                        //if (obj.Component<BasicAI>().GetPath.Count > 2)
                        //    obj.Component<Components.Transform>().Rotation = Utilites.GetRotation(obj.Component<BasicAI>().TargetPoint() - obj.Component<BasicAI>().GetPath[1]);
                        //else
                        if (obj.Component<BasicAI>().HasTarget)
                            obj.Component<Components.Main.Transform>().Rotation = Utilites.GetRotation(objBody.Center - obj.Component<BasicAI>().TargetPoint());
                    }

        }
    }
}
