using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Taunt : Ability
    {
        float radius;

        List<Enemy> enemyList;

        Player player;

        public Taunt(Texture2D tex, Vector2 pos, Vector2 direction, Player player) : base(tex, pos, direction)
        {
            this.player = player;
            radius = 200;

            enemyList = new List<Enemy>();
        }

        public override void Update(GameTime gameTime)
        {
            if (HitCharacter != null)
            {
                enemyList.Add(HitCharacter as Enemy);

                foreach (var e in enemyList)
                {
                    e.targetPlayer = player;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
