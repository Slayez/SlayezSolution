using GameEngine.Components.Main;
using GameEngine.Systems;

namespace GameEngine.Components
{
    public class Stats : Component
    {

        public float hp { get; set; }

        public float MaxHp { get; set; }
        public float HpRegen { get; set; }

        public float xp { get; set; }
        public int lvl { get; set; }

        public float mana { get; set; }
        public float MaxMana { get; set; }
        public float ManaRegen { get; set; }

        public float stamina { get; set; }
        public float MaxStamina { get; set; }
        public float StaminaRegen { get; set; }

        public float Speed { get; set; }
        public float Evasion { get; set; }

        public float MeleeAttack { get; set; }
        public float RangeAttack { get; set; }
        public float MagicAttack { get; set; }

        public int endurance { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int intelligence { get; set; }

        public float MaxWeight { get; set; }

        public Stats(int lvl)
        {
            this.lvl = lvl;
            StatsSystem.ResetStatsFromLvl(this);
            StatsSystem.UpdateStats(this);
            //StatsSystem.ResetStats(ref stats);
        }
        public Stats()
        {
            this.lvl = 1;
            StatsSystem.ResetStatsFromLvl(this);
            StatsSystem.UpdateStats(this);
            //StatsSystem.ResetStats(ref stats);
        }

        public void TakeDamage(float value)
        {
            this.hp -= value;
        }
    }
}
