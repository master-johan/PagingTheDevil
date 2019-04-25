using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Ability : GameObject
    {
        protected Vector2 direction;
        public  Texture2D btnTexture { get; protected set;  }
        public int coolDownTime { get; protected set; }

        public float Damage { get; set; }
        public float Heal { get; set; }

        public bool Active { get; set; }
        public bool ToRemove { get; set; }

        public Character HitCharacter { get; set; }

        public Ability(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos)
        {
            this.direction = direction;
        }
        public override void Update()
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }

        protected Vector2 GetSpellDirection(Vector2 direction)
        {
            Vector2 spellDirection;
            spellDirection = direction;
            spellDirection.Normalize();
            spellDirection.Y = -spellDirection.Y;
            return spellDirection;
        }

        protected void UpdateRect()
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
        }

        protected virtual void ApplyDamage()
        {
            HitCharacter.HealthPoints -= Damage;
        }

        protected virtual void ApplyHeal()
        {
            HitCharacter.HealthPoints += Heal;
        }
    }
}
