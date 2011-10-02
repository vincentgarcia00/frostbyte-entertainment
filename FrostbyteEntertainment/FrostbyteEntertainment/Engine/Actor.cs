using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostbyte
{
    /// <summary>
    /// This class is tasked with keeping track of the
    /// curent Animation's state.
    /// </summary>
    class Actor
    {
        #region Properties
        /// <summary>
        /// The frame number.
        /// </summary>
        public int Frame { get; set; }

        /// <summary>
        /// Index of the current loaded animation.
        /// </summary>
        public int CurrentAnimation { get; set; }
        #endregion

        #region Variables
        /// <summary>
        /// List of all the Actor's animations.
        /// </summary>
        public List<Animation> Animations = new List<Animation>();
        #endregion

        #region Constructor
        public Actor(Animation anim)
        {
            Animations.Add(anim);
        }
        #endregion
    }
}

