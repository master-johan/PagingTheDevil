﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject
{

    class Player : Character
    {
        float movementSpeed;
        int slashTimer;
        int playerIndex;
        int frame;

        double timer;
        double interval;

        Rectangle left, right, up, down;
        Rectangle hitboxLeft, hitboxRight, hitboxTop, hitboxBot;
        protected Rectangle drawRect;

        public List<Ability> abilityList;

        protected Vector2 spellDirection;
        protected Vector2 inputDirection;
        protected Vector2 lastInputDirection;
        
        protected Ability ability1;
        protected Ability ability2;
        protected Ability ability3;

        bool angleRight;
        bool angleLeft;
        bool angleUp;
        bool angleDown;


        public Player(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller)
            : base(tex, pos)
        {
            this.playerIndex = playerIndex;
            this.Controller = Controller;
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
            right = new Rectangle(0, 390, 50, 60);
            up = new Rectangle(0, 195, 50, 60);
            left = new Rectangle(0, 650, 50, 60);
            down = new Rectangle(0, 0, 50, 60);
        }
        /// <summary>
        /// Den här metoden bestämmer värde.
        /// </summary>
        private void DecidingValues()
        {
            abilityList = new List<Ability>();
            slashTimer = 0;

            HealthPoints = 100f;

            movementSpeed = 3.0f;
            interval = 200;
            angleUp = true;
            angleDown = true;
            angleRight = true;
            angleLeft = true;
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

        public override void Update(GameTime gameTime)
        {
            Movment();

            Hitboxes();

            if (Controller.ButtonPressed(Buttons.B))
            {
                if (slashTimer == 0)
                {

                    abilityList.Add(ability1);
                    slashTimer = 20;
                }
            }

            UpdateAbility();
            ResetTimers();
            DrawDifferentRects(gameTime);
        }
        /// <summary>
        /// Den här metoden sköter spelarens rörelse.
        /// </summary>
        private void Movment()
        {
            if (UpMovementBlocked && inputDirection.Y >0)
            {
                inputDirection.Y = 0;
               
            }

            if (DownMovementBlocked && inputDirection.Y<0)
            {
                inputDirection.Y = 0;
            }

            if (RightMovementBlocked && inputDirection.X >0)
            {
                inputDirection.X = 0;
            }
            if (LeftMovementBlocked && inputDirection.X <0)
            {
                inputDirection.X = 0;
            }
            pos.X += inputDirection.X * movementSpeed;
            pos.Y -= inputDirection.Y * movementSpeed;
        }
        /// <summary>
        /// Den här metoden återställer timers.
        /// </summary>
        private void ResetTimers()
        {
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
            Ability toRemove = null;
            foreach (var A in abilityList)
            {
                A.Update();
                if (A == A as Slash)
                {
                    if (!(A as Slash).Active)
                    {
                        toRemove = A;
                    }
                }
                if (A is Trap)
                {
                    if ((A as Trap).timePassed.TotalSeconds > 5)
                    {
                        toRemove = A;
                    }
                }
            }
            if (toRemove != null)
            {
                abilityList.Remove(toRemove);
            }
        }
        /// <summary>
        /// Den här metoden skapar slashes beroende på vinklar.
        /// </summary>
        private void Slashes()
        {
            double slashDir = Math.Atan2(lastInputDirection.Y, lastInputDirection.X);

            float slashAngle = MathHelper.ToDegrees((float)slashDir);

            Vector2 meleeDirection = Vector2.Zero;

            if (slashAngle > 45 && slashAngle < 135) // up
            {
                meleeDirection = new Vector2(0, -1);
            }

            else if (slashAngle > 135 || slashAngle < -135) // left
            {
                meleeDirection = new Vector2(-1, 0);
            }

            else if (slashAngle > -135 && slashAngle < -45) // down
            {
                meleeDirection = new Vector2(0, 1);
            }

            else if (slashAngle > -45 && slashAngle < 45) // right
            {
                meleeDirection = new Vector2(1, 0);
            }
            CreateSlash(meleeDirection);

            slashTimer = 20;
        }
        /// <summary>
        /// Den här metoden skapar slashes.
        /// </summary>
        /// <param name="meleeDirection"></param>
        private void CreateSlash(Vector2 meleeDirection)
        {
            Ability slashObject = new Slash(TextureManager.mageSpellList[1], pos, lastInputDirection);
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, drawRect, Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);

            foreach (var A in abilityList)
            {
                A.Draw(spriteBatch);
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar vilken bild som ska ritas ut beroende på hur man styr sin gubbe. 
        /// </summary>
        private void DrawDifferentRects(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (inputDirection != Vector2.Zero)
            {
                if (Math.Abs(inputDirection.X) > Math.Abs(inputDirection.Y))
                {
                    if (inputDirection.X < 0)
                    {
                        if (angleLeft)
                        {
                            drawRect = left;
                            angleLeft = false;
                            angleDown = true;
                            angleRight = true;
                            angleUp = true;
                        }
                        Animation(left);
                    }
                    else
                    {
                        if (angleRight)
                        {
                            drawRect = right;
                            angleLeft = true;
                            angleDown = true;
                            angleRight = false;
                            angleUp = true;
                        }
                        Animation(right);
                    }
                }
                else
                {
                    if (inputDirection.Y < 0)
                    {
                        if (angleDown)
                        {
                            drawRect = down;
                            angleLeft = true;
                            angleDown = false;
                            angleRight = true;
                            angleUp = true;
                        }
                        Animation(down);
                    }

                    else
                    {
                        if (angleUp)
                        {
                            drawRect = up;
                            angleLeft = true;
                            angleDown = true;
                            angleRight = true;
                            angleUp = false;
                        }
                        Animation(up);
                    }
                }
            }
        }
        /// <summary>
        /// Den här metoden animerar karaktärerna
        /// </summary>
        /// <param name="rect"></param>
        private void Animation(Rectangle rect)
        {
            if (timer <= 0)
            {
                timer = interval; frame++;
                drawRect.Y = rect.Y + (frame % 3) * 60 + (5 * (frame % 3));
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

        public Controller Controller { get; set; }
        public bool UpMovementBlocked { get; set; }
        public bool DownMovementBlocked { get; set; }
        public bool LeftMovementBlocked { get; set; }
        public bool RightMovementBlocked { get; set; }
    }
}
