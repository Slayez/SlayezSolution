using GameEngine.Components.Main;
using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.GameObjects
{
    public class GameObject
    {
        public string Name { get; set; }
        public bool Visible { get; set; }

        private List<Component> _components;

        public GameObject Copy()
        {
            List<Component> clonecomp = new List<Component>();
            foreach (Component comp in _components)
                clonecomp.Add(comp.Copy());
            return new GameObject(Name, clonecomp, true);
        }

        public GameObject(string name, List<Component> components = null, bool visible = true)
        {
            Name = name + Utilites.GenerateGUID();
            Visible = visible;
            if (components != null)
                _components = components.ToList();
            else
                _components = null;
        }

        /// <summary>
        /// Adds a component to this object. You can't have more than one of a kind.
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            if (HasComponentType(component.GetType()) == false)
            {
                _components.Add(component);
            }
        }

        /// <summary>
        /// Removes all components of the given type
        /// </summary>
        public void RemoveComponent<T>() where T : class, IComponent
        {
            var components = from component in _components
                             where !(component is T)
                             select component;

            _components = components.ToList();
        }

        public T Component<T>() where T : class, IComponent
        {
            //try
            //{
            var components = from component in _components
                             where component is T
                             select component;

            if (components.Any())
            {
                return components.First() as T;
            }
            return null;
            //}
            /*
            catch (Exception e)
            {
                IOCore.AddToLogError(e.Message);
                return null;
            }*/
        }

        /// <summary>
        /// Содержит ли object такой тип компонента
        /// </summary>
        /// <param name="component">Компонент</param>
        /// <returns></returns>
        public bool HasComponentType(Type type)
        {
            if (_components != null)
            {
                //var type = component.GetType();
                return _components.Any(comp => comp.GetType() == type);
            }
            else return false;
        }

        /// <summary>
        /// Содержит ли object конкретный компонент
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool HasSpecificComponent(Component component)
        {
            return _components.Contains(component);
        }

        /// <summary>
        /// Удаляет object из GOM(GameObjectManager)
        /// </summary>
        public void Destroy()
        {
            Deregister();

            if (_components != null)
            {
                for (int i = _components.Count - 1; i >= 0; i--)
                {
                    _components[i] = null;
                }
                _components.Clear();
                _components = null;
            }
        }
        /// <summary>
        /// Регистрирует object в GOM(GameObjectManager)
        /// </summary>
        public void CreateInWorld()
        {
            Register();
        }
        public void CreateInWorld(Vector3 pos)
        {
            Component<Transform>().Position = pos;
            Register();
        }
        private void Register()
        {
            GameObjectManager.Register(this);
        }

        private void Deregister()
        {
            GameObjectManager.Deregister(this);
        }
    }
}
