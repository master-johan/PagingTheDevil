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
        Rectangle left, right, up, down;
        Rectangle drawRect;

        public bool shoot;

        public List<Ability> abilityList;

        GamePadState currentPadState;

        Vector2 spellDirection;
        Vector2 inputDirection;
        Vector2 lastInputDirection;

        public Player(Texture2D tex, Vector2 pos, Rectangle spellRect, int playerIndex)
            : base(tex, pos)
        {
            this.playerIndex = playerIndex;
            this.spellRect = spellRect;
            rect = new Rectangle((int)pos.X, (int)pos.Y, 59, 61);
            //controller = new Controller();
            abilityList = new List<Ability>();
            shoot = false;
            fireballTimer = 0;
            slashTimer = 0;

            movementSpeed = 2.0f;

            right = new Rectangle(0, 140, 60, 70);
            up = new Rectangle(0, 210, 60, 70);
            left = new Rectangle(0, 70, 60, 70);
            down = new Rectangle(0, 0, 60, 70);
            drawRect = down;
        }

        public override void Update()
        {
            rect.X = (int)pos.X - 30;
            rect.Y = (int)pos.Y - 30;

            pos.X += inputDirection.X * movementSpeed;
            pos.Y -= inputDirection.Y * movementSpeed;


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

                    float slashAngle = MathHelper.ToDegrees((float)slashDir);

                    Vector2 meleeDirection;

                    if (slashAngle > 45 && slashAngle < 135) // up
                    {
                        meleeDirection = new Vector2(0, -1);
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, meleeDirection);
                        abilityList.Add(slashObject);
                    }

                    else if (slashAngle > 135 || slashAngle < -135) // left
                    {
                        meleeDirection = new Vector2(-1, 0);
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, meleeDirection);
                        abilityList.Add(slashObject);

                    }

                    else if (slashAngle > -135 && slashAngle < -45) // down
                    {
                        meleeDirection = new Vector2(0, 1);
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, meleeDirection);
                        abilityList.Add(slashObject);

                    }

                    else if (slashAngle > -45 && slashAngle < 45) // right
                    {
                        meleeDirection = new Vector2(1, 0);
                        Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, this, meleeDirection);
                        abilityList.Add(slashObject);

                    }
                    slashTimer = 20;
                }
            }

            foreach (var A in abilityList)
            {
                A.Update();
                if (A == A as Slash)
                {
                    if (!(A as Slash).Active)
                    {
                        abilityList.Remove(A);
                        break;
                    }
                }
            }

            if (fireballTimer > 0)
            {
                fireballTimer--;
            }
            if (slashTimer > 0)
            {
                slashTimer--;
            }

            GetDirection();

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
            spriteBatch.Draw(tex, pos, drawRect, Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);

            foreach (var a in abilityList)
            {
                a.Draw(spriteBatch);
            }
        }

        public void InputDirection(Vector2 newDirection)
        {
            inputDirection = newDirection;
        }

        public void InputPadState(GamePadState padState)
        {
            currentPadState = padState;
        }

        public void LastInputDirection(Vector2 direction)
        {
            lastInputDirection = direction;
        }

        private void GetDirection()
        {
            if (inputDirection != Vector2.Zero)
            {
                if (Math.Abs(inputDirection.X) > Math.Abs(inputDirection.Y))
                {
                    if (inputDirection.X < 0)
                    {
                        drawRect = left;
                    }
                    else
                    {
                        drawRect = right;
                    }
                }
                else
                {
                    if (inputDirection.Y < 0)
                    {
                        drawRect = down;
                    }

                    else
                    {
                        drawRect = up;
                    }
                }
            }
        }
    }
}
