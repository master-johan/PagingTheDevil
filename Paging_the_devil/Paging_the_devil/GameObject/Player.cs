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

        float movementSpeed;
        
        int playerIndex;
        int fireballTimer;
        int slashTimer;

        Rectangle spellRect;

        public bool shoot;
        public bool slash;

        public List<Ability> abilityList;

        GamePadState currentPadState;

        Vector2 spellDirection;
        Vector2 lastInputDirection;

        public Player(Texture2D tex, Vector2 pos, Rectangle spellRect, int playerIndex)
            : base(tex, pos)
        {
            this.playerIndex = playerIndex;
            this.spellRect = spellRect;
            rect = new Rectangle((int)pos.X, (int)pos.Y, 59, 61);
            //controller = new Controller();
            abilityList = new List<Ability>();
            slash = false;
            shoot = false;
            fireballTimer = 0;
            slashTimer = 0;

            movementSpeed = 2.0f;
        }

        public override void Update()
        {
            rect.X = (int)pos.X - 30;
            rect.Y = (int)pos.Y - 30;

            slash = false;
            pos.X += lastInputDirection.X * movementSpeed;
            pos.Y -= lastInputDirection.Y * movementSpeed;


            if (currentPadState.IsButtonDown(Buttons.X))
            {
                if (fireballTimer == 0)
                {
                    Shoot();
                }

            }
            if (currentPadState.IsButtonDown(Buttons.B))
            {
                if (slashTimer == 0)
                {
                    double slashDir = Math.Atan2(lastInputDirection.Y, lastInputDirection.X);

                    float slashAngle = (float)MathHelper.ToDegrees((float)slashDir);

                    if (slashAngle > 45 && slashAngle < 135) // up
                    {
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, new Vector2(0,-1), slashAngle);
                        abilityList.Add(slashObject);
                        slash = true;

                    }

                    else if (slashAngle > 135 || slashAngle <-135) // left
                    {
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, new Vector2(0, -1), slashAngle);
                        abilityList.Add(slashObject);
                        slash = true;

                    }

                    else if (slashAngle > -135 && slashAngle < -45) // down
                    {
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, new Vector2(0, -1),slashAngle);
                        abilityList.Add(slashObject);
                        slash = true;

                    }

                    else if (slashAngle >-45 && slashAngle < 45) // right
                    {
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, new Vector2(0, -1),slashAngle);
                        abilityList.Add(slashObject);
                        slash = true;

                    }

                    //float xValue = lastInputDirection.X;
                    //float yValue = lastInputDirection.Y;

                    //float degreesX = 
                    //float degreesY = 



                    //if (slashDirection == 0)
                    //{
                    //    
                    //}
                }
            }

            foreach (var A in abilityList)
            {
                A.Update();
            }

            if (fireballTimer > 0)
            {
                fireballTimer--;
            }
        }

        private void Shoot()
        {
            spellDirection = lastInputDirection;
            spellDirection.Normalize();
            spellDirection.Y = -spellDirection.Y;
            Ability ability = new Fireball(TextureManager.mageSpellList[0], pos, this, spellDirection);
            abilityList.Add(ability);
            shoot = true;
            fireballTimer = 60;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if(c1.IsConnected)
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, 60, 70), Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);
            
            foreach (var a in abilityList)
            {
                a.Draw(spriteBatch);
            }
        }

        public void InputDirection(Vector2 newDirection)
        {
            lastInputDirection = newDirection;
        }

        public void InputPadState(GamePadState padState)
        {
            currentPadState = padState;
        }
    }
}
