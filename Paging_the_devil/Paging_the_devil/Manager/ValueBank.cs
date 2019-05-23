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
        public static float DevilHealth;
        public static float SlimeHealth;
        public static float SmallDevilIdleMoveSpeed;
        public static float SlimeSpeed;
        public static float SmallDevilMoveSpeed;
        public static float SpiderMoveSpeed;
        public static float PlayerSpeed;
        public static float DashTimer;
        public static float ChargeTimer;
        public static float DashSpeed;
        public static float ChargeSpeed;
        public static float WebbballTimer;
        public static float DevilSpeed;
        public static float DevilCleaveTimer;
        public static float TauntTimer;
        public static float WebRootTimer;
        public static float RootTimer;
        public static float HitTimerMax;
        public static float TrapNoHitTimer;

        public static int BlockTimer;
        public static int DashCooldown;
        public static int HealHarmCooldown;
        public static int CleaveCooldown;
        public static int SlashCooldown;
        public static int ArrowCooldown;
        public static int TrapCooldown;
        public static int ChargeCooldown;
        public static int BlockCooldown;
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
        public static int WallSpiderHealth;
        public static int WebballSpeed;
        public static int WebballDmg;
        public static int DevilCleaveCooldown;
        public static int FlowerPowerCooldown;

        public static Random rand;

        public static void Values()
        {

            //Random
            rand = new Random();
            //Character Health
            BarbarianHealth = 200f;
            KnightHealth = 250f;
            DruidHealth = 120f;
            RangerHealth = 100f;

            //Enemy Health
            SmallDevilHealth = 10;
            SlimeHealth = 15;
            WallSpiderHealth = 10;
            DevilHealth = 150;

            //Ability Cooldown
            DashCooldown = 60;
            CleaveCooldown = 100;
            HealHarmCooldown = 50;
            ArrowCooldown = 60;
            SlashCooldown = 30;
            TrapCooldown = 80;
            ChargeCooldown = 100;
            DevilCleaveCooldown = 360;
            BlockCooldown = 1000;
            FlowerPowerCooldown = 800;
          
            //Ability Dmg
            ArrowDmg = 3;
            TrapDmg = 1;
            HealHarmDmg = 1;
            SlashDmg = 2;
            CleaveDmg = 6;
            ChargeDmg = 3;

            //Enemy Ability Dmg
            FireballDmg = 4;
            WebballDmg = 2;

            //Ability Heal
            HealHarmHeal = 6;

            //Ability Speed
            ArrowSpeed = 10;
            HealHarmSpeed = 10;
            FireballSpeed = 10;
            WebballSpeed = 14;
            DashSpeed = 15f;
            ChargeSpeed = 15f;

            //Ability Timer
            HealHarmTimer = 4000;
            SmallDevilShootTimer = 40;
            TrapTimer = 2000;
            DashTimer = 200;
            ChargeTimer = 400;
            WebbballTimer = 160f;
            DevilCleaveTimer = 2000;
            TauntTimer = 4000;
            WebRootTimer = 3000;
            BlockTimer = 4;
            RootTimer = 2000;
            HitTimerMax = 350;
            TrapNoHitTimer = 4000;

            //Enemy Speed
            SmallDevilMoveSpeed = 4f;
            SmallDevilIdleMoveSpeed = 1f;
            SlimeSpeed = 1f;
            SpiderMoveSpeed = 2f;
            DevilSpeed = 3f;

            //Player Speed

            PlayerSpeed = 4;
        }
    }
}
