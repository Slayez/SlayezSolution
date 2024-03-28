using GameEngine.Components.Main;

namespace GameEngine.Components
{
    public class Spawner : Component
    {
        /// <summary>
        /// Осталось до спавна
        /// </summary>
        private float _delay;
        public float Delay { get => _delay; set => _delay = value; }
        /// <summary>
        /// Всего времени для спавна
        /// </summary>
        private float _timer;
        public float Timer { get => _timer; set => _timer = value; }
        /// <summary>
        /// Радиус вокруг для спавна
        /// </summary>
        private float _radius;
        public float Radius { get => _radius; set => _radius = value; }
        /// <summary>
        /// Имя факрикатора объектов
        /// </summary>
        private string _prefab_name;
        public string Prefab_name { get => _prefab_name; set => _prefab_name = value; }

        private int _spawned;

        public int Spawned { get => _spawned; }

        public bool CanSpawn { get => (Spawned < _max) | (_max == -1); }
        /// <summary>
        /// Макс. объектов спавнить
        /// </summary>
        private int _max;

        public void ResetDelay()
        {
            _delay = _timer;
            if (_max != -1)
                _spawned++;
        }
        public Spawner(string prefab_name, int max = -1, float timer = 5, float radius = 0)
        {
            _timer = timer;
            _radius = radius;
            _prefab_name = prefab_name;
            _max = max;
        }

        public Spawner()
        {
        }
    }
}
