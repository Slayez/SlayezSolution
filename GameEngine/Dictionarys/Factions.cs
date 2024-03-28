using System;
using System.Collections.Generic;

namespace GameEngine.Dictionarys
{
    public static class Factions
    {
        public struct SFaction : IComparable
        {

            public string Name, DisplayName;

            public SFaction(string name, string displayName)
            {
                Name = name;
                DisplayName = displayName;
            }

            public int CompareTo(object obj)
            {
                throw new NotImplementedException();
            }

            public bool Equals(SFaction obj)
            {
                return (this == obj);
            }

            public static bool operator ==(SFaction left, SFaction right)
            {
                return (left.Name == right.Name);
            }
            public static bool operator !=(SFaction left, SFaction right)
            {
                return (left.Name != right.Name);
            }
        }

        public struct SRelationship : IComparable
        {
            public byte me, other;
            public ERelationship relationship;

            public SRelationship(byte me, byte other, ERelationship respect)
            {
                this.me = me;
                this.other = other;
                this.relationship = respect;
            }

            public int CompareTo(object obj)
            {
                throw new NotImplementedException();
            }

            public bool FindFromId(SRelationship obj)
            {
                return (obj.me == me & obj.other == other);
            }
        }

        public enum ERelationship { Enemy, Neutral, Ally }
        public static List<SFaction> GetFactions { get => _factions; }
        public static SFaction GetFaction(int id)
        {
            return _factions[id];
        }
        public static void AddFaction(SFaction newfaction)
        {
            if (!_factions.Contains(newfaction))
                _factions.Add(newfaction);
        }

        public static ERelationship GetRelationship(SFaction faction1, SFaction faction2)
        {
            return _relationships.Find(new SRelationship((byte)GetId(faction1), (byte)GetId(faction2), 0).FindFromId).relationship;
        }

        private static int GetId(SFaction faction)
        {
            return _factions.FindIndex(faction.Equals);
        }


        private static List<SFaction> _factions = new List<SFaction> { new SFaction("Player", "Игрок"), new SFaction("Enemy", "Враг") };
        private static List<SRelationship> _relationships = new List<SRelationship> { new SRelationship(0, 1, 0), new SRelationship(1, 0, 0) };
    }
}
