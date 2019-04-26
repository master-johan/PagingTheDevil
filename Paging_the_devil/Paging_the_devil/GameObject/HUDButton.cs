using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject
{
    class HUDButton : GameObject
    {
        Rectangle rect;
        Rectangle coolDownRect;
        Rectangle coolDownSrcRect;
        Ability ability;
        Vector2 abilityTexturePos;
        int currentTimer;
        int maxTimer;

        public HUDButton(Texture2D tex, Vector2 pos, Rectangle rect, Ability ability) : base(tex, pos)
        {
            this.rect = rect;
            this.ability = ability;
            abilityTexturePos = new Vector2(pos.X + 5, pos.Y + 5);
            maxTimer = ability.coolDownTime;
            coolDownRect = rect;
            coolDownRect.X = 0;
            coolDownRect.Y = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (currentTimer > 0)
            {
                float procent = (float)currentTimer / (float)maxTimer;
                double height = procent * rect.Height;
                coolDownRect.Height = (int)height;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, Color.White);
            spriteBatch.Draw(ability.btnTexture, abilityTexturePos, Color.White);
            if (currentTimer >0 )
            {
                spriteBatch.Draw(TextureManager.hudTextureList[6],
                                 new Vector2(rect.X +2 ,rect.Y + 5),
                                 coolDownRect,
                                 Color.White,
                                 MathHelper.ToRadians(180f),
                                 new Vector2(40, 40),
                                 1,
                                 SpriteEffects.None,
                                 1);
            }

        }

        public void GetCooldownTimer(int timer)
        {
            currentTimer = timer; 
        }
    }
}
