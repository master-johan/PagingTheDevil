using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.GameObject.Characters
{
    class Player : Character
    {
        float rotation;

        protected float maxHealthPoints;

        int playerIndex;
        int frame;

        double timer;
        double interval;
        
        Rectangle left;
        Rectangle right;
        Rectangle up;
        Rectangle down;
        Rectangle hitboxLeft;
        Rectangle hitboxRight;
        Rectangle hitboxTop;
        Rectangle hitboxBot;

        protected Rectangle drawRect;

        public List<Ability> abilityList;
        
        protected Vector2 inputDirection;

        bool angleRight;
        bool angleLeft;
        bool angleUp;
        bool angleDown;

        public bool Dead { get; set; }

        public int Ability1CooldownTimer { get; protected set; }
        public int Ability2CooldownTimer { get; protected set; }
        public int Ability3CooldownTimer { get; protected set; }

        public Vector2 LastDirection { get; set; }

        public Ability Ability1 { get; protected set; }
        public Ability Ability2 { get; protected set; }
        public Ability Ability3 { get; protected set; }

        public float movementSpeed { get; set; }

        public Rectangle GetTopHitbox { get { return hitboxTop; } }
        public Rectangle GetBotHitbox { get { return hitboxBot; } }
        public Rectangle GetLeftHitbox { get { return hitboxLeft; } }
        public Rectangle GetRightHitbox { get { return hitboxRight; } }

        public Controller Controller { get; set; }
        public bool UpMovementBlocked { get; set; }
        public bool DownMovementBlocked { get; set; }
        public bool LeftMovementBlocked { get; set; }
        public bool RightMovementBlocked { get; set; }

        public Player(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller)
            : base(tex, pos)
        {
            this.playerIndex = playerIndex;
            this.Controller = Controller;

            DecidingSourceRect();
            GenerateRectangles(pos);
            DecidingValues();

            drawRect = down;
            rotation = 0;
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

            movementSpeed = ValueBank.PlayerSpeed;
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

            if(!Dead)
            {
                if (Controller.ButtonPressed(Buttons.X) && Ability1CooldownTimer <= 0)
                {
                    abilityList.Add(CastAbility1());
                }

                else if (Controller.ButtonPressed(Buttons.A) && Ability2CooldownTimer <= 0)
                {
                    abilityList.Add(CastAbility2());
                }

                else if (Controller.ButtonPressed(Buttons.B) && Ability3CooldownTimer <= 0)
                {
                    abilityList.Add(CastAbility3());
                }
            }

            if(Dead)
            {
                rotation = MathHelper.ToRadians(90);
            }

            else
            {
                rotation = 0;
            }

            UpdateAbility(gameTime);

            DecreseCooldownTimers();
            IfHealthIsZero();
            IfHealthIsFull();
            Revive();

            DrawDifferentRects(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var A in abilityList)
            {
                A.Draw(spriteBatch);
            }

            spriteBatch.Draw(tex, pos, drawRect, Color.White, rotation, new Vector2(30, 35), 1, SpriteEffects.None, 1);
        }
        /// <summary>
        /// Den här metoden sköter spelarens rörelse.
        /// </summary>
        private void Movment()
        {
            if (UpMovementBlocked && inputDirection.Y > 0)
            {
                inputDirection.Y = 0;

            }

            else if (DownMovementBlocked && inputDirection.Y < 0)
            {
                inputDirection.Y = 0;
            }

            if (RightMovementBlocked && inputDirection.X > 0)
            {
                inputDirection.X = 0;
            }

            else if (LeftMovementBlocked && inputDirection.X < 0)
            {
                inputDirection.X = 0;
            }

            if (!Dead)
            {
                pos.X += inputDirection.X * movementSpeed;
                pos.Y -= inputDirection.Y * movementSpeed;                
            }
            
        }
        /// <summary>
        /// Den här metoden återställer timers.
        /// </summary>
        private void DecreseCooldownTimers()
        {
            if (Ability1CooldownTimer > 0)
            {
                Ability1CooldownTimer--;
            }

            if (Ability2CooldownTimer > 0)
            {
                Ability2CooldownTimer--;
            }

            if (Ability3CooldownTimer > 0)
            {
                Ability3CooldownTimer--;
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar abilities samt tar bort abilities.
        /// </summary>
        private void UpdateAbility(GameTime gameTime)
        {
            Ability toRemove = null;

            foreach (var A in abilityList)
            {
                A.Update(gameTime);
                if ( A is Slash)
                {
                    if (!(A as Slash).Active)
                    {
                        toRemove = A;
                    }
                }

                if(A is Cleave)
                {
                    if(!(A as Cleave).Active)
                    {
                        toRemove = A;
                    }
                }

                if (A is Dash)
                {
                    if (!(A as Dash).Active)
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
            LastDirection = direction;
        }
        /// <summary>
        /// Den här metoden uppdaterar vilken bild som ska ritas ut beroende på hur man styr sin gubbe. 
        /// </summary>
        private void DrawDifferentRects(GameTime gameTime)
        {
            if(!Dead)
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
                            PlayerAnimation(left);
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
                            PlayerAnimation(right);
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
                            PlayerAnimation(down);
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
                            PlayerAnimation(up);
                        }
                    }
                }
            }
            
        }
        /// <summary>
        /// Den här metoden animerar karaktärerna
        /// </summary>
        /// <param name="rect"></param>
        private void PlayerAnimation(Rectangle rect)
        {
            if (timer <= 0)
            {
                timer = interval; frame++;
                drawRect.Y = rect.Y + (frame % 3) * 60 + (5 * (frame % 3));
            }
        }
        /// <summary>
        /// Den här metoden sköter vad som händer vid en död spelare
        /// </summary>
        public void IfHealthIsZero()
        {
            if(HealthPoints <= 0)
            {
                HealthPoints = 0;
                Dead = true;
            }
        }
        /// <summary>
        /// Den här metoden sköter vad som händer när hp = max
        /// </summary>
        public void IfHealthIsFull()
        {
            if(HealthPoints >= maxHealthPoints)
            {
                HealthPoints = maxHealthPoints;
            }
        }
        /// <summary>
        /// Den här metoden sköter vad som händer vid hp över 0
        /// </summary>
        public void Revive()
        {
            if(HealthPoints > 0)
            {
                Dead = false;
            }
        }

        protected virtual Ability CastAbility1()
        {
            return null;
        }
        protected virtual Ability CastAbility2()
        {
            return null;
        }
        protected virtual Ability CastAbility3()
        {
            return null;
        }
    }
}
