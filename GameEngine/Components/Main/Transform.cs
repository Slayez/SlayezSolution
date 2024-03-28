using Microsoft.Xna.Framework;

namespace GameEngine.Components.Main
{
    public class Transform : Component
    {
        /// <summary>
        /// Позиция в игре
        /// </summary>
        private Vector3 _position;
        /// <summary>
        /// Поворот
        /// </summary>
        private float _rotation;
        public float Rotation { get => _rotation; set => _rotation = value; }
        public Vector3 Position { get => _position; set => _position = value; }

        /// <summary>
        /// Относительно камеры
        /// </summary>
        private bool _overlay;
        public bool overlay { get => _overlay; set => _overlay = value; }


        public Transform(Vector3 position, float rotation = 0, bool overlay = false)
        {
            _position = position;
            _rotation = rotation;
            _overlay = overlay;
        }

        public Transform(float x, float y, float rotation = 0, bool overlay = false)
        {
            _position = new Vector3(x, y, 0);
            _rotation = rotation;
            _overlay = overlay;
        }

        public Transform()
        {
            _position = new Vector3(0, 0, 0);
            _rotation = 0;
            _overlay = false;
        }

        public Transform(bool overlay)
        {
            _position = new Vector3(0, 0, 0);
            _rotation = 0;
            _overlay = overlay;
        }

        public void Move(Vector3 pos)
        {
            this._position += pos;
        }

        public void Move(float x, float y, float z = 0)
        {
            _position += new Vector3(x, y, z);
        }
    }
}
