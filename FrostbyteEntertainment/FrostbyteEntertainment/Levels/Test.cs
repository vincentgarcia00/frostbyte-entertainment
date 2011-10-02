using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostbyte.Levels
{
    internal static class Test
    {
        internal static void Load()
        {
            FrostbyteLevel l = (This.Game.CurrentLevel != This.Game.NextLevel && This.Game.NextLevel != null ? This.Game.NextLevel : This.Game.CurrentLevel) as FrostbyteLevel;
            //l.Background = new Background("LALA", "none");

            LevelFunctions.Spawn(delegate()
            {
                return new FerociousEnemy("e1", new Actor(new DummyAnimation("e1")));
            }, 10, new Microsoft.Xna.Framework.Vector2(50, 50));

            Characters.Mage mage= new Characters.Mage("mage", new Actor(new DummyAnimation("mage")));
            mage.Pos = new Microsoft.Xna.Framework.Vector2(50, 50);
            l.Camera.Pos = mage.Pos - new Microsoft.Xna.Framework.Vector2(This.Game.GraphicsDevice.Viewport.Width / 2,
                This.Game.GraphicsDevice.Viewport.Height / 2);
        }

        internal static void Update()
        {

        }
    }
}
