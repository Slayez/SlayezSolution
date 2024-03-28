using GameEngine.Components.Main;

namespace GameEngine.Components
{
    public class Lifetime : Component
    {
        private float _lifetime;
        public float Get { get => _lifetime; }
        public float Set { set => _lifetime = value; }
        public void Add(float value)
        {
            _lifetime += value;
        }

        public Lifetime()
        {
            _lifetime = 1000;
        }

        public Lifetime(float lifetime)
        {
            _lifetime = lifetime;
        }
    }
}
