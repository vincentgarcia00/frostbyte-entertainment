using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostbyte
{
    class Background_Collision : WorldObject
    {
        public Background_Collision(CollisionObject col)
        {
            Col = col;
            CollisionList = 0;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        internal override List<CollisionObject> GetCollision()
        {
            return new List<CollisionObject>() { Col };
        }
    }
}
