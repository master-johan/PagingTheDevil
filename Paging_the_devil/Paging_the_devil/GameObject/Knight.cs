using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Knight : Player
    {
        public Knight(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base (tex, pos, playerIndex, Controller)
        {
            HealthPoints = ValueBank.KnightHealth;
            maxHealthPoints = HealthPoints;
        }
    }
}
