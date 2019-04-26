﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;



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
        Vector2 pos;
        float health;
        float maxHealth;
        int healBarWidthMax;
        int healBarWidth;
        double procentHealth;
        Player player;
        HUDButton aBtn;
        HUDButton bBtn;
        HUDButton xBtn;
        HUDButton yBtn;


        public HUD( Vector2 pos, Player player)
        {
            this.pos = pos;
            this.player = player;
           
            hudBox = new Rectangle((int)pos.X, (int)pos.Y, TextureManager.WindowSizeX / 5, TextureManager.WindowSizeY / 8);
            healthBar = new Rectangle(hudBox.X + 12, hudBox.Y + 45, hudBox.Width / 2, hudBox.Height / 3);
            btnX = new Rectangle(hudBox.X + 225, hudBox.Y + 45, hudBox.Width / 9, hudBox.Height / 3);
            btnA = new Rectangle(hudBox.X + 274, hudBox.Y + 70, hudBox.Width / 9, hudBox.Height / 3);
            btnB = new Rectangle(hudBox.X + 323, hudBox.Y + 45, hudBox.Width / 9, hudBox.Height / 3);
            btnY = new Rectangle(hudBox.X + 274, hudBox.Y + 15, hudBox.Width / 9, hudBox.Height / 3);

            xBtn = new HUDButton(TextureManager.hudTextureList[2], new Vector2(btnX.X, btnX.Y), btnX, player.Ability1);
            aBtn = new HUDButton(TextureManager.hudTextureList[0], new Vector2(btnA.X, btnA.Y), btnA, player.Ability2);
            bBtn = new HUDButton(TextureManager.hudTextureList[1], new Vector2(btnB.X, btnB.Y), btnB, player.Ability3);

            healBarWidthMax = healBarWidth = healthBar.Width;
            
            maxHealth = player.HealthPoints;
        }

        public void Update(GameTime gameTime)
        {
            if (player.HealthPoints < maxHealth)
            {
                procentHealth = (double)player.HealthPoints / (double)maxHealth;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.hudTextureList[4], hudBox, Color.White);
            spriteBatch.Draw(TextureManager.hudTextureList[1], healthBar, Color.White);
            //spriteBatch.Draw(TextureManager.hudTextureList[2], btnX, Color.White);
            //spriteBatch.Draw(TextureManager.hudTextureList[0], btnA, Color.White);
            //spriteBatch.Draw(TextureManager.hudTextureList[1], btnB, Color.White);
            spriteBatch.Draw(TextureManager.hudTextureList[3], btnY, Color.White);

            xBtn.Draw(spriteBatch);
            aBtn.Draw(spriteBatch);
            bBtn.Draw(spriteBatch);

        }
    }
}
