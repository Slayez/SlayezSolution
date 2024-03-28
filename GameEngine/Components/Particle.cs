using GameEngine.Components.Main;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class Particle : Component
    {
        private Vector4 _velocity;

        public Vector4 Velocity { get => _velocity; set => _velocity = value; }

        public Particle(Vector4 velocity)
        {
            _velocity = velocity;
        }
        public Particle()
        {
            _velocity = new Vector4(0, 0, 0, 0);
        }
    }
}
