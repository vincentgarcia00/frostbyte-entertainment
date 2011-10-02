using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}
