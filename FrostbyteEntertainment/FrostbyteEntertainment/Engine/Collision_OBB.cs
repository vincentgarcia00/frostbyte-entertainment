using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frostbyte
{
    internal class Collision_OBB : CollisionObject
    {
        /// <summary>
        /// Initializes a Bounding Circle.
        /// </summary>
        internal Collision_OBB(int _id, Vector2 _cornerOffset1, Vector2 _cornerOffset2, float _thickness)
        {
            cornerOffset1 = _cornerOffset1;
            cornerOffset2 = _cornerOffset2;
            thickness = _thickness;
            id = _id;
            type = 'o';
            previousPos = new Vector2(100017, 100017);


            //create collision object's points for drawing
            Vector3 normal = Vector3.Normalize(new Vector3(cornerOffset2.Y - cornerOffset1.Y, cornerOffset1.X - cornerOffset2.X,0));
            drawPoints = new VertexPositionColor[5];
            drawPoints[0].Position = new Vector3(cornerOffset1.X, cornerOffset1.Y, 0f);
            drawPoints[0].Color = Color.Yellow;
            drawPoints[1].Position = new Vector3(cornerOffset2.X, cornerOffset2.Y, 0f);
            drawPoints[1].Color = Color.Green;
            drawPoints[2].Position = new Vector3(cornerOffset2.X + normal.X*thickness, cornerOffset2.Y + normal.Y*thickness, 0f);
            drawPoints[2].Color = Color.Red;
            drawPoints[3].Position = new Vector3(cornerOffset1.X + normal.X * thickness, cornerOffset1.Y + normal.Y * thickness, 0f);
            drawPoints[3].Color = Color.Red;
            drawPoints[4].Position = new Vector3(cornerOffset1.X, cornerOffset1.Y, 0f);
            drawPoints[4].Color = Color.Red;
        }

        /// <summary>
        /// Offset of Corner1 from sprite anchor.
        /// </summary>
        internal Vector2 cornerOffset1 { get; set; }

        /// <summary>
        /// Offset of Corner2 from sprite anchor.
        /// </summary>
        internal Vector2 cornerOffset2 { get; set; }

        /// <summary>
        /// Thickness of box
        /// </summary>
        internal float thickness { get; set; }

        /// <summary>
        /// Determines which grid cells the object is in
        /// </summary>
        internal override List<Vector2> gridLocations(WorldObject worldObject)
        {
            //int bottomLeftX = (int)(parentObject.Pos.X + topLeftPointOffset.X) / (int)Collision.gridCellWidth;
            //int bottomLeftY = (int)(parentObject.Pos.Y + bottomRightPointOffset.Y) / (int)Collision.gridCellHeight;
            //int topRightX = (int)(parentObject.Pos.X + bottomRightPointOffset.X) / (int)Collision.gridCellWidth;
            //int topRightY = (int)(parentObject.Pos.Y + topLeftPointOffset.Y) / (int)Collision.gridCellHeight;

            List<Vector2> gridLocations = new List<Vector2>();
            //for (int i = bottomLeftX; i <= topRightX; i++) //cols
            //{
                //for (int j = bottomLeftY; j <= topRightY; j++) //rows
                //{
                    //Vector2 location = new Vector2(i, j);
                    //gridLocations.Add(location);
                //}
            //}

            return gridLocations;
        }

        /// <summary>
        /// Add this CollisionObject to bucket.
        /// </summary>
        internal override void addToBucket(WorldObject worldObject)
        {
            //if (previousPos == worldObject.Pos)
            //{
            //    foreach (Vector2 location in bucketLocations)
            //    {
            //        try{
            //            Collision.bucket[location].Add(worldObject);
            //        }
            //        catch
            //        {
            //            List<WorldObject> possibleCollisions = new List<WorldObject>();
            //            possibleCollisions.Add(worldObject);
            //            Collision.bucket.Add(location, possibleCollisions);
            //        }
            //    }
            //}
            //else
            //{
            //    int highestY = (int)(worldObject.Pos.Y + drawPoints[0].Position.Y) / (int)Collision.gridCellHeight;
            //    int lowestY = highestY;
            //    int highestX = (int)(worldObject.Pos.X + drawPoints[0].Position.X) / (int)Collision.gridCellHeight;
            //    int lowestX = highestX;
            //    for (int i = 1; i < drawPoints.Length - 1; i++)
            //    {
            //        int y = (int)(worldObject.Pos.Y + drawPoints[i].Position.Y) / (int)Collision.gridCellHeight;
            //        if (y < lowestY)
            //            lowestY = y;
            //        else if (y > highestY)
            //            highestY = y;

            //        int x = (int)(worldObject.Pos.X + drawPoints[i].Position.X) / (int)Collision.gridCellWidth;
            //        if (x < lowestX)
            //            lowestX = x;
            //        else if (x > highestX)
            //            highestX = x;
            //    }


            //    Vector2 o1Anchor = new Vector2(worldObject.Pos.X, worldObject.Pos.Y);
            //    Vector2 drawPoint0 = new Vector2(drawPoints[0].Position.X + o1Anchor.X, drawPoints[0].Position.Y + o1Anchor.Y);
            //    Vector2 drawPoint1 = new Vector2(drawPoints[1].Position.X + o1Anchor.X, drawPoints[1].Position.Y + o1Anchor.Y);
            //    Vector2 drawPoint2 = new Vector2(drawPoints[2].Position.X + o1Anchor.X, drawPoints[2].Position.Y + o1Anchor.Y);
            //    Vector2 drawPoint3 = new Vector2(drawPoints[3].Position.X + o1Anchor.X, drawPoints[3].Position.Y + o1Anchor.Y);
            //    bucketLocations.Clear();
            //    for (int i = lowestX; i <= highestX; i++) //cols
            //    {
            //        for (int j = lowestY; j <= highestY; j++) //rows
            //        {
            //            float slope0to1;
            //            bool isInGrid = false;
            //            try
            //            {
            //                slope0to1 = (drawPoint0.Y - drawPoint1.Y) / (drawPoint0.X - drawPoint1.X);
            //                //slope1to2 = (drawPoint1.Y - drawPoint2.Y) / (drawPoint1.X - drawPoint2.X);

            //                /*if ((slope0to1 * (i * Collision.gridCellWidth) + (drawPoint0.Y - (slope0to1 * (drawPoint0.X))) < ((j + 1) * Collision.gridCellHeight) && slope0to1 * (i * Collision.gridCellWidth) + (drawPoint0.Y - (slope0to1 * (drawPoint0.X))) > (j * Collision.gridCellHeight)) ||
            //                    (slope0to1 * (i * Collision.gridCellWidth) + (drawPoint3.Y - (slope0to1 * (drawPoint3.X))) < ((j + 1) * Collision.gridCellHeight) && slope0to1 * (i * Collision.gridCellWidth) + (drawPoint3.Y - (slope0to1 * (drawPoint3.X))) > (j * Collision.gridCellHeight)) ||
            //                    (slope1to2 * (i * Collision.gridCellWidth) + (drawPoint1.Y - (slope1to2 * (drawPoint1.X))) < ((j + 1) * Collision.gridCellHeight) && slope1to2 * (i * Collision.gridCellWidth) + (drawPoint1.Y - (slope1to2 * (drawPoint1.X))) > (j * Collision.gridCellHeight)) ||
            //                    (slope1to2 * (i * Collision.gridCellWidth) + (drawPoint2.Y - (slope1to2 * (drawPoint2.X))) < ((j + 1) * Collision.gridCellHeight) && slope1to2 * (i * Collision.gridCellWidth) + (drawPoint2.Y - (slope1to2 * (drawPoint2.X))) > (j * Collision.gridCellHeight)))
            //                {*/
            //                if ((slope0to1 * (i * Collision.gridCellWidth) + (drawPoint0.Y - (slope0to1 * (drawPoint0.X))) < ((j + 1) * Collision.gridCellHeight) && slope0to1 * (i * Collision.gridCellWidth) + (drawPoint0.Y - (slope0to1 * (drawPoint0.X))) > (j * Collision.gridCellHeight)) ||
            //                    (slope0to1 * ((i+1) * Collision.gridCellWidth) + (drawPoint0.Y - (slope0to1 * (drawPoint0.X))) < ((j + 1) * Collision.gridCellHeight) && slope0to1 * ((i+1) * Collision.gridCellWidth) + (drawPoint0.Y - (slope0to1 * (drawPoint0.X))) > (j * Collision.gridCellHeight)) ||
            //                    (((j * Collision.gridCellHeight) - drawPoint0.Y) / slope0to1 + drawPoint0.X < ((i + 1) * Collision.gridCellWidth) && ((j * Collision.gridCellHeight) - drawPoint0.Y) / slope0to1 + drawPoint0.X > (i * Collision.gridCellWidth)) ||
            //                    ((((j+1) * Collision.gridCellHeight) - drawPoint0.Y) / slope0to1 + drawPoint0.X < ((i + 1) * Collision.gridCellWidth) && (((j+1) * Collision.gridCellHeight) - drawPoint0.Y) / slope0to1 + drawPoint0.X > (i * Collision.gridCellWidth))
            //                    )
            //                {
            //                    isInGrid = true;
            //                }
            //            }
            //            catch 
            //            { 
            //                isInGrid = true; 
            //            }
            //            if (isInGrid) //check if grid cell is colliding with box
            //            {
            //                Vector2 location = new Vector2(i, j);
            //                bucketLocations.Add(location);
            //                if (Collision.bucket.ContainsKey(location))
            //                {
            //                    Collision.bucket[location].Add(worldObject);
            //                }
            //                else
            //                {
            //                    List<WorldObject> possibleCollisions = new List<WorldObject>();
            //                    possibleCollisions.Add(worldObject);
            //                    Collision.bucket.Add(location, possibleCollisions);
            //                }
            //            }
            //        }
            //    }
            //    previousPos = worldObject.Pos;
            //}
        }

        internal override void draw(WorldObject world, Matrix transformation)
        {
            Collision.basicEffect.World = Matrix.CreateTranslation(new Vector3(world.Pos, 0)) * transformation;

            foreach (EffectPass pass in Collision.basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                This.Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, drawPoints, 0, 4);
            }
        }
    }
}