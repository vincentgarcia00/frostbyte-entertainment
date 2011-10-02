using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frostbyte
{
    public abstract class CollisionObject
    {
        /// <summary>
        /// Id that will be used to determine which CollisionObject was touched.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The type of CollisionObject (ex: c = circle, a = AABB, o = OBB, e = Ellipse)
        /// </summary>
        public char type { get; set; }

        /// <summary>
        /// Previous bucket locations
        /// </summary>
        public List<Vector2> bucketLocations = new List<Vector2>();

        public Vector2 previousPos = new Vector2();

        /// <summary>
        /// Determines which grid cells the object is in
        /// </summary>
        public abstract List<Vector2> gridLocations(WorldObject worldObject);

        /// <summary>
        /// Add this CollisionObject to bucket.
        /// </summary>
        public abstract void addToBucket(WorldObject worldObject);

        /// <summary>
        /// Adds The world object to all cell contained within the four points
        /// </summary>
        /// <param name="worldObject">Object to add</param>
        /// <param name="bottomLeftX">Bottom left xcoord point of the rect</param>
        /// <param name="bottomLeftY">Bottom left ycoord point of the rect</param>
        /// <param name="topRightX">Top Right xcoord point of the rect</param>
        /// <param name="topRightY">Top Right ycoord point of the rect</param>
        public void AddToBucket(WorldObject worldObject, int bottomLeftX, int bottomLeftY, int topRightX, int topRightY)
        {
            for (int i = bottomLeftX; i <= topRightX; i++) //cols
            {
                for (int j = bottomLeftY; j <= topRightY; j++) //rows
                {
                    Dictionary<Vector2,List<WorldObject>> bucket = Collision.Buckets[worldObject.CollisionList];
                    Vector2 location = new Vector2(i, j);
                    List<WorldObject> value;
                    if(bucket.TryGetValue(location, out value))
                    {
                        value.Add(worldObject);
                    }
                    else
                    {
                        value = new List<WorldObject>();
                        value.Add(worldObject);
                        bucket[location] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Holds the points that make up the linestrip for drawing
        /// </summary>
        public VertexPositionColor[] drawPoints;

        public abstract void draw(WorldObject world, Matrix transformation);

        public override int GetHashCode()
        {
            return id;
        }
    }
}
