using GameEngine.Components.Main;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine.Systems.Main
{
    public static class GameObjectManager
    {
        private static Dictionary<string, GameObjectFactory> _factories = new Dictionary<string, GameObjectFactory>();
        public static GameObjectList gameObjects = new GameObjectList();

        #region Static Wrapper Methods
        public static GameObject Create(string name)
        {
            try
            {
                return MakeGameObject(name);
            }
            catch (Exception e)
            {
                IOCore.AddToLogError(e.Message);
                return null;
            }
        }

        public static void CreateType(string name, List<Component> components)
        {
            MakeType(name, components);
        }
        #endregion

        public static GameObject MakeGameObject(string name)
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

        private static void MakeType(string name, List<Component> components)
        {
            _factories[name.ToLower()] = new GameObjectFactory(name, components);
        }

        public static void Register(GameObject obj)
        {
            SystemManager.gameObjects.Add(obj);
        }

        public static void Deregister(GameObject obj)
        {
            SystemManager.gameObjects.RemoveAt(SystemManager.gameObjects.IndexOf(obj));
        }

        internal static void Remove(string name)
        {
            bool destr = false;
            if (SystemManager.gameObjects.Count > 0)
                foreach (GameObject obj in SystemManager.gameObjects)
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

        internal static bool InPoint(Vector3 pos, Component component)
        {
            foreach (GameObject obj in SystemManager.gameObjects)
                if (obj.HasComponentType(component.GetType()))
                    if (obj.Component<Transform>().Position == pos)
                        return true;
            return false;
        }
    }
}
