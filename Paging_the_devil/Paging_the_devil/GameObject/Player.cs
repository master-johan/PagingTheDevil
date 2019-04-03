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
        Rectangle spellRect, hitboxLeft, hitboxRight, hitboxTop, hitboxBot;
        public bool shoot;
        bool slash;
        int timer;
        List<Ability> abilityList;
        GamePadState currentPadState;

        public Player(Texture2D tex, Vector2 pos, Rectangle spellRect, int playerIndex)
            : base(tex, pos)
        {
            this.playerIndex = playerIndex;
            this.spellRect = spellRect;
            rect = new Rectangle((int)pos.X, (int)pos.Y, 59, 61);
            hitboxLeft = new Rectangle((int)pos.X, (int)pos.Y, 10, 59);
            hitboxTop = new Rectangle((int)pos.X, (int)pos.Y + 5, 57, 10);
            hitboxBot = new Rectangle((int)pos.X, (int)pos.Y - 56, 57, 10);
            hitboxRight = new Rectangle((int)pos.X - 49, (int)pos.Y, 10, 59);
            //controller = new Controller();
            abilityList = new List<Ability>();
            slash = false;
            shoot = false;
            timer = 0;

        }

        public override void Update()
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            slash = false;
            pos.X += direction.X * 10.0f;
            pos.Y -= direction.Y * 10.0f;

            hitboxLeft.X = (int)pos.X - 30;
            hitboxLeft.Y = (int)pos.Y - 28;

            hitboxTop.X = (int)pos.X - 28;
            hitboxTop.Y = (int)pos.Y - 35;

            hitboxBot.X = (int)pos.X - 28;
            hitboxBot.Y = (int)pos.Y + 25;

            hitboxRight.X = (int)pos.X + 20 ;
            hitboxRight.Y = (int)pos.Y - 28;

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
            spriteBatch.Draw(tex, hitboxLeft, Color.Black);
            spriteBatch.Draw(tex, hitboxRight, Color.Red);
            spriteBatch.Draw(tex, hitboxTop, Color.Blue);
            spriteBatch.Draw(tex, hitboxBot, Color.Yellow);
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

        public void InputPadState(GamePadState padState)
        {
            currentPadState = padState;
        }

        public Rectangle GetTopHitbox
        {
            get { return hitboxTop; }
        }
        public Rectangle GetBotHitbox
        {
            get { return hitboxBot; }
        }
        public Rectangle GetLeftHitbox
        {
            get { return hitboxLeft; }
        }
        public Rectangle GetRightHitbox
        {
            get { return hitboxRight; }
        }
    }
}
