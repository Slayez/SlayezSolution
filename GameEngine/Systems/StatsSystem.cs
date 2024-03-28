using GameEngine.Components;
using GameEngine.GameObjects;
using GameEngine.Systems.Main;
using System;

namespace GameEngine.Systems
{
    public class StatsSystem : AbstractSystem
    {
        public static float NeedXpToLvl(int lvl)
        {
            return (float)Math.Pow(lvl, 3) * 5;
        }

        public static float DropXp(int lvl)
        {
            return (float)Math.Pow(lvl, 2);
        }

        /// <summary>
        /// Повышение уровня
        /// </summary>
        public static void LvlUp(Stats stat)
        {
            stat.xp = 0;
            stat.lvl++;

            stat.intelligence++;
            stat.strength++;
            stat.endurance++;
            stat.dexterity++;
        }

        public static void ResetStatsFromLvl(Stats stat)
        {
            stat.intelligence = 5 + stat.lvl - 1;
            stat.strength = 5 + stat.lvl - 1;
            stat.endurance = 5 + stat.lvl - 1;
            stat.dexterity = 5 + stat.lvl - 1;
        }

        public static void CheckStats(Stats stat)
        {
            if (stat.hp < 0)
                stat.hp = 0;
            if (stat.mana < 0)
                stat.mana = 0;
            if (stat.stamina < 0)
                stat.stamina = 0;

            if (stat.hp > stat.MaxHp)
                stat.hp = stat.MaxHp;
            if (stat.mana > stat.MaxMana)
                stat.mana = stat.MaxMana;
            if (stat.stamina > stat.MaxStamina)
                stat.stamina = stat.MaxStamina;

            if (stat.xp >= NeedXpToLvl(stat.lvl))
            {
                //Level UP
                LvlUp(stat);
                //Пополняем хп и ману
                ResetStats(stat);
            }
        }

        public static void UpdateStats(Stats stat)
        {
            stat.MaxHp = stat.endurance * 12 + 100;
            stat.HpRegen = ((float)stat.endurance / 10);
            stat.MaxMana = stat.intelligence * 12 + 100;
            stat.ManaRegen = (float)stat.intelligence / 10;
            stat.MaxStamina = stat.dexterity * 12 + 100;
            stat.StaminaRegen = (float)stat.dexterity / 10;
            stat.Speed = 1 + (float)stat.dexterity / 100;
            stat.Evasion = stat.dexterity;
            stat.MeleeAttack = stat.strength * 12;
            stat.RangeAttack = stat.dexterity * 12;
            stat.MagicAttack = stat.intelligence * 12;
            stat.MaxWeight = stat.strength * 2;
        }

        public override void Update()
        {
            foreach (GameObject obj in SystemManager.gameObjects)
            {
                if (obj.HasComponentType(typeof(Stats)))
                {
                    Regen(obj.Component<Stats>());
                }
            }
        }

        //Действия по завершению хода
        //public override void Turn()
        //{
        //    foreach (GameObject obj in SystemManager<GameObjectManager>().gameObjects)
        //        if (obj.HasComponentType(new Stats()))
        //        {
        //            UpdateStats(obj.Component<Stats>());
        //            //Применение регена
        //            Regen(obj.Component<Stats>());
        //            // Проверка на отрицательные значения
        //            CheckStats(obj.Component<Stats>());
        //        }
        //}

        public static void ResetStats(Stats stat)
        {
            UpdateStats(stat);
            stat.hp = stat.MaxHp;
            stat.mana = stat.MaxMana;
            stat.stamina = stat.MaxStamina;
        }

        public static void Regen(Stats stat)
        {
            if (stat.hp < stat.MaxHp & stat.hp > 0)
                stat.hp += stat.HpRegen * SystemManager.deltaTime;
            if (stat.hp > 0)
            {
                if (stat.mana < stat.MaxMana)
                    stat.mana += stat.ManaRegen * SystemManager.deltaTime;
                if (stat.stamina < stat.MaxStamina)
                    stat.stamina += stat.StaminaRegen * SystemManager.deltaTime;
            }
        }
    }
}
