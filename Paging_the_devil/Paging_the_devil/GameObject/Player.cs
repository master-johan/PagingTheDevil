using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.GameObject;

namespace Paging_the_devil
{

    class Player : Character
    {

        Controller controller;
        Vector2 direction;
        int playerIndex;
        Rectangle spellRect;
        public bool shoot;
        bool slash;
        int timer;
        List<Ability> abilityList;
        GamePadState currentPadState;

      

       

        public Player(Texture2D tex, Vector2 pos, Rectangle rect, Rectangle spellRect,int playerIndex) 
            : base(tex, pos, rect)
        {
            this.playerIndex = playerIndex;
            this.spellRect = spellRect;
            //controller = new Controller();
            abilityList = new List<Ability>();
            slash = false;
            shoot = false;
            timer = 0;
        }

        public override void Update()
        {
            slash = false;
            pos.X += direction.X * 10.0f;
            pos.Y -= direction.Y * 10.0f;


            if (currentPadState.IsButtonDown(Buttons.X))
            {
                if (timer == 0)
                {
                    Ability ability = new Ability(TextureManager.mageSpellList[0], pos, spellRect, this);
                    abilityList.Add(ability);
                    shoot = true;
                    timer = 60;
                }
            }
            if (currentPadState.IsButtonDown(Buttons.B))
            {
                slash = true;
            }

            foreach (var A in abilityList)
            {
                A.Update();
            }

            if (timer > 0)
            {
                timer--;
            }



        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if(c1.IsConnected)
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, 60, 70), Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);
            if (slash)
            {
                spriteBatch.Draw(tex, pos, Color.Black);
            }
            foreach (var a in abilityList)
            {
                a.Draw(spriteBatch);
            }
        }

        public void InputDirection(Vector2 newDirection)
        {
            direction = newDirection;
        }

        public void InputPadState (GamePadState padState)
        {

            currentPadState = padState;

        }
    }
}
