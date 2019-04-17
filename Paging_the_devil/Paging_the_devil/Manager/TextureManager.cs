using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.Manager
{

    static class TextureManager
    {
        public static List<Texture2D> playerTextureList = new List<Texture2D>();
        public static List<Texture2D> mageSpellList = new List<Texture2D>();
        public static List<Texture2D> roomTextureList = new List<Texture2D>();
        public static List<Texture2D> enemyTextureList = new List<Texture2D>();
        public static List<Texture2D> menuTextureList = new List<Texture2D>();
        public static List<Texture2D> hudTextureList = new List<Texture2D>();
        public static List<Texture2D> playerSelectBackgroundList = new List<Texture2D>();

        public static int WindowSizeY;
        public static int WindowSizeX;
        public static int GameWindowStartY;

        public static void LoadTextures(ContentManager Content)
        {
            //Player
            playerTextureList.Add(Content.Load<Texture2D>(@"KnightSprite"));
            playerTextureList.Add(Content.Load<Texture2D>(@"BarbarianSpritesheet"));
            playerTextureList.Add(Content.Load<Texture2D>(@"DruidSpriteSheet"));
            playerTextureList.Add(Content.Load<Texture2D>(@"RangerSpriteSheet"));

            //Spells
            mageSpellList.Add(Content.Load<Texture2D>(@"redSquare"));
            mageSpellList.Add(Content.Load<Texture2D>(@"slash"));

            //Room
            roomTextureList.Add(Content.Load<Texture2D>(@"Portal"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Horisontell Vägg"));
            roomTextureList.Add(Content.Load<Texture2D>(@"Vertikal Vägg"));
            roomTextureList.Add(Content.Load<Texture2D>(@"floorTexture"));

            //Enemey
            enemyTextureList.Add(Content.Load<Texture2D>(@"RedDevil"));

            //Menu
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayGameBTN"));
            menuTextureList.Add(Content.Load<Texture2D>(@"ControlsBTN"));
            menuTextureList.Add(Content.Load<Texture2D>(@"ExitGameBTN"));
            menuTextureList.Add(Content.Load<Texture2D>(@"SvartBak"));
            menuTextureList.Add(Content.Load<Texture2D>(@"PTDlogo"));
            menuTextureList.Add(Content.Load<Texture2D>(@"Pointer"));
            menuTextureList.Add(Content.Load<Texture2D>(@"PlayerSelectBG"));
            menuTextureList.Add(Content.Load<Texture2D>(@"ConnectPlayer"));
            //HUD
            hudTextureList.Add(Content.Load<Texture2D>(@"Abtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"Bbtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"Xbtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"Ybtn"));
            hudTextureList.Add(Content.Load<Texture2D>(@"HUDtest"));
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
        }
    }
}

