using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
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
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
        /// <summary>
        /// Den här metoden sköter riktningen på spells.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        protected Vector2 GetSpellDirection(Vector2 direction)
        {
            Vector2 spellDirection;
            spellDirection = direction;
            spellDirection.Normalize();
            spellDirection.Y = -spellDirection.Y;
            return spellDirection;
        }
        /// <summary>
        /// Den här metoden uppdaterar hitboxen
        /// </summary>
        protected void UpdateRect()
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
        }
        /// <summary>
        /// Den här metoden gör skada
        /// </summary>
        protected virtual void ApplyDamage()
        {
            HitCharacter.HealthPoints -= Damage;
        }
        /// <summary>
        /// Den här metoden healar
        /// </summary>
        protected virtual void ApplyHeal()
        {
            HitCharacter.HealthPoints += Heal;
        }
    }
}
