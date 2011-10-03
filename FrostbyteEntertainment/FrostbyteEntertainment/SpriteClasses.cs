using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Frostbyte
{
    internal abstract class Player : Sprite
    {
        internal Player(string name, Actor actor)
            : base(name, actor)
        {
            (This.Game.CurrentLevel as FrostbyteLevel).allies.Add(this);
        }
    }

    internal abstract class Enemy : Sprite
    {
        internal Enemy(string name, Actor actor)
            : base(name, actor)
        {
            (This.Game.CurrentLevel as FrostbyteLevel).enemies.Add(this);
        }
    }

    internal abstract class Obstacle : Sprite
    {
        internal Obstacle(string name, Actor actor)
            : base(name, actor)
        {
            (This.Game.CurrentLevel as FrostbyteLevel).obstacles.Add(this);
        }
    }

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
