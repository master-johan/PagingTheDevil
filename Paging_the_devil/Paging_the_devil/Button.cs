using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil
{
    public class Button
    {
        Texture2D buttonTex;
        Vector2 buttonPos;
        Rectangle hitBox;

        Color color = new Color(255, 255, 255, 255);

        public Vector2 buttonSize;

        bool down;

        public bool isClicked;
        public bool activeButton;

        public Vector2 GetPos { get { return buttonPos; } }

        public Button(Texture2D newButtonTex, GraphicsDevice graphics, Vector2 buttonPos)
        {
            this.buttonPos = buttonPos;

            buttonTex = newButtonTex;
            buttonSize = new Vector2(newButtonTex.Width, newButtonTex.Height);
        }

        public void Update()
        {
            hitBox = new Rectangle((int)buttonPos.X, (int)buttonPos.Y, (int)buttonSize.X, (int)buttonSize.Y);

            if (activeButton)
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 3; else color.A -= 3;
            }

            else if (color.A < 255)
            {
                    color.A += 3;
                    isClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTex, hitBox, null, color, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public void setPosition(Vector2 newButtonPos)
        {
            buttonPos = newButtonPos;
        }
    }
}