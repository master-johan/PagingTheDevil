using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil
{
    public class Button
    {
        Texture2D buttonTex;
        Vector2 buttonPos, pos;
        Rectangle hitBox;

        Color color = new Color(255, 255, 255, 255);

        public Vector2 buttonSize;

        bool down;
        public bool isClicked;
        public bool activeButton;


        public Button(Texture2D newButtonTex, GraphicsDevice graphics, Vector2 buttonPos)
        {
            buttonTex = newButtonTex;
            buttonSize = new Vector2(newButtonTex.Width, newButtonTex.Height);
            this.buttonPos = buttonPos;
            activeButton = true; 

        }

        public void Update()
        {

            
            hitBox = new Rectangle((int)buttonPos.X, (int)buttonPos.Y, (int)buttonSize.X, (int)buttonSize.Y);

            if (activeButton)
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 3;
                else color.A -= 3;
                //if (Utility.mouseState.LeftButton == ButtonState.Pressed) isClicked = true;


                if (color.A < 255)
                {
                    color.A += 3;
                    isClicked = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTex, hitBox, color);
        }

        public void setPosition(Vector2 newButtonPos)
        {
            buttonPos = newButtonPos;
        }

        public Vector2 GetPos { get { return buttonPos; } }

        //public bool ActiveBtn()
        //{

        
        //    return true;
        //}

    }
}