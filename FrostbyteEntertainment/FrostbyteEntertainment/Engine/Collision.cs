using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Frostbyte
{
    public static class Collision
    {
        /// <summary>
        /// The Buckets of dictionaries for collision detection
        /// </summary>
        public static List<Dictionary<Vector2, List<WorldObject>>> Buckets = new List<Dictionary<Vector2, List<WorldObject>>>();

        /// <summary>
        /// List of the Collision data to check
        /// </summary>
        public static List<KeyValuePair<int, int>> Lists = new List<KeyValuePair<int, int>>();

        public static int gridCellHeight { get; set; }
        public static int gridCellWidth { get; set; }

        //Tuple value defs: 1=Key's Collision Object;  2=WorldObject that collided with Key;  3=Value 2's Key that Collided
        public static Dictionary<WorldObject, List<Tuple<CollisionObject, WorldObject, CollisionObject>>> collisionData;
        public static bool ShowCollisionData { get; set; }
        public static BasicEffect basicEffect = new BasicEffect(This.Game.GraphicsDevice);

        public static void fillBuckets()
        {
            /// \todo This needs to dissapear. Should only ever happen once
            var BG = This.Game.CurrentLevel.Background;
            foreach (var obj in BG.GetObjects())
            {
                //make sure we've got a bucket list
                while (Collision.Buckets.Count - 1 < obj.CollisionList)
                {
                    Collision.Buckets.Add(new Dictionary<Vector2, List<WorldObject>>());
                }
                obj.Col.addToBucket(obj);
            }

            foreach (WorldObject worldObject in This.Game.CurrentLevel.mSprites)
            {
                //make sure we've got a bucket list
                while (Collision.Buckets.Count - 1 < worldObject.CollisionList)
                {
                    Collision.Buckets.Add(new Dictionary<Vector2, List<WorldObject>>());
                }
                foreach (CollisionObject collisionObject in worldObject.GetCollision())
                {
                    collisionObject.addToBucket(worldObject);
                }
            }
        }

        public static void detectCollisions()
        {
            if (This.Game.CurrentLevel.mSprites.Count > 0)
            {
                collisionData = new Dictionary<WorldObject, List<Tuple<CollisionObject, WorldObject, CollisionObject>>>();

                //for each list we care about checking we add them
                foreach (var pair in Lists)
                {
                    var bucket1 = Buckets[pair.Key];
                    var bucket2 = Buckets[pair.Value];
                    foreach (var dict1 in bucket1)
                    {
                        var key = dict1.Key;
                        List<WorldObject> list1 = dict1.Value;
                        List<WorldObject> list2;
                        if (bucket2.TryGetValue(key, out list2))
                        {
                            foreach (var item in list1)
                            {
                                foreach (var item2 in list2)
                                {
                                    List<Tuple<CollisionObject, CollisionObject>> detectedCollisions = detectCollision(item, item2);
                                    if (detectedCollisions.Count != 0)
                                    {
                                        List<Tuple<CollisionObject, WorldObject, CollisionObject>> collisionFront;
                                        List<Tuple<CollisionObject, WorldObject, CollisionObject>> collisionK;
                                        if (!collisionData.TryGetValue(item, out collisionFront))
                                        {
                                            collisionData[item] = collisionFront = new List<Tuple<CollisionObject, WorldObject, CollisionObject>>();
                                        }
                                        if (!collisionData.TryGetValue(item2, out collisionK))
                                        {
                                            collisionData[item2] = collisionK = new List<Tuple<CollisionObject, WorldObject, CollisionObject>>();
                                        }

                                        foreach (Tuple<CollisionObject, CollisionObject> tuple in detectedCollisions)
                                        {
                                            collisionData[item].Add(new Tuple<CollisionObject, WorldObject, CollisionObject>(tuple.Item1, item2, tuple.Item2));
                                            collisionData[item2].Add(new Tuple<CollisionObject, WorldObject, CollisionObject>(tuple.Item2, item, tuple.Item1));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    KeyValuePair<Vector2, List<WorldObject>> bucketElem;
                    var bucket = Buckets[pair.Key];
                    while (bucket.Count > 0)
                    {
                        bucketElem = bucket.First();
                        bucket.Remove(bucketElem.Key);
                        List<WorldObject> list = bucketElem.Value;

                        while (list.Count > 1)
                        {
                            WorldObject front = list.First();
                            list.RemoveAt(0);
                            for (int k = 0; k < list.Count; k++)
                            {
                                List<Tuple<CollisionObject, CollisionObject>> detectedCollisions = detectCollision(front, list[k]);
                                if (detectedCollisions.Count != 0)
                                {
                                    List<Tuple<CollisionObject, WorldObject, CollisionObject>> collisionFront;
                                    List<Tuple<CollisionObject, WorldObject, CollisionObject>> collisionK;
                                    if (!collisionData.TryGetValue(front, out collisionFront))
                                    {
                                        collisionData[front] = collisionFront = new List<Tuple<CollisionObject, WorldObject, CollisionObject>>();
                                    }
                                    if (!collisionData.TryGetValue(list[k], out collisionK))
                                    {
                                        collisionData[list[k]] = collisionK = new List<Tuple<CollisionObject, WorldObject, CollisionObject>>();
                                    }

                                    foreach (Tuple<CollisionObject, CollisionObject> tuple in detectedCollisions)
                                    {
                                        collisionData[front].Add(new Tuple<CollisionObject, WorldObject, CollisionObject>(tuple.Item1, list[k], tuple.Item2));
                                        collisionData[list[k]].Add(new Tuple<CollisionObject, WorldObject, CollisionObject>(tuple.Item2, front, tuple.Item1));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #region new
            /*
            List<Thread> threads = new List<Thread>();
            List<List<Collision>> threadResults = new List<List<Collision>>();
            Object thislock = new Object();
            List<Circle>[] values = bucket.Values.ToArray();
            for (int i = 0; i < threadMax; i++)
            {
                threadResults.Add(new List<Collision>());
                int index = i;
                List<Circle>[] valuesPortion = values.Skip(i * values.Length / threadMax).Take(values.Length / threadMax).ToArray();
                Thread t = new Thread(new ThreadStart(delegate()
                    {
                        for(int j=0; j<valuesPortion.Length; j++)
                        {
                            List<Circle> list = valuesPortion[j];

                            if (list.Count <= 1)
                                continue;

                            while (list.Count > 1)
                            {
                                Circle front = list.First();
                                list.RemoveAt(0);
                                for (int k = 0; k < list.Count; k++)
                                {
                                    if (distance(front, list[k]) <= front.radius + list[k].radius)
                                    {
                                        Collision collision = new Collision();
                                        collision.objectOne = front;
                                        collision.objectTwo = list[k];
                                        threadResults[index].Add(collision);
                                    }
                                }
                            }
                        }
                    }));
                threads.Add(t);
                t.Start();
            }
            for (int i = 0; i < threadMax; i++)
                threads[i].Join();

            for (int i = 0; i < threadMax; i++)
                collisions.AddRange(threadResults[i]);
            */
            #endregion
        }

        public static void Draw(Matrix transformation)
        {
            if (ShowCollisionData)
            {
                float height = This.Game.GraphicsDevice.Viewport.Height;
                float width = This.Game.GraphicsDevice.Viewport.Width;
                basicEffect.View = Matrix.CreateLookAt(new Vector3(This.Game.GraphicsDevice.Viewport.X + width / 2, This.Game.GraphicsDevice.Viewport.Y + height / 2, -10),
                                                       new Vector3(This.Game.GraphicsDevice.Viewport.X + width / 2, This.Game.GraphicsDevice.Viewport.Y + height / 2, 0), new Vector3(0, -1, 0));
                basicEffect.Projection = Matrix.CreateOrthographic(This.Game.GraphicsDevice.Viewport.Width, This.Game.GraphicsDevice.Viewport.Height, 1, 20);
                basicEffect.VertexColorEnabled = true;


                foreach (WorldObject world in This.Game.CurrentLevel.mSprites)
                {
                    foreach (CollisionObject collisionObject in world.GetCollision())
                    {
                        collisionObject.draw(world, transformation);
                    }
                }

                var BG = This.Game.CurrentLevel.Background;
                foreach (var col in BG.GetCollision())
                {
                    col.draw(BG, transformation);
                }
            }
        }

        public static void update()
        {
            fillBuckets();
            detectCollisions();
        }

        public static float distanceSquared(Vector2 p1, Vector2 p2)
        {
            Vector2 d = p1 - p2;
            return d.X * d.X + d.Y * d.Y;
        }

        public static List<Tuple<CollisionObject, CollisionObject>> detectCollision(WorldObject w1, WorldObject w2)
        {
            List<Tuple<CollisionObject, CollisionObject>> output = new List<Tuple<CollisionObject, CollisionObject>>();

            List<CollisionObject> w1CollisionObj = w1.GetCollision();
            List<CollisionObject> w2CollisionObj = w2.GetCollision();

            foreach (CollisionObject cw1 in w1CollisionObj)
                foreach (CollisionObject cw2 in w2CollisionObj)
                {

                    if (detectCollision(w1, (dynamic)cw1, w2, (dynamic)cw2))
                    {
                        output.Add(new Tuple<CollisionObject, CollisionObject>(cw1, cw2));
                    }
                }

            return output;
        }

        /// <summary>
        /// Determine if BoundingCircle and BoundingCircle collide
        /// </summary>
        /// /// <param name="w1">This Collision data's world object</param>
        /// <param name="w2">Other collision data's world object</param>
        /// <param name="c2">Collision circle which to check</param>
        /// <returns>Whether a collision occurred</returns>
        public static bool detectCollision(WorldObject w1, Collision_BoundingCircle c1, WorldObject w2, Collision_BoundingCircle o)
        {
            float ds = Collision.distanceSquared(w1.Pos + c1.centerPointOffset, w2.Pos + o.centerPointOffset);
            return (ds <= c1.radius * o.radius);
        }

        /// <summary>
        /// Detects OBB on OBB collision
        /// </summary>
        /// <param name="w1">This Collision data's world object</param>
        /// <param name="w2">Other collision data's world object</param>
        /// <param name="o">Collision circle which to check</param>
        /// <returns>Whether a collision occurred</returns>
        public static bool detectCollision(WorldObject w1, Collision_OBB c1, WorldObject w2, Collision_OBB o)
        {
            return false;
        }

        /// <summary>
        /// Determine whether an AABB and AABB collide
        /// </summary>
        /// <param name="w1">This Collision data's world object</param>
        /// <param name="w2">Other collision data's world object</param>
        /// <param name="o">AABB which to check</param>
        /// <returns>Whether a collision occurred</returns>
        public static bool detectCollision(WorldObject w1, Collision_AABB a1, WorldObject w2, Collision_AABB o)
        {
            Vector2 TL1 = a1.topLeftPointOffset + w1.Pos;
            Vector2 BR1 = a1.bottomRightPointOffset + w1.Pos;
            Vector2 TL2 = o.topLeftPointOffset + w2.Pos;
            Vector2 BR2 = o.bottomRightPointOffset + w2.Pos;

            ///check rect vs rect really just axis aligned box check (simpler)
            bool outsideX = BR1.X < TL2.X || TL1.X > BR2.X;
            bool outsideY = BR1.Y < TL2.Y || TL1.Y > BR2.Y;
            return !(outsideY || outsideX);
        }


        /// <summary>
        /// Determine if AABB and BoundingCircle collide
        /// </summary>
        /// /// <param name="w1">This Collision data's world object</param>
        /// <param name="w2">Other collision data's world object</param>
        /// <param name="a1">AABB which to check</param>
        /// <returns>Whether a collision occurred</returns>
        public static bool detectCollision(WorldObject w1, Collision_BoundingCircle c1, WorldObject w2, Collision_AABB o)
        {
            return detectCollision(w2, o, w1, c1);
        }
        /// <summary>
        /// Determine whether an AABB and Collision circle collide
        /// </summary>
        /// <param name="w1">This Collision data's world object</param>
        /// <param name="w2">Other collision data's world object</param>
        /// <param name="o">Collision circle which to check</param>
        /// <returns>Whether a collision occurred</returns>
        public static bool detectCollision(WorldObject w1, Collision_AABB a1, WorldObject w2, Collision_BoundingCircle o)
        {
            Vector2 centerPoint = o.centerPointOffset + w2.Pos;
            Vector2 topLeftPoint = a1.topLeftPointOffset + w1.Pos;
            Vector2 bottomRightPoint = a1.bottomRightPointOffset + w1.Pos;

            int regionCode = 0;

            if (centerPoint.X < topLeftPoint.X)
                regionCode += 1; // 0001
            if (centerPoint.X > bottomRightPoint.X)
                regionCode += 2; // 0010
            if (centerPoint.Y > topLeftPoint.Y)
                regionCode += 4; // 0100
            if (centerPoint.Y < bottomRightPoint.Y)
                regionCode += 8;

            float radius = o.radius;
            switch (regionCode)
            {
                case 0: //0000
                    return true;
                case 1: //0001
                    if (Math.Abs(topLeftPoint.X - centerPoint.X) <= radius)
                        return true;
                    break;
                case 2: //0010
                    if (Math.Abs(centerPoint.X - bottomRightPoint.X) <= radius)
                        return true;
                    break;
                case 4: //0100
                    if (Math.Abs(centerPoint.Y - topLeftPoint.Y) <= radius)
                        return true;
                    break;
                case 8: //1000
                    if (Math.Abs(bottomRightPoint.Y - centerPoint.Y) <= radius)
                        return true;
                    break;
                case 5: //0101
                case 9: //1001
                    if (Collision.distanceSquared(centerPoint, topLeftPoint) <= radius * radius)
                        return true;
                    break;
                case 6: //0110
                case 10: //1010
                    if (Collision.distanceSquared(centerPoint, bottomRightPoint) <= radius * radius)
                        return true;
                    break;
            }


            return false;
        }

        /// <summary>
        /// Determine if OBB and BoundingCircle collide
        /// </summary>
        /// /// <param name="w1">This Collision data's world object</param>
        /// <param name="w2">Other collision data's world object</param>
        /// <param name="o1">OBB which to check</param>
        /// <returns>Whether a collision occurred</returns>
        public static bool detectCollision(WorldObject w1, Collision_BoundingCircle c1, WorldObject w2, Collision_OBB o)
        {
            return detectCollision(w2, o, w1, c1);
        }
        /// <summary>
        /// Detect of a Collision circle and OBB collide
        /// </summary>
        /// <param name="w1">This Collision data's world object</param>
        /// <param name="w2">Other collision data's world object</param>
        /// <param name="o">Collision circle which to check</param>
        /// <returns>Whether a collision occurred</returns>
        public static bool detectCollision(WorldObject w1, Collision_OBB o1, WorldObject w2, Collision_BoundingCircle o)
        {
            Vector2 c1Center = o.centerPointOffset + w2.Pos;
            Vector2 o1Anchor = new Vector2(w1.Pos.X, w1.Pos.Y);

            Vector2 drawPoint0 = new Vector2(o1.drawPoints[0].Position.X + o1Anchor.X, o1.drawPoints[0].Position.Y + o1Anchor.Y);
            Vector2 drawPoint1 = new Vector2(o1.drawPoints[1].Position.X + o1Anchor.X, o1.drawPoints[1].Position.Y + o1Anchor.Y);
            Vector2 drawPoint2 = new Vector2(o1.drawPoints[2].Position.X + o1Anchor.X, o1.drawPoints[2].Position.Y + o1Anchor.Y);
            Vector2 drawPoint3 = new Vector2(o1.drawPoints[3].Position.X + o1Anchor.X, o1.drawPoints[3].Position.Y + o1Anchor.Y);

            Vector2 C = drawPoint1 + Vector2.Dot(c1Center - drawPoint1, Vector2.Normalize(drawPoint1 - drawPoint0)) * Vector2.Normalize(drawPoint1 - drawPoint0);
            Vector2 D = drawPoint1 + Vector2.Dot(c1Center - drawPoint1, Vector2.Normalize(drawPoint1 - drawPoint2)) * Vector2.Normalize(drawPoint1 - drawPoint2);

            float CtoDP1 = Vector2.DistanceSquared(C, drawPoint1);
            float CtoDP0 = Vector2.DistanceSquared(C, drawPoint0);
            float DP0toDP1 = Vector2.DistanceSquared(drawPoint1, drawPoint0);

            float DtoDP1 = Vector2.DistanceSquared(D, drawPoint1);
            float DtoDP2 = Vector2.DistanceSquared(D, drawPoint2);
            float DP2toDP1 = Vector2.DistanceSquared(drawPoint1, drawPoint2);

            float CentertoDP0 = Vector2.DistanceSquared(c1Center, drawPoint0);
            float CentertoDP1 = Vector2.DistanceSquared(c1Center, drawPoint1);
            float CentertoDP2 = Vector2.DistanceSquared(c1Center, drawPoint2);
            float CentertoDP3 = Vector2.DistanceSquared(c1Center, drawPoint3);

            if (((DP0toDP1 + (o.radius * 2) * (o.radius * 2) + 100 >= CtoDP0 + CtoDP1) && (DP2toDP1 + (o.radius * 2) * (o.radius * 2) + 100 >= DtoDP2 + DtoDP1)) ||
                  (CentertoDP0 <= o.radius * o.radius) || (CentertoDP1 <= o.radius * o.radius) || (CentertoDP2 <= o.radius * o.radius) || (CentertoDP3 <= o.radius * o.radius))
                return true;


            return false;
        }

    }
}
