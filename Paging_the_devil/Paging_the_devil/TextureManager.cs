using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil
{

    static class TextureManager
    {
        public static List<Texture2D> playerTextureList = new List<Texture2D>();
        public static List<Texture2D> mageSpellList = new List<Texture2D>();
        public static List<Texture2D> rommTextureList = new List<Texture2D>();
        public static List<Texture2D> enemyTextureList = new List<Texture2D>();

        public static void LoadTextures(ContentManager Content)
        {
            //Player
            playerTextureList.Add(Content.Load<Texture2D>(@"KnightSprite"));

            //Spells
            mageSpellList.Add(Content.Load<Texture2D>(@"redSquare"));
            mageSpellList.Add(Content.Load<Texture2D>(@"slash"));

            //Room
            rommTextureList.Add(Content.Load<Texture2D>(@"Portal"));
            rommTextureList.Add(Content.Load<Texture2D>(@"Horisontell Vägg"));
            rommTextureList.Add(Content.Load<Texture2D>(@"Vertikal Vägg"));

            //Enemey
            enemyTextureList.Add(Content.Load<Texture2D>(@"Ghost"));
        }
    }
}

