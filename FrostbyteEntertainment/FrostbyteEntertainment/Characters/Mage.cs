using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Frostbyte.Characters
{
    class Mage : Player
    {
        enum TargetAlignment
        {
            Ally,
            Enemy,
            None
        }

        #region Constructors
        public Mage(string name, Actor actor)
            : this(name, actor, PlayerIndex.One)
        {

        }

        internal Mage(string name, Actor actor, PlayerIndex input)
            : base(name, actor)
        {
            controller = new Controller(input);
            currentTargetAlignment = TargetAlignment.None;
            target = new Target("target", new Actor(new DummyAnimation("target")));
            target.mVisible = false;

            UpdateBehavior = Update;
        }
        #endregion

        #region Variables
        private Sprite currentTarget = null;
        private TargetAlignment currentTargetAlignment;
        private Controller controller;
        private Sprite target;
        #endregion

        #region Methods
        /// <summary>
        /// Finds the closest enemey sprite to the player that's further than the current target
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private Sprite findMinimum(List<Sprite> list)
        {
            Sprite nextMin = null;
            foreach (Sprite s in list)
            {
                if (s != this && (currentTarget == null || s != currentTarget))
                {
                    Vector2 targetPos = currentTarget == null ? Pos : currentTarget.Pos;
                    if (nextMin == null ||
                        ((s.Pos - Pos).LengthSquared() >= (targetPos - Pos).LengthSquared() &&
                        (s.Pos - Pos).LengthSquared() < (nextMin.Pos - Pos).LengthSquared()))
                    {
                        nextMin = s;
                    }
                }
            }
            if (nextMin != null)
            {
                return nextMin;
            }
            else
            {
                return currentTarget;
            }
        }

        public void Update()
        {
            controller.Update();
            if (controller.IsConnected)
            {
                #region Targeting
                if (controller.TargetEnemies)
                {
                    if (currentTargetAlignment != TargetAlignment.Enemy)
                    {
                        currentTarget = null;
                    }
                    
                    currentTarget = findMinimum((This.Game.CurrentLevel as FrostbyteLevel).enemies);
                    if (currentTarget != null)
                    {
                        currentTargetAlignment = TargetAlignment.Enemy;
                    }
                }
                else if (controller.TargetAllies)
                {
                    if (currentTargetAlignment != TargetAlignment.None)
                    {
                        currentTarget = null;
                    }

                    currentTarget = findMinimum((This.Game.CurrentLevel as FrostbyteLevel).allies);
                    if (currentTarget != null)
                    {
                        currentTargetAlignment = TargetAlignment.Ally;
                    }
                }
                else if (controller.CancelTargeting == ReleasableButtonState.Clicked)
                {
                    target.mVisible = false;
                    currentTarget = null;
                }

                if (currentTarget != null)
                {
                    target.mVisible = true;
                    target.Pos = currentTarget.Pos + GetAnimation().AnimationPeg - target.GetAnimation().AnimationPeg;
                }
                #endregion
            }
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
            VertexPositionColor[] points = new VertexPositionColor[4]{
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y, 0), Color.AliceBlue),
                new VertexPositionColor(new Vector3(Pos.X + size, Pos.Y, 0), Color.AliceBlue),
                new VertexPositionColor(new Vector3(Pos.X + size, Pos.Y + size, 0), Color.AliceBlue),
                new VertexPositionColor(new Vector3(Pos.X, Pos.Y + size, 0), Color.AliceBlue)};
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                This.Game.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, points, 0, 1);
            }
        }
        #endregion
    }
}
