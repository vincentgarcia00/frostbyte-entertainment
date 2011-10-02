using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostbyte
{
    internal partial class Game : Microsoft.Xna.Framework.Game
    {


        void LoadResources(){
            mLevels.Add(new FrostbyteLevel("TitleScreen", Levels.Test.Load, Levels.Test.Update, delegate() { }, delegate() { return false; }));
        }
    }
}
