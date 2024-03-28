using GameEngine.Components.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.GameObjects
{
    class GameObjectFactory
    {
        private string _name;
        private List<Component> _components;

        public GameObjectFactory(string name, List<Component> components)
        {
            _name = name;
            _components = components;
        }

        public GameObject CreateGameObject()
        {
            try
            {
                var components = (from component in _components
                                  select component.Copy()).ToList();
                return new GameObject(_name, components);
            }
            catch (Exception e)
            {
                Systems.Main.IOCore.AddToLogError(e.Message);
                return null;
            }
        }
    }
}
