using GameEngine.Components;
using GameEngine.Components.Main;
using GameEngine.GameObjects;
using GameEngine.Systems.Main;

namespace GameEngine.Systems
{
    public class SpawnerSystem : AbstractSystem
    {
        //------------------!!
        public override void Update()
        {
            bool spawned = false;
            foreach (GameObject obj in SystemManager.gameObjects)
                if (obj.HasComponentType(typeof(Spawner)))
                {
                    obj.Component<Spawner>().Delay -= SystemManager.deltaTime;
                    if (obj.Component<Spawner>().Delay <= 0)
                    {
                        // Спавн
                        obj.Component<Spawner>().ResetDelay();
                        GameObject a = GameObjectManager.Create(obj.Component<Spawner>().Prefab_name);
                        if (obj.HasComponentType(typeof(Transform)))
                        {
                            if (a.HasComponentType(typeof(Transform)))
                            {
                                if (obj.Component<Spawner>().Radius > 0)
                                {
                                    //a.Component<Transform>().Position = obj.Component<Transform>().Position +
                                    //    new Vector2(Utilites.RandomFloat((int)-obj.Component<Spawner>().Radius, (int)obj.Component<Spawner>().Radius),
                                    //    Utilites.RandomFloat((int)-obj.Component<Spawner>().Radius, (int)obj.Component<Spawner>().Radius));
                                }
                                else
                                {
                                    a.Component<Transform>().Position = obj.Component<Transform>().Position;
                                }
                            }
                        }
                        a.CreateInWorld();
                        spawned = true;
                        break;
                    }
                }
            if (spawned)
                Update();
        }
    }
}
