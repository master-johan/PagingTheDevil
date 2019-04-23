using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil.Manager
{
    static class ValueBank
    {
        public static float RangerHealth;
        public static float DruidHealth;
        public static float BarbarianHealth;
        public static float KnightHealth;
        public static float SmallDevilHealth;
        public static float SlimeHealth;

        public static int ArrowDmg;
        public static int TrapDmg;
        public static int HealHarmDmg;
        public static int SlashDmg;
        public static int FireballDmg;

        public static int HealHarmHeal;

        public static int ArrowSpeed;
        public static int HealHarmSpeed;
        public static int FireballSpeed;

        public static int HealHarmTimer;
        public static int TrapTimer;
        public static int SmallDevilShootTimer;

        public static int SmallDevilMoveSpeed;
        public static float SlimeSpeed;


        public static void Values()
        {
            //Character Health
            BarbarianHealth = 150f;
            KnightHealth = 200f;
            DruidHealth = 120f;
            RangerHealth = 100f;

            //Enemy Health
            SmallDevilHealth = 10;
            SlimeHealth = 20;

            //Ability Dmg
            ArrowDmg = 2;
            TrapDmg = 2;
            HealHarmDmg = 3;
            SlashDmg = 2;

            //Enemy Ability Dmg
            FireballDmg = 2;

            //Ability Heal
            HealHarmHeal = 4;

            //Ability Speed
            ArrowSpeed = 7;
            HealHarmSpeed = 7;
            FireballSpeed = 7;

            //Ability Timer
            HealHarmTimer = 4;
            SmallDevilShootTimer = 40;
            TrapTimer = 5;

            //Enemy Speed
            SmallDevilMoveSpeed = 4;
            SlimeSpeed = 1f;
        }


    }
}
