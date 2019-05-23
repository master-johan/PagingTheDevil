using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject
{
    class InfoBox : GameObject
    {
        double procentHealth;
        double healBarWidthMax;
        int healBarWidth;

        Rectangle healthBar;
        Rectangle maxHealthBar;

        Vector2 devilTextPos;
        Vector2 infoTextPos;

        string devilHealth;
        string infoText;
        string infoDoor;

        double maxHealth;

        List<Enemy> enemyList;

        Enemy enemy;

        bool once = false;
        bool enemiesDead = false; 

        public InfoBox(Texture2D tex, Vector2 pos, List<Enemy> enemyList) : base(tex, pos)
        {
            this.enemyList = enemyList;

            healthBar = new Rectangle((int)pos.X + 80, (int)pos.Y + 77, tex.Width / 2, tex.Height / 3);
            maxHealthBar = new Rectangle((int)pos.X + 80, (int)pos.Y + 77, tex.Width / 2, tex.Height / 3);

            devilTextPos = new Vector2((int)pos.X + 60, (int)pos.Y + 25);
            infoTextPos = new Vector2((int)pos.X + 60, (int)pos.Y + 25);

            devilHealth ="Devil Health";
            infoText = "Kill all enemies to proceed!";
            infoDoor = "Press Y at the open door \n to proceed";
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var e in enemyList)
            {
                if (e is Devil)
                {
                    enemy = e;
                }
                else
                {
                    enemy = e;
                }
            }
            if (enemy as Devil != null && !once)
            {
                maxHealth = enemy.HealthPoints;
                healBarWidthMax = healBarWidth = healthBar.Width;
                once = true;
            }
            if (enemy as Devil != null)
            {
                procentHealth = enemy.HealthPoints / (double)maxHealth;

                double healthFloat = healBarWidthMax * procentHealth;
                healBarWidth = (int)healthFloat;
                healthBar.Width = healBarWidth;
            }
            if (enemyList.Count != 0 && enemy as Devil == null)
            {
                enemiesDead = false;
            }
            else if (enemyList.Count == 0)
            {
                enemiesDead = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
            if (enemy as Devil != null)
            {
                spriteBatch.DrawString(TextureBank.spriteFont, devilHealth, devilTextPos, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
                spriteBatch.Draw(TextureBank.menuTextureList[3], maxHealthBar, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
                spriteBatch.Draw(TextureBank.hudTextureList[18], healthBar, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            }
            else if (enemiesDead == false)
            {
                spriteBatch.DrawString(TextureBank.spriteFontInfo, infoText, infoTextPos, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
            }
            else if (enemiesDead == true)
            {
                spriteBatch.DrawString(TextureBank.spriteFontInfo, infoDoor, infoTextPos, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
            }

            

        }

        public void GetEnemyList(List<Enemy> enemyList)
        {
            this.enemyList = enemyList;
        }
    }
}
