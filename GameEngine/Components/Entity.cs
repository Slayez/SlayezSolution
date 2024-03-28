using GameEngine.Components.Main;
using Microsoft.Xna.Framework;
using static GameEngine.Dictionarys.Factions;

namespace GameEngine.Components
{
    //Объект игрового мира кроме блоков
    public class Entity : Component
    {
        private float _size;
        public float Size { get => _size; set => _size = value; }

        private bool _ghost;
        public bool Ghost { get => _ghost; set => _ghost = value; }

        private SFaction _faction;
        public SFaction Faction { get => _faction; set => _faction = value; }

        public Vector2 GetBodySize { get => new Vector2(_size, _size); }

        public Entity(SFaction faction, bool ghost = false, float size = 16)
        {
            this._ghost = ghost;
            this._size = size;
            this._faction = faction;
        }

    }
}
