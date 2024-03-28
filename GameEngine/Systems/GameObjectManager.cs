using GameEngine.Components;
using GameEngine.GameObjects;
using GameEngine.Systems.Main;
using SFML.System;
using System;
using System.Collections.Generic;

namespace GameEngine.Systems
{
    public class GameObjectManager : AbstractSystem
    {
        private Dictionary<string, GameObjectFactory> _factories;
        public GameObjectList gameObjects;

        public GameObjectManager()
        {
            _factories = new Dictionary<string, GameObjectFactory>();
            gameObjects = new GameObjectList();
        }

        #region Static Wrapper Methods
        public static GameObject Create(string name)
        {
            try
            {
                return Engine.Manager.System<GameObjectManager>().MakeGameObject(name);
            }
            catch (Exception e)
            {
                IOCore.AddToLogError(e.Message);
                return null;
            }
        }

        public static void CreateType(string name, List<Component> components)
        {

            Engine.Manager.System<GameObjectManager>().MakeType(name, components);
        }
        #endregion

        public GameObject MakeGameObject(string name)
        {
            try
            {
                return _factories[name.ToLower()].CreateGameObject();
            }
            catch (Exception e)
            {
                IOCore.AddToLogError(e.Message);
                return null;
            }
        }

        private void MakeType(string name, List<Component> components)
        {
            _factories[name.ToLower()] = new GameObjectFactory(name, components);
        }

        public static void Register(GameObject obj)
        {
            SystemManager.System<GameObjectManager>().gameObjects.Add(obj);
        }

        public static void Deregister(GameObject obj)
        {
            SystemManager.System<GameObjectManager>().gameObjects.RemoveAt(SystemManager.System<GameObjectManager>().gameObjects.IndexOf(obj));
        }

        internal static void Remove(string name)
        {
            bool destr = false;
            if (SystemManager.System<GameObjectManager>().gameObjects.Count > 0)
                foreach (GameObject obj in SystemManager.System<GameObjectManager>().gameObjects)
                {
                    if (obj.Name.Contains(name))
                    {
                        obj.Destroy();
                        destr = true;
                        break;
                    }
                }
            if (destr)
                Remove(name);
        }

        internal bool InPoint(Vector2f pos, Component component)
        {
            foreach (GameObject obj in SystemManager.System<GameObjectManager>().gameObjects)
                if (obj.HasComponentType(component))
                    if (obj.Component<Transform>().Position == pos)
                    return true;
            return false;
        }
    }
}
