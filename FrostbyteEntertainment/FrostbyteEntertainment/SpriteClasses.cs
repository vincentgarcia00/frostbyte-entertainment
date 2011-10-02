using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frostbyte
{
    internal abstract class Player : Sprite
    {
        internal Player(string name, Actor actor)
            : base(name, actor)
        {
            (This.Game.CurrentLevel as FrostbyteLevel).allies.Add(this);
        }
    }

    internal abstract class Enemy : Sprite
    {
        internal Enemy(string name, Actor actor)
            : base(name, actor)
        {
            (This.Game.CurrentLevel as FrostbyteLevel).enemies.Add(this);
        }
    }

    internal class Target : Obstacle
    {
        internal Target(string name, Actor actor)
            : base(name, actor)
        {

        }

        internal override void Draw(GameTime gameTime)
        {
            if (mVisible)
            {
                float height = This.Game.GraphicsDevice.Viewport.Height;
                float width = This.Game.GraphicsDevice.Viewport.Width;
                BasicEffect basicEffect = new BasicEffect(This.Game.GraphicsDevice);
                basicEffect.View = Matrix.CreateLookAt(new Vector3(This.Game.GraphicsDevice.Viewport.X + width / 2, This.Game.GraphicsDevice.Viewport.Y + height / 2, -10),
                                                       new Vector3(This.Game.GraphicsDevice.Viewport.X + width / 2, This.Game.GraphicsDevice.Viewport.Y + height / 2, 0), new Vector3(0, -1, 0));
                basicEffect.Projection = Matrix.CreateOrthographic(This.Game.GraphicsDevice.Viewport.Width, This.Game.GraphicsDevice.Viewport.Height, 1, 20);
                basicEffect.VertexColorEnabled = true;
                basicEffect.World = Matrix.CreateTranslation(new Vector3(Pos, 0)) * This.Game.CurrentLevel.Camera.GetTransformation(This.Game.GraphicsDevice);
                int size = 15;
                VertexPositionColor[] points = new VertexPositionColor[5]{
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y, 0), Color.Red),
                new VertexPositionColor(new Vector3(Pos.X + size, Pos.Y, 0), Color.Red),
                new VertexPositionColor(new Vector3(Pos.X + size, Pos.Y + size, 0), Color.Red),
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y + size, 0), Color.Red),
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y, 0), Color.Red)};
                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    This.Game.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, points, 0, points.Length - 1);
                }
            }
        }
    }

    internal class FerociousEnemy : Enemy
    {
        internal FerociousEnemy(string name, Actor actor)
            : base(name, actor)
        {

        }

        internal override void Draw(GameTime gameTime)
        {
            float height = This.Game.GraphicsDevice.Viewport.Height;
            float width = This.Game.GraphicsDevice.Viewport.Width;
            BasicEffect basicEffect = new BasicEffect(This.Game.GraphicsDevice);
            basicEffect.View = Matrix.CreateLookAt(new Vector3(This.Game.GraphicsDevice.Viewport.X + width / 2, This.Game.GraphicsDevice.Viewport.Y + height / 2, -10),
                                                   new Vector3(This.Game.GraphicsDevice.Viewport.X + width / 2, This.Game.GraphicsDevice.Viewport.Y + height / 2, 0), new Vector3(0, -1, 0));
            basicEffect.Projection = Matrix.CreateOrthographic(This.Game.GraphicsDevice.Viewport.Width, This.Game.GraphicsDevice.Viewport.Height, 1, 20);
            basicEffect.VertexColorEnabled = true;
            basicEffect.World = Matrix.CreateTranslation(new Vector3(Pos, 0)) * This.Game.CurrentLevel.Camera.GetTransformation(This.Game.GraphicsDevice);
            int size = 10;
            VertexPositionColor[] points = new VertexPositionColor[5]{
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y, 0), Color.BlueViolet),
                new VertexPositionColor(new Vector3(Pos.X + size, Pos.Y, 0), Color.BlueViolet),
                new VertexPositionColor(new Vector3(Pos.X + size, Pos.Y + size, 0), Color.BlueViolet),
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y + size, 0), Color.BlueViolet),
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y, 0), Color.BlueViolet)};
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                This.Game.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, points, 0, points.Length - 1);
            }
        }
    }

    internal abstract class Obstacle : Sprite
    {
        internal Obstacle(string name, Actor actor)
            : base(name, actor)
        {
            (This.Game.CurrentLevel as FrostbyteLevel).obstacles.Add(this);
        }
    }

    class OurSprite : Sprite
    {
        internal OurSprite(string name, Actor actor)
            : base(name, actor) { }

        internal OurSprite(string name, Actor actor, int collisionlist)
            : base(name, actor, collisionlist) { }

        #region Properties

        /// <summary>
        /// This will be the base of the world object relative to Pos (for placing in cells) This is defined as Centerpoint + Centerpoint.Y
        /// </summary>
        internal Vector2 GroundPos
        {
            get
            {
                return new Vector2(Center.X, Center.Y) * 2;
            }
        }

        #endregion Properties
    }
}
