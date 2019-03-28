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
    public enum PlayerState { One, Two, Three, Four };
    class Player : Character
    {
        public bool shoot;
        public PlayerState currentPlayer;
        Controller controller;
        //public bool playerOne;
        //public bool playerTwo;
        bool slash;
        float slashDamage;

        int timer;

        Rectangle spellRect;

        List<Ability> abilityList = new List<Ability>();

        public GamePadCapabilities c1 = GamePad.GetCapabilities(PlayerIndex.One);
        public GamePadCapabilities c2 = GamePad.GetCapabilities(PlayerIndex.Two);

        public Player(Texture2D tex, Vector2 pos, Rectangle rect, Rectangle spellRect) : base(tex, pos, rect)
        {
            slash = false;
            slashDamage = 2.5f;
            this.spellRect = spellRect;
            shoot = false;
            timer = 0;
            //controller = new Controller();
        }

        public override void Update()
        {
            slash = false;

            //KONTROLL KLASS (TEST)
            //if (currentPlayer == PlayerState.One)
            //{
            //    controller.UpdateCtrlOne();
            //}
            //if (currentPlayer == PlayerState.Two)
            //{
            //    controller.UpdateCtrlTwo();
            //}

            if (c1.IsConnected)
            {
                currentPlayer = PlayerState.One;
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                ////controller.UpdateCtrlOne();



                if (c1.HasLeftXThumbStick)
                {
                    pos.X += state.ThumbSticks.Left.X * 3.0f;
                    pos.Y -= state.ThumbSticks.Left.Y * 3.0f;
                }

                if (state.IsButtonDown(Buttons.B))
                {
                    slash = true;
                }
                if (state.IsButtonDown(Buttons.X))
                {
                    if (timer == 0)
                    {
                        Ability ability = new Ability(TextureManager.mageSpellList[0], pos, spellRect, this);
                        abilityList.Add(ability);
                        shoot = true;
                        timer = 60;
                    }
                }
            }

            foreach (var a in abilityList)
            {
                a.Update();
            }

            if (timer > 0)
            {
                timer--;
            }
        }
        public void Update2()
        {
            if (c2.IsConnected)
            {
                currentPlayer = PlayerState.Two;
                GamePadState state = GamePad.GetState(PlayerIndex.Two);

                //controller.UpdateCtrlTwo();

                if (c2.HasLeftXThumbStick)
                {
                    pos.X += state.ThumbSticks.Left.X * 10.0f;
                    pos.Y -= state.ThumbSticks.Left.Y * 10.0f;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(c1.IsConnected)
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
    }
}
