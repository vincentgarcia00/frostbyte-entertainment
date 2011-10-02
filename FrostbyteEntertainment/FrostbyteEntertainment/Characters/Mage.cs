using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frostbyte.Characters
{
    class Mage : Player
    {
        Sprite currentTarget = null;

        #region Constructors
        public Mage(string name, Actor actor)
            : this(name, actor, PlayerIndex.One)
        {

        }

        internal Mage(string name, Actor actor, PlayerIndex input)
            : base(name, actor)
        {

        }
        #endregion

        #region Variables
        private PlayerIndex gamepad;
        #endregion

        public void Update()
        {
            GamePadState currentState = GamePad.GetState(gamepad);
            if (currentState.IsConnected)
            {
                #region Targeting
                
                #endregion
            }
        }
    }
}
