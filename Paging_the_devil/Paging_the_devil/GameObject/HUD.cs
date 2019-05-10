using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject
{
    class HUD
    {
        Rectangle btnX;
        Rectangle btnA;
        Rectangle btnY;
        Rectangle btnB;
        Rectangle hudBox;
        Rectangle healthBar;
        Rectangle playerIconRect;

        Vector2 pos;

        float maxHealth;

        int healBarWidthMax;
        int healBarWidth;
        int nrOfPlayers;

        double procentHealth;

        Texture2D[] playerIconTex;

        Player player;

        HUDButton aBtn;
        HUDButton bBtn;
        HUDButton xBtn;
        HUDButton yBtn;

        public HUD( Vector2 pos, Player player,int nrOfPlayers)
        {
            this.pos = pos;
            this.player = player;
            this.nrOfPlayers = nrOfPlayers;
           
            hudBox = new Rectangle((int)pos.X, (int)pos.Y, ValueBank.WindowSizeX / 5, ValueBank.WindowSizeY / 8);
            healthBar = new Rectangle(hudBox.X + 6, hudBox.Y + 77, hudBox.Width / 2, hudBox.Height / 3);
            playerIconRect = new Rectangle(hudBox.X + 130, hudBox.Y + 5, hudBox.Width / 7, 65);

            btnX = new Rectangle(hudBox.X + 225, hudBox.Y + 45, hudBox.Width / 9, hudBox.Height / 3);
            btnA = new Rectangle(hudBox.X + 274, hudBox.Y + 70, hudBox.Width / 9, hudBox.Height / 3);
            btnB = new Rectangle(hudBox.X + 323, hudBox.Y + 45, hudBox.Width / 9, hudBox.Height / 3);
            btnY = new Rectangle(hudBox.X + 274, hudBox.Y + 15, hudBox.Width / 9, hudBox.Height / 3);

            xBtn = new HUDButton(TextureBank.hudTextureList[2], new Vector2(btnX.X, btnX.Y), btnX, player.Ability1);
            aBtn = new HUDButton(TextureBank.hudTextureList[0], new Vector2(btnA.X, btnA.Y), btnA, player.Ability2);
            bBtn = new HUDButton(TextureBank.hudTextureList[1], new Vector2(btnB.X, btnB.Y), btnB, player.Ability3);

            healBarWidthMax = healBarWidth = healthBar.Width;
            maxHealth = player.HealthPoints;

            playerIconTex = new Texture2D[4];
        }

        public void Update(GameTime gameTime)
        {
            ChoosingPlayerIcon();

            if (player.HealthPoints < maxHealth)
            {
                procentHealth = player.HealthPoints / (double)maxHealth;

                double healthFloat = healBarWidthMax * procentHealth;
                healBarWidth = (int)healthFloat;
                healthBar.Width = healBarWidth;
            }

            xBtn.Update(gameTime);
            aBtn.Update(gameTime);
            bBtn.Update(gameTime);

            xBtn.GetCooldownTimer(player.Ability1CooldownTimer);
            aBtn.GetCooldownTimer(player.Ability2CooldownTimer);
            bBtn.GetCooldownTimer(player.Ability3CooldownTimer);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tex)
        {
            spriteBatch.Draw(tex, hudBox, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.3f);

            DrawPlayerIcon(spriteBatch);

            spriteBatch.Draw(TextureBank.hudTextureList[18], healthBar, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            spriteBatch.Draw(TextureBank.hudTextureList[3], btnY, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            spriteBatch.Draw(TextureBank.hudTextureList[17], hudBox, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.4f);

            xBtn.Draw(spriteBatch);
            aBtn.Draw(spriteBatch);
            bBtn.Draw(spriteBatch);
        }

        /// <summary>
        /// Denna metoden ritar ut karaktären som valts i respektive HUD
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawPlayerIcon(SpriteBatch spriteBatch)
        {
            if (player is Knight)
            {
                spriteBatch.Draw(playerIconTex[0], playerIconRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
            else if (player is Barbarian)
            {
                spriteBatch.Draw(playerIconTex[1], playerIconRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
            else if (player is Druid)
            {
                spriteBatch.Draw(playerIconTex[2], playerIconRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
            else if (player is Ranger)
            {
                spriteBatch.Draw(playerIconTex[3], playerIconRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
        }
        /// <summary>
        /// Denna metoden Sätter rätt texture till den valda karaktären
        /// </summary>
        private void ChoosingPlayerIcon()
        {
            playerIconTex[0] = TextureBank.hudTextureList[15];
            playerIconTex[1] = TextureBank.hudTextureList[13];
            playerIconTex[2] = TextureBank.hudTextureList[14];
            playerIconTex[3] = TextureBank.hudTextureList[16];     
        }

    }
}
