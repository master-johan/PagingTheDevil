using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil.GameObject
{

    class Player : Character
    {
        float movementSpeed;
        
        int fireballTimer;
        int slashTimer;
        int playerIndex;

        Rectangle left, right, up, down;
        Rectangle spellRect, hitboxLeft, hitboxRight, hitboxTop, hitboxBot;
        Rectangle drawRect;

        public List<Ability> abilityList;

        Vector2 spellDirection;
        Vector2 inputDirection;
        Vector2 lastInputDirection;

        Controller controller;

        Ability ability;

        public Player(Texture2D tex, Vector2 pos, Rectangle spellRect, int playerIndex, Controller controller)
            : base(tex, pos)
        {
            this.playerIndex = playerIndex;
            this.spellRect = spellRect;
            this.controller = controller;
            GenerateRectangles(pos);
            DecidingValues();
            DecidingSourceRect();
            drawRect = down;
        }
        /// <summary>
        /// Den här metoden bestämmer sourcerektanglar.
        /// </summary>
        private void DecidingSourceRect()
        {
            right = new Rectangle(0, 140, 60, 70);
            up = new Rectangle(0, 210, 60, 70);
            left = new Rectangle(0, 70, 60, 70);
            down = new Rectangle(0, 0, 60, 70);
        }
        /// <summary>
        /// Den här metoden bestämmer värde.
        /// </summary>
        private void DecidingValues()
        {
            abilityList = new List<Ability>();
            fireballTimer = 0;
            slashTimer = 0;

            HealthPoints = 10;

            movementSpeed = 2.0f;
        }
        /// <summary>
        /// Den här metoden generar hitboxes.
        /// </summary>
        /// <param name="pos"></param>
        private void GenerateRectangles(Vector2 pos)
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, 59, 61);
            hitboxLeft = new Rectangle((int)pos.X, (int)pos.Y, 10, 59);
            hitboxTop = new Rectangle((int)pos.X, (int)pos.Y + 5, 57, 10);
            hitboxBot = new Rectangle((int)pos.X, (int)pos.Y - 56, 57, 10);
            hitboxRight = new Rectangle((int)pos.X - 49, (int)pos.Y, 10, 59);
        }

        public override void Update()
        {
            pos.X += inputDirection.X * movementSpeed;
            pos.Y -= inputDirection.Y * movementSpeed;

            Hitboxes();

            if (controller.ButtonPressed(Buttons.X))
            {
                if (fireballTimer == 0)
                {
                    ShootFireball();
                }
            }

            if (controller.ButtonPressed(Buttons.B))
            {
                if (slashTimer == 0)
                {
                    Slashes();
                }
            }

            UpdateAbility();
            ResetTimers();
            DrawDifferentRects();
        }
        /// <summary>
        /// Den här metoden återställer timers.
        /// </summary>
        private void ResetTimers()
        {
            if (fireballTimer > 0)
            {
                fireballTimer--;
            }
            if (slashTimer > 0)
            {
                slashTimer--;
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar abilities samt tar bort abilities.
        /// </summary>
        private void UpdateAbility()
        {
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
        }
        /// <summary>
        /// Den här metoden skapar slashes beroende på vinklar.
        /// </summary>
        private void Slashes()
        {
            double slashDir = Math.Atan2(lastInputDirection.Y, lastInputDirection.X);

            float slashAngle = MathHelper.ToDegrees((float)slashDir);

            Vector2 meleeDirection;

            if (slashAngle > 45 && slashAngle < 135) // up
            {
                meleeDirection = new Vector2(0, -1);
                CreateSlash(meleeDirection);
            }

            else if (slashAngle > 135 || slashAngle < -135) // left
            {
                meleeDirection = new Vector2(-1, 0);
                CreateSlash(meleeDirection);
            }

            else if (slashAngle > -135 && slashAngle < -45) // down
            {
                meleeDirection = new Vector2(0, 1);
                CreateSlash(meleeDirection);
            }

            else if (slashAngle > -45 && slashAngle < 45) // right
            {
                meleeDirection = new Vector2(1, 0);
                CreateSlash(meleeDirection);
            }

            slashTimer = 20;
        }
        /// <summary>
        /// Den här metoden skapar slashes.
        /// </summary>
        /// <param name="meleeDirection"></param>
        private void CreateSlash(Vector2 meleeDirection)
        {
            Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, meleeDirection);
            abilityList.Add(slashObject);
        }
        /// <summary>
        /// Den här metoden uppdaterar hitboxes.
        /// </summary>
        private void Hitboxes()
        {
            rect.X = (int)pos.X - 30;
            rect.Y = (int)pos.Y - 30;

            hitboxLeft.X = (int)pos.X - 30;
            hitboxLeft.Y = (int)pos.Y - 28;

            hitboxTop.X = (int)pos.X - 28;
            hitboxTop.Y = (int)pos.Y - 35;

            hitboxBot.X = (int)pos.X - 28;
            hitboxBot.Y = (int)pos.Y + 25;

            hitboxRight.X = (int)pos.X + 20;
            hitboxRight.Y = (int)pos.Y - 28;
        }
        /// <summary>
        /// Den här metoden skapar fireballs.
        /// </summary>
        private void ShootFireball()
        {
            spellDirection = lastInputDirection;
            spellDirection.Normalize();
            spellDirection.Y = -spellDirection.Y;

            ability = new Fireball(TextureManager.mageSpellList[0], pos, spellDirection);
            abilityList.Add(ability);

            fireballTimer = 60;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if(c1.IsConnected)

            spriteBatch.Draw(tex, pos, drawRect, Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);

            foreach (var A in abilityList)
            {
                A.Draw(spriteBatch);
            }

            spriteBatch.Draw(tex, hitboxLeft, Color.Black);
            spriteBatch.Draw(tex, hitboxRight, Color.Red);
            spriteBatch.Draw(tex, hitboxTop, Color.Blue);
            spriteBatch.Draw(tex, hitboxBot, Color.Yellow);
        }
        /// <summary>
        /// Den här metoden uppdaterar riktnngen.
        /// </summary>
        /// <param name="newDirection"></param>
        public void InputDirection(Vector2 newDirection)
        {
            inputDirection = newDirection;
        }
        /// <summary>
        /// Den här metoden uppdaterar senaste riktningen.
        /// </summary>
        /// <param name="direction"></param>
        public void LastInputDirection(Vector2 direction)
        {
            lastInputDirection = direction;
        }
        /// <summary>
        /// Den här metoden uppdaterar vilken bild som ska ritas ut beroende på hur man styr sin gubbe. 
        /// </summary>
        private void DrawDifferentRects()
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
        /// <summary>
        /// Den här metoden returnerar hitboxen i norr.
        /// </summary>
        public Rectangle GetTopHitbox
        {
            get { return hitboxTop; }
        }
        /// <summary>
        /// Den här metoden returnerar hitboxen i söder.
        /// </summary>
        public Rectangle GetBotHitbox
        {
            get { return hitboxBot; }
        }
        /// <summary>
        /// Den här metoden returnerar hitboxen i väst.
        /// </summary>
        public Rectangle GetLeftHitbox
        {
            get { return hitboxLeft; }
        }
        /// <summary>
        /// Den här metoden returnerar hitboxen i öst.
        /// </summary>
        public Rectangle GetRightHitbox
        {
            get { return hitboxRight; }

        }
    }
}
