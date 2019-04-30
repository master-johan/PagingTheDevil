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
        public static float SmallDevilIdleMoveSpeed;
        public static float SlimeSpeed;
        public static float PlayerSpeed;
        public static float DashTimer;
        public static float ChargeTimer;
        public static float DashSpeed;
        public static float ChargeSpeed;

        public static int DashCooldown;
        public static int HealHarmCooldown;
        public static int CleaveCooldown;
        public static int SlashCooldown;
        public static int ArrowCooldown;
        public static int TrapCooldown;
        public static int ChargeCooldown;
        public static int ArrowDmg;
        public static int TrapDmg;
        public static int HealHarmDmg;
        public static int SlashDmg;
        public static int FireballDmg;
        public static int CleaveDmg;
        public static int ChargeDmg;
        public static int HealHarmHeal;
        public static int ArrowSpeed;
        public static int HealHarmSpeed;
        public static int FireballSpeed;
        public static int HealHarmTimer;
        public static int TrapTimer;
        public static int SmallDevilShootTimer;
        public static int WindowSizeY;
        public static int WindowSizeX;
        public static int GameWindowStartY;
        public static int SmallDevilMoveSpeed;

        public static Random rand;

        public static void Values()
        {

            //Random
            rand = new Random();
            //Character Health
            BarbarianHealth = 150f;
            KnightHealth = 200f;
            DruidHealth = 120f;
            RangerHealth = 100f;

            //Enemy Health
            SmallDevilHealth = 10;
            SlimeHealth = 20;

            //Ability Cooldown
            DashCooldown = 40;
            CleaveCooldown = 40;
            HealHarmCooldown = 30;
            ArrowCooldown = 30;
            SlashCooldown = 30;
            TrapCooldown = 30;
            ChargeCooldown = 40;


            //Ability Dmg
            ArrowDmg = 2;
            TrapDmg = 2;
            HealHarmDmg = 3;
            SlashDmg = 2;
            CleaveDmg = 4;
            ChargeDmg = 4;

            //Enemy Ability Dmg
            FireballDmg = 2;

            //Ability Heal
            HealHarmHeal = 4;

            //Ability Speed
            ArrowSpeed = 7;
            HealHarmSpeed = 7;
            FireballSpeed = 7;
            DashSpeed = 15f;
            ChargeSpeed = 15f;

            //Ability Timer
            HealHarmTimer = 4000;
            SmallDevilShootTimer = 40;
            TrapTimer = 2000;
            DashTimer = 200;
            ChargeTimer = 200;

            //Enemy Speed
            SmallDevilMoveSpeed = 4;
            SmallDevilIdleMoveSpeed = 1f;
            SlimeSpeed = 1f;

            //Player Speed

            PlayerSpeed = 4;

        }


    }
}
