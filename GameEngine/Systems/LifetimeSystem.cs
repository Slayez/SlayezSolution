using GameEngine.Components;
using GameEngine.Systems.Main;

namespace GameEngine.Systems
{
    public class LifetimeSystem : AbstractSystem
    {
        public override void Update()
        {
            bool destroyed = false;
            for (int i = 0; i < SystemManager.gameObjects.Count; i++)
                if (SystemManager.gameObjects[i].HasComponentType(typeof(Lifetime)))
                {
                    SystemManager.gameObjects[i].Component<Lifetime>().Add(-SystemManager.deltaTime);

                    if (SystemManager.gameObjects[i].Component<Lifetime>().Get <= 0)
                    {
                        //Удаляем объект по истечению таймера
                        SystemManager.gameObjects.RemoveAt(i);

                        //SystemManager.DestroyObject(SystemManager.gameObjects[i]);
                        /*
                        SystemManager.gameObjects[i] = null;//.Destroy();
                        SystemManager.gameObjects.RemoveAt(i);*/
                        destroyed = true;
                        break;
                    }
                }
            if (destroyed)
                Update();
        }
    }
}
