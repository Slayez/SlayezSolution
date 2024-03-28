using GameEngine.Components.Main;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Components
{
    public class BasicAI : Component
    {
        private List<Vector2> _path;
        public List<Vector2> GetPath { get => _path; }
        private float _speed;
        public float Speed { get => _speed; set => _speed = value; }
        public bool HasTarget { get => _path.Count > 0; }
        public Vector2 TargetPoint()
        {
            if (_path.Count > 0)
                return _path.First();
            else
                return new Vector2(0, 0);
        }

        /// <summary>
        /// Текущая точка достигнута
        /// </summary>
        public void NextTarget()
        {
            if (_path.Count > 0)
                _path.RemoveAt(0);
        }

        /// <summary>
        /// Ставим путь для движения в задачу
        /// </summary>
        public void SetPath(List<Vector2> newPath)
        {

            this._path = new List<Vector2>(newPath);
        }
        public BasicAI(List<Vector2> path, float speed = 150)
        {
            _path = path;
            _speed = speed;
        }

        public BasicAI()
        {
            _path = new List<Vector2>();
            _speed = 0.25f;
        }
    }
}
