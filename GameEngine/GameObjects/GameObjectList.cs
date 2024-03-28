using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.GameObjects
{
    public class GameObjectList : IList<GameObject>
    {
        private List<GameObject> _gameObjects;
        public int Capacity { get => _gameObjects.Capacity; set => _gameObjects.Capacity = value; }

        /// <summary>
        /// Constructs a new GameObjectList, which only adds gameObjects with the set of required components
        /// </summary>
        public GameObjectList()
        {
            _gameObjects = new List<GameObject>();
        }


        public IEnumerator<GameObject> GetEnumerator()
        {
            return _gameObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(GameObject item)
        {
            _gameObjects.Add(item);
            _gameObjects.TrimExcess();
        }

        public void Clear()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i] = null;
            _gameObjects.Clear();
            _gameObjects.TrimExcess();
        }

        public bool Contains(GameObject item)
        {
            return _gameObjects.Contains(item);
        }

        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            _gameObjects.CopyTo(array, arrayIndex);
        }

        public bool Remove(GameObject item)
        {
            return _gameObjects.Remove(item);
        }

        /// <summary>
        /// Removes ALL gameObjects with the given name from the list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Remove(string name)
        {
            _gameObjects = (from entity in _gameObjects
                            where entity.Name != name
                            select entity).ToList();
            _gameObjects.TrimExcess();
        }

        public bool Contains(string part)
        {
            return
            (from entity in _gameObjects
             where entity.Name.Contains(part)
             select entity).ToList().Count > 0;
        }

        public int Count { get => _gameObjects.Count; }
        public bool IsReadOnly { get; private set; }

        public int IndexOf(GameObject item)
        {
            return _gameObjects.IndexOf(item);
        }

        public void Insert(int index, GameObject item)
        {

            _gameObjects.Insert(index, item);
            _gameObjects.TrimExcess();
        }

        public void RemoveAt(int index)
        {
            _gameObjects.RemoveAt(index);
            _gameObjects.TrimExcess();
        }

        public GameObject this[int index]
        {
            get { return _gameObjects[index]; }
            set { _gameObjects[index] = value; }
        }

        /// <summary>
        /// Returns first occurrence of the entity with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject this[string name]
        {
            get { return _gameObjects.FirstOrDefault(entity => entity.Name == name); }
        }
    }
}
