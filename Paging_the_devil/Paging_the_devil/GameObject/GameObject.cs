using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil
{
    public abstract class GameObject
    {
        protected Texture2D tex;
        protected Vector2 pos;
        protected Rectangle rect;

        

        public GameObject(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
        }

        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle GetRect { get { return rect; } }

        public Vector2 GetPos { get { return pos; } }

        public Vector2 SetPos { set => pos = value; }
    }
}
