using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Frostbyte
{
    class OurSprite : Sprite
    {
        internal OurSprite(string name, Actor actor)
            : base(name, actor) { }

        internal OurSprite(string name, Actor actor, int collisionlist)
            : base(name, actor, collisionlist) { }

        #region Properties

        /// <summary>
        /// This will be the base of the world object relative to Pos (for placing in cells) This is defined as Centerpoint + Centerpoint.Y
        /// </summary>
        internal Vector2 GroundPos
        {
            get
            {
                return new Vector2(Center.X, Center.Y) * 2;
            }
        }

        #endregion Properties

    }
}
