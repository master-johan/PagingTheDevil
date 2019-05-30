using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil.GameObject
{
    class Gateway : GameObject
    {
        public bool IsVisible { get; set; }
        
        float rotation;

        Texture2D openTex;
        
        public bool Open { get; set; }

        public Gateway(Texture2D tex, Vector2 pos, int gateId, Texture2D openTex) : base(tex, pos)
        {
            this.openTex = openTex;

            Open = false;

            if (gateId == 0)
            { rotation = 0f; }
            else if (gateId == 1)
            { rotation = MathHelper.ToRadians(180); }
            else if (gateId == 2)
            { rotation = MathHelper.ToRadians(270); }
            else if (gateId == 3)
            { rotation = MathHelper.ToRadians(90); }

            if (gateId == 2)
            {
                rect.Width = tex.Height - tex.Height/2;
                rect.Height = tex.Width + tex.Width/2;
                rect.X = (int)pos.X + 10;
                rect.Y = (int)pos.Y - 48;
            }
            else if (gateId == 3)
            {
                rect.Width = tex.Height - tex.Height / 2;
                rect.Height = tex.Width + tex.Width / 2;
                rect.X = (int)pos.X - 30;
                rect.Y = (int)pos.Y - 48;
            }
            else
            {
                rect.Width = tex.Width;
                rect.Height = tex.Height;
                rect.X = (int)pos.X - 48;
                rect.Y = (int)pos.Y - 20;
            }

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Open)
            {
                spriteBatch.Draw(openTex, pos, null, Color.White, rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0.3f);
            }
            else
            {
                spriteBatch.Draw(tex, pos, null, Color.White, rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0.3f);
            }
        }
    }
}
