using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Frostbyte
{
    abstract public class WorldObject : IComparable<WorldObject>
    {
        

        public WorldObject(int z = 0)
        {
            ZOrder = z;
            mVisible = true;
            mTransparency = 1;
            mAngle = 0;
        }

        #region Methods
        /// <summary>
        /// Draws the obejct
        /// </summary>
        /// <param name="gameTime">The gametime for the drawing frame.</param>
        public abstract void Draw(Microsoft.Xna.Framework.GameTime gameTime);

        /// <summary>
        /// 
        /// </summary>
        public void DoCollisions()
        {
            if (mCollidesWithBackground)
            {
                CollideWithBackground();
            }
            CollisionBehavior();
        }

        protected virtual void CollideWithBackground()
        {
            throw new NotImplementedException();
        }
        #endregion Methods

        internal Behavior CollisionBehavior = () => { };

        #region Properties
        /// <summary>
        ///     gets the sprite's name
        /// </summary>
        public string Name { get { return mName; } }

        /// <summary>
        /// Sets the Sprite's transparency.
        /// </summary>
        /// <param name="f">The sprite's transparancy [0,1] other values will be force set </param>
        public float Transparency
        {
            get { return mTransparency; }
            set { mTransparency = value > 1 ? 1 : value < 0 ? 0 : value; }
        }
        /// <summary>
        /// Angle in degrees.
        /// </summary>
        /// <returns>Angle in degrees</returns>
        public float Angle
        {
            get { return mAngle; }
            set
            {
                float a = value;
                mAngle = a;
                while (a > 360)
                    a -= 360;
            }
        }

        /// <summary>
        /// The current (x,y) position
        /// </summary>
        public Vector2 Pos = new Vector2(0, 0);

        /// <summary>
        /// Stacking order. Determines what draws on top.
        /// </summary>
        public int ZOrder;

        /// <summary>
        /// Sprite's scale for drawing
        /// </summary>
        public Vector2 Scale = new Vector2(1, 1);

        /// <summary>
        /// Determines whether or not the WorldObject is transformed by the camera or not
        /// </summary>
        public bool Static { get; set; }

        #endregion Properties

        #region Variables
        /// <summary>
        /// Sprite's name
        /// </summary>
        protected string mName;

        /// <summary>
        /// Determine if Object should be visible
        /// </summary>
        public bool mVisible;
        /// <summary>
        /// Transparency!
        /// </summary>
        protected float mTransparency;

        /// <summary>
        /// Angle of rotation
        /// </summary>
        protected float mAngle;

        private bool mCollidesWithBackground = true;

        /// <summary>
        /// \todo get rid of this
        /// this is to make it work
        /// </summary>
        internal CollisionObject Col { get; set; }
        #endregion Variables

        internal abstract List<CollisionObject> GetCollision();

        /// <summary>
        /// Allows sorting
        /// </summary>
        /// <param name="other">value with which to compare</param>
        /// <returns></returns>
        public int CompareTo(WorldObject other)
        {
            return ZOrder.CompareTo(other.ZOrder);
        }

        /// <summary>
        /// Collision list for World objects Objects in the same list are not checked against eachother
        /// Background's collision defaults to list 0
        /// All other objects default to 1
        /// </summary>
        public int CollisionList = 1;
    }
}
