using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Frostbyte
{
    class SpriteFrame
    {
        #region Properties
        /// <summary>
        /// Image Texture
        /// </summary>
        public Texture2D Image { get; set; }

        /// <summary>
        /// Amount of time to pause between
        /// this frame and next.
        /// </summary>
        public long Pause { get; set; }

        /// <summary>
        /// The offeset from center of the sprite of the image. Defaults to (0,0)
        /// </summary>
        public Vector2 AnimationPeg { get; set; }

        /// <summary>
        /// The frame's width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///  The frame's height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Position of the top left corner
        /// </summary>
        public Vector2 StartPos { get; set; }
        #endregion

        #region Variables
        /// <summary>
        ///  Hot spots that can be used for
        ///  locating objects on the sprite
        ///  default is tagged to center
        ///  of the sprite
        /// </summary>
        public List<Point> HotSpots = new List<Point>();

        /// <summary>
        /// The collision data for this sprite.
        /// </summary>
        public List<CollisionObject> CollisionData = new List<CollisionObject>();
        #endregion
    }
}
