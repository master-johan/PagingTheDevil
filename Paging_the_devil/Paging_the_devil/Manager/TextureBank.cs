using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.Manager
{

    static class TextureBank
    {
        public static List<Texture2D> playerTextureList = new List<Texture2D>();
        public static List<Texture2D> mageSpellList = new List<Texture2D>();
        public static List<Texture2D> roomTextureList = new List<Texture2D>();
        public static List<Texture2D> enemyTextureList = new List<Texture2D>();
        public static List<Texture2D> menuTextureList = new List<Texture2D>();
        public static List<Texture2D> hudTextureList = new List<Texture2D>();
        public static List<Texture2D> playerSelectBackgroundList = new List<Texture2D>();
        public static List<Texture2D> buttonTextureList = new List<Texture2D>();
        public static List<Texture2D> abilityButtonList = new List<Texture2D>();
        public static SpriteFont spriteFont;

        public static void LoadTextures(ContentManager Content)
        {
            //Player
            playerTextureList.Add(Content.Load<Texture2D>(@"KnightSpriteSheet"));//0
            playerTextureList.Add(Content.Load<Texture2D>(@"BarbarianSpritesheet"));//1
            playerTextureList.Add(Content.Load<Texture2D>(@"DruidSpriteSheet"));//2
            playerTextureList.Add(Content.Load<Texture2D>(@"RangerSpriteSheet"));//3
            playerTextureList.Add(Content.Load<Texture2D>(@"PlayerRing"));//4

            //Spells
            mageSpellList.Add(Content.Load<Texture2D>(@"Fireball"));//0
            mageSpellList.Add(Content.Load<Texture2D>(@"slash"));//1
            mageSpellList.Add(Content.Load<Texture2D>(@"Trap"));//2
            mageSpellList.Add(Content.Load<Texture2D>(@"HealHarmTex"));//3
            mageSpellList.Add(Content.Load<Texture2D>(@"Arrow"));//4
            mageSpellList.Add(Content.Load<Texture2D>(@"AxeBarb"));//5
            mageSpellList.Add(Content.Load<Texture2D>(@"CleavePNG"));//6
            mageSpellList.Add(Content.Load<Texture2D>(@"Webball"));//7
            mageSpellList.Add(Content.Load<Texture2D>(@"DevilSword"));//8
            mageSpellList.Add(Content.Load<Texture2D>(@"Taunt1"));//9
            mageSpellList.Add(Content.Load<Texture2D>(@"Taunt2"));//10
            mageSpellList.Add(Content.Load<Texture2D>(@"CircleOfHeal"));//11
            mageSpellList.Add(Content.Load<Texture2D>(@"SpiderWeb"));//12
            mageSpellList.Add(Content.Load<Texture2D>(@"Root"));//13

            //Room
            roomTextureList.Add(Content.Load<Texture2D>(@"Portal"));//0
            roomTextureList.Add(Content.Load<Texture2D>(@"Horisontell Vägg"));//1
            roomTextureList.Add(Content.Load<Texture2D>(@"Vertikal Vägg"));//2
            roomTextureList.Add(Content.Load<Texture2D>(@"FloorPNG"));//3
            roomTextureList.Add(Content.Load<Texture2D>(@"Walltop"));//4
            roomTextureList.Add(Content.Load<Texture2D>(@"Walldown"));//5
            roomTextureList.Add(Content.Load<Texture2D>(@"Wallright"));//6
            roomTextureList.Add(Content.Load<Texture2D>(@"Wallleft"));//7

            //Enemy
            enemyTextureList.Add(Content.Load<Texture2D>(@"RedDevil"));//0
            enemyTextureList.Add(Content.Load<Texture2D>(@"Slime"));//1
            enemyTextureList.Add(Content.Load<Texture2D>(@"SpiderBoi"));//2
            enemyTextureList.Add(Content.Load<Texture2D>(@"DevilSprite"));//3
            enemyTextureList.Add(Content.Load<Texture2D>(@"TargetDummy"));//4

            //Menu
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayGameBTN"));//0
            menuTextureList.Add(Content.Load<Texture2D>(@"ControlsBTN"));//1
            menuTextureList.Add(Content.Load<Texture2D>(@"ExitGameBTN"));//2
            menuTextureList.Add(Content.Load<Texture2D>(@"SvartBak"));//3
            menuTextureList.Add(Content.Load<Texture2D>(@"PTDlogo"));//4
            menuTextureList.Add(Content.Load<Texture2D>(@"Pointer"));//5
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayerSelectBG"));//6
            menuTextureList.Add(Content.Load<Texture2D>(@"ConnectPlayer"));//7
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayerSelectLogo"));//8
            menuTextureList.Add(Content.Load<Texture2D>(@"SelectPlayer"));//9
            menuTextureList.Add(Content.Load<Texture2D>(@"Ready"));//10
            menuTextureList.Add(Content.Load<Texture2D>(@"startGameText"));//11
            menuTextureList.Add(Content.Load<Texture2D>(@"KnightInfo"));//12
            menuTextureList.Add(Content.Load<Texture2D>(@"RangerInfo"));//13
            menuTextureList.Add(Content.Load<Texture2D>(@"BarbInfo"));//14
            menuTextureList.Add(Content.Load<Texture2D>(@"DruidInfo"));//15
            menuTextureList.Add(Content.Load<Texture2D>(@"Controls"));//16
            menuTextureList.Add(Content.Load<Texture2D>(@"SkipText"));//17
            menuTextureList.Add(Content.Load<Texture2D>(@"PausTex"));//18
            menuTextureList.Add(Content.Load<Texture2D>(@"PauseResume"));//19
            menuTextureList.Add(Content.Load<Texture2D>(@"PauseCharInfo"));//20
            menuTextureList.Add(Content.Load<Texture2D>(@"GameOver"));//21
            menuTextureList.Add(Content.Load<Texture2D>(@"Winner"));//22
            menuTextureList.Add(Content.Load<Texture2D>(@"Restart"));//23

            //HUD
            hudTextureList.Add(Content.Load<Texture2D>(@"Abtn"));//0
            hudTextureList.Add(Content.Load<Texture2D>(@"Bbtn"));//1
            hudTextureList.Add(Content.Load<Texture2D>(@"Xbtn"));//2
            hudTextureList.Add(Content.Load<Texture2D>(@"Ybtn"));//3
            hudTextureList.Add(Content.Load<Texture2D>(@"HUDtest"));//4
            hudTextureList.Add(Content.Load<Texture2D>(@"slashBtn"));//5
            hudTextureList.Add(Content.Load<Texture2D>(@"CooldownTex"));//6
            hudTextureList.Add(Content.Load<Texture2D>(@"TauntBtn"));//7
            hudTextureList.Add(Content.Load<Texture2D>(@"FlowerPowerBtn"));//8
            hudTextureList.Add(Content.Load<Texture2D>(@"P1"));//9
            hudTextureList.Add(Content.Load<Texture2D>(@"P2"));//10
            hudTextureList.Add(Content.Load<Texture2D>(@"P3"));//11
            hudTextureList.Add(Content.Load<Texture2D>(@"P4"));//12
            hudTextureList.Add(Content.Load<Texture2D>(@"HudBarb"));//13
            hudTextureList.Add(Content.Load<Texture2D>(@"HudDruid"));//14
            hudTextureList.Add(Content.Load<Texture2D>(@"HudKnight"));//15
            hudTextureList.Add(Content.Load<Texture2D>(@"HudRanger"));//16
            hudTextureList.Add(Content.Load<Texture2D>(@"HudHealth"));//17
            hudTextureList.Add(Content.Load<Texture2D>(@"HP"));//18
            



            //AbilityButtons
            abilityButtonList.Add(Content.Load<Texture2D>(@"ArrowBtn"));//0
            abilityButtonList.Add(Content.Load<Texture2D>(@"SlashBtn"));//1
            abilityButtonList.Add(Content.Load<Texture2D>(@"TrapBtn"));//2
            abilityButtonList.Add(Content.Load<Texture2D>(@"HealHarmBtn"));//3
            abilityButtonList.Add(Content.Load<Texture2D>(@"CleaveBtn"));//4
            abilityButtonList.Add(Content.Load<Texture2D>(@"DashBtn"));//5
            abilityButtonList.Add(Content.Load<Texture2D>(@"RootBtn"));//6
            abilityButtonList.Add(Content.Load<Texture2D>(@"FlowerPowerBtn"));//7
            abilityButtonList.Add(Content.Load<Texture2D>(@"TauntBtn"));//8
            abilityButtonList.Add(Content.Load<Texture2D>(@"RootBtn"));//9
            abilityButtonList.Add(Content.Load<Texture2D>(@"BlockBtn"));//10

            //PlayerSelectBackground bilder
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Background"));//0
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Clouds"));//1
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Cloud2"));//2
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"MountRight"));//3
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"MountLeft"));//4
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"TwoMount"));//5
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Cloud3"));//6
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Bird"));//7
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Walls"));//8

            // ButtonTexture
            buttonTextureList.Add(Content.Load<Texture2D>(@"A"));//0
            buttonTextureList.Add(Content.Load<Texture2D>(@"B"));//1
            buttonTextureList.Add(Content.Load<Texture2D>(@"X"));//2
            buttonTextureList.Add(Content.Load<Texture2D>(@"Y"));//3

            // Fonts
            spriteFont = Content.Load<SpriteFont>(@"HudPlayerTxt");
        }
    }
}

