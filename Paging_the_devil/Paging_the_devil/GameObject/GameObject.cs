using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil
{
    public abstract class GameObject
    {
        protected Texture2D tex;
        public Vector2 pos;
        protected Rectangle rect;
        

        public GameObject(Texture2D tex, Vector2 pos, Rectangle rect)
        {
            this.tex = tex;
            this.pos = pos;
            this.rect = rect;
        }

        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch);

        //public Vector2 Pos { get => pos; set => pos = value; }

        public Vector2 GetPos
        {

            get
            {
                return pos;
            }
        
        }
    }
}
