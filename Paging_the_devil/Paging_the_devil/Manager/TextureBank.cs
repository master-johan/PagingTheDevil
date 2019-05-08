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
            playerTextureList.Add(Content.Load<Texture2D>(@"KnightSpriteSheet"));
            playerTextureList.Add(Content.Load<Texture2D>(@"BarbarianSpritesheet"));
            playerTextureList.Add(Content.Load<Texture2D>(@"DruidSpriteSheet"));
            playerTextureList.Add(Content.Load<Texture2D>(@"RangerSpriteSheet"));

            //Spells
            mageSpellList.Add(Content.Load<Texture2D>(@"Fireball"));
            mageSpellList.Add(Content.Load<Texture2D>(@"slash"));
            mageSpellList.Add(Content.Load<Texture2D>(@"Trap"));
            mageSpellList.Add(Content.Load<Texture2D>(@"HealHarmTex"));
            mageSpellList.Add(Content.Load<Texture2D>(@"Arrow"));
            mageSpellList.Add(Content.Load<Texture2D>(@"AxeBarb"));
            mageSpellList.Add(Content.Load<Texture2D>(@"CleavePNG"));
            mageSpellList.Add(Content.Load<Texture2D>(@"Webball"));
            mageSpellList.Add(Content.Load<Texture2D>(@"DevilSword"));
            mageSpellList.Add(Content.Load<Texture2D>(@"Taunt1"));
            mageSpellList.Add(Content.Load<Texture2D>(@"Taunt2"));
            mageSpellList.Add(Content.Load<Texture2D>(@"CircleOfHeal"));
            mageSpellList.Add(Content.Load<Texture2D>(@"SpiderWeb"));

            //Room
            roomTextureList.Add(Content.Load<Texture2D>(@"Portal"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Horisontell Vägg"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Vertikal Vägg"));
            roomTextureList.Add(Content.Load<Texture2D>(@"FloorPNG"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Walltop"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Walldown"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Wallright"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Wallleft"));

            //Enemy
            enemyTextureList.Add(Content.Load<Texture2D>(@"RedDevil"));
            enemyTextureList.Add(Content.Load<Texture2D>(@"Slime"));
            enemyTextureList.Add(Content.Load<Texture2D>(@"SpiderBoi"));
            enemyTextureList.Add(Content.Load<Texture2D>(@"DevilSprite"));

            //Menu
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayGameBTN"));
            menuTextureList.Add(Content.Load<Texture2D>(@"ControlsBTN"));
            menuTextureList.Add(Content.Load<Texture2D>(@"ExitGameBTN"));
            menuTextureList.Add(Content.Load<Texture2D>(@"SvartBak"));
            menuTextureList.Add(Content.Load<Texture2D>(@"PTDlogo"));
            menuTextureList.Add(Content.Load<Texture2D>(@"Pointer"));
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayerSelectBG"));
            menuTextureList.Add(Content.Load<Texture2D>(@"ConnectPlayer"));
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayerSelectLogo"));
            menuTextureList.Add(Content.Load<Texture2D>(@"SelectPlayer"));
            menuTextureList.Add(Content.Load<Texture2D>(@"Ready"));
            menuTextureList.Add(Content.Load<Texture2D>(@"startGameText"));
            menuTextureList.Add(Content.Load<Texture2D>(@"KnightInfo"));//12
            menuTextureList.Add(Content.Load<Texture2D>(@"RangerInfo"));
            menuTextureList.Add(Content.Load<Texture2D>(@"BarbInfo"));
            menuTextureList.Add(Content.Load<Texture2D>(@"DruidInfo"));
            menuTextureList.Add(Content.Load<Texture2D>(@"Controls"));
            menuTextureList.Add(Content.Load<Texture2D>(@"SkipText"));

            //HUD
            hudTextureList.Add(Content.Load<Texture2D>(@"Abtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"Bbtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"Xbtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"Ybtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"HUDtest"));
            hudTextureList.Add(Content.Load<Texture2D>(@"slashBtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"CooldownTex"));
            hudTextureList.Add(Content.Load<Texture2D>(@"TauntBtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"FlowerPowerBtn"));


            //AbilityButtons
            abilityButtonList.Add(Content.Load<Texture2D>(@"ArrowBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"SlashBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"TrapBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"HealHarmBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"CleaveBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"DashBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"RootBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"FlowerPowerBtn"));
            abilityButtonList.Add(Content.Load<Texture2D>(@"TauntBtn"));

            //PlayerSelectBackground bilder
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Background"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Clouds"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Cloud2"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"MountRight"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"MountLeft"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"TwoMount"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Cloud3"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Bird"));
            playerSelectBackgroundList.Add(Content.Load<Texture2D>(@"Walls"));

            // ButtonTexture
            buttonTextureList.Add(Content.Load<Texture2D>(@"A"));
            buttonTextureList.Add(Content.Load<Texture2D>(@"B"));
            buttonTextureList.Add(Content.Load<Texture2D>(@"X"));
            buttonTextureList.Add(Content.Load<Texture2D>(@"Y"));

            spriteFont = Content.Load<SpriteFont>(@"HudPlayerTxt");
        }
    }
}

