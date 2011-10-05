using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frostbyte
{
    internal enum ReleasableButtonState
    {
        Pressed,
        Released,
        Clicked
    }

    interface IController
    {
        void Update();
        bool IsConnected { get; }

        /// <summary>
        /// Determines whether or not the Earth element was selected on the controller
        /// </summary>
        ReleasableButtonState Earth { get; }

        /// <summary>
        /// Determines whether or not the Fire element was selected on the controller
        /// </summary>
        ReleasableButtonState Fire { get; }

        /// <summary>
        /// Determines whether or not the Water element was selected on the controller
        /// </summary>
        ReleasableButtonState Water { get; }

        /// <summary>
        /// Determines whether or not the Lightning element was selected by the controller.
        /// </summary>
        ReleasableButtonState Lightning { get; }


        /// <summary>
        /// Determines whether or not the button for cancelling targeting was pressed
        /// </summary>
        ReleasableButtonState CancelTargeting { get; }

        /// <summary>
        /// Determines whether or not the button for interacting with the environment was pressed.
        /// </summary>
        ReleasableButtonState Interact { get; }

        /// <summary>
        /// Provides a float representing how far down the sword trigger is pressed.
        /// </summary>
        float Sword { get; }

        /// <summary>
        /// Determines whether or not the button for targeting allies was pressed
        /// </summary>
        bool TargetAllies { get; }

        /// <summary>
        /// Determines whether or not the button for targeting enemies was pressed
        /// </summary>
        bool TargetEnemies { get; }

        /// <summary>
        /// Returns the value of the left joystick
        /// </summary>
        Vector2 Movement { get; }
    }

    class GamePadController : IController
    {
        #region Constructors
        internal GamePadController(PlayerIndex ix)
        {
            input = ix;
            mCurrentControllerState = GamePad.GetState(PlayerIndex.One);
            mLastControllerState = mCurrentControllerState;
        }
        #endregion

        #region Variables
        private PlayerIndex input;
        private GamePadState mLastControllerState;
        protected GamePadState mCurrentControllerState;
        private float InteractElementThreshold = 0.6f;
        #endregion

        #region Methods
        public void Update()
        {
            mLastControllerState = mCurrentControllerState;
            mCurrentControllerState = GamePad.GetState(PlayerIndex.One);
        }

        public GamePadState getRawState()
        {
            return mCurrentControllerState;
        }
        #endregion

        #region Properties
        private GamePadButtons LastButtons { get { return mLastControllerState.Buttons; } }
        private GamePadButtons CurrentButtons { get { return mCurrentControllerState.Buttons; } }

        public bool IsConnected { get { return mCurrentControllerState.IsConnected; } }


        public ReleasableButtonState Earth
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > InteractElementThreshold)
                {
                    if (CurrentButtons.A == ButtonState.Pressed)
                    {
                        return ReleasableButtonState.Pressed;
                    }
                    else if (LastButtons.A == ButtonState.Pressed && CurrentButtons.A == ButtonState.Released)
                    {
                        return ReleasableButtonState.Clicked;
                    }
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Fire
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > InteractElementThreshold)
                {
                    if (CurrentButtons.B == ButtonState.Pressed)
                    {
                        return ReleasableButtonState.Pressed;
                    }
                    else if (LastButtons.B == ButtonState.Pressed && CurrentButtons.B == ButtonState.Released)
                    {
                        return ReleasableButtonState.Clicked;
                    }
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Water
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > InteractElementThreshold)
                {
                    if (CurrentButtons.X == ButtonState.Pressed)
                    {
                        return ReleasableButtonState.Pressed;
                    }
                    else if (LastButtons.X == ButtonState.Pressed && CurrentButtons.X == ButtonState.Released)
                    {
                        return ReleasableButtonState.Clicked;
                    }
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Lightning
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > InteractElementThreshold)
                {
                    if (CurrentButtons.Y == ButtonState.Pressed)
                    {
                        return ReleasableButtonState.Pressed;
                    }
                    else if (LastButtons.Y == ButtonState.Pressed && CurrentButtons.Y == ButtonState.Released)
                    {
                        return ReleasableButtonState.Clicked;
                    }
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Interact
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left <= InteractElementThreshold)
                {
                    if (CurrentButtons.B == ButtonState.Pressed)
                    {
                        return ReleasableButtonState.Pressed;
                    }
                    else if (LastButtons.B == ButtonState.Pressed && CurrentButtons.B == ButtonState.Released)
                    {
                        return ReleasableButtonState.Clicked;
                    }
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState CancelTargeting
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left <= InteractElementThreshold)
                {
                    if (CurrentButtons.B == ButtonState.Pressed)
                    {
                        return ReleasableButtonState.Pressed;
                    }
                    else if (LastButtons.B == ButtonState.Pressed && CurrentButtons.B == ButtonState.Released)
                    {
                        return ReleasableButtonState.Clicked;
                    }
                }

                return ReleasableButtonState.Released;
            }
        }

        public bool TargetAllies
        {
            get
            {
                return LastButtons.LeftShoulder == ButtonState.Pressed &&
                    CurrentButtons.LeftShoulder == ButtonState.Released;
            }
        }

        public bool TargetEnemies
        {
            get
            {
                return LastButtons.RightShoulder == ButtonState.Pressed &&
                    CurrentButtons.RightShoulder == ButtonState.Released;
            }
        }

        public float Sword
        {
            get
            {
                return mCurrentControllerState.Triggers.Right;
            }
        }

        public Vector2 Movement
        {
            get
            {
                return mCurrentControllerState.ThumbSticks.Left;
            }
        }

        #endregion
    }

    class KeyboardController : IController
    {
        #region Constructors
        internal KeyboardController()
        {
            mCurrentControllerState = Keyboard.GetState();
            mLastControllerState = mCurrentControllerState;
        }
        #endregion

        #region Variables
        private KeyboardState mLastControllerState;
        protected KeyboardState mCurrentControllerState;
        #endregion

        #region Methods
        public void Update()
        {
            mLastControllerState = mCurrentControllerState;
            mCurrentControllerState = Keyboard.GetState();
        }
        #endregion

        #region Properties
        public bool IsConnected { get { return true; } }

        public ReleasableButtonState Earth
        {
            get
            {
                if (mCurrentControllerState.IsKeyDown(Keys.S))
                {
                    return ReleasableButtonState.Pressed;
                }
                else if (mLastControllerState.IsKeyDown(Keys.S) && mCurrentControllerState.IsKeyUp(Keys.S))
                {
                    return ReleasableButtonState.Clicked;
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Fire
        {
            get
            {
                if (mCurrentControllerState.IsKeyDown(Keys.D))
                {
                    return ReleasableButtonState.Pressed;
                }
                else if (mLastControllerState.IsKeyDown(Keys.D) && mCurrentControllerState.IsKeyUp(Keys.D))
                {
                    return ReleasableButtonState.Clicked;
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Water
        {
            get
            {
                if (mCurrentControllerState.IsKeyDown(Keys.A))
                {
                    return ReleasableButtonState.Pressed;
                }
                else if (mLastControllerState.IsKeyDown(Keys.A) && mCurrentControllerState.IsKeyUp(Keys.A))
                {
                    return ReleasableButtonState.Clicked;
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Lightning
        {
            get
            {
                if (mCurrentControllerState.IsKeyDown(Keys.W))
                {
                    return ReleasableButtonState.Pressed;
                }
                else if (mLastControllerState.IsKeyDown(Keys.W) && mCurrentControllerState.IsKeyUp(Keys.W))
                {
                    return ReleasableButtonState.Clicked;
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState Interact
        {
            get
            {
                if (mCurrentControllerState.IsKeyDown(Keys.Z))
                {
                    return ReleasableButtonState.Pressed;
                }
                else if (mLastControllerState.IsKeyDown(Keys.Z) && mCurrentControllerState.IsKeyUp(Keys.Z))
                {
                    return ReleasableButtonState.Clicked;
                }

                return ReleasableButtonState.Released;
            }
        }

        public ReleasableButtonState CancelTargeting
        {
            get
            {
                if (mCurrentControllerState.IsKeyDown(Keys.C))
                {
                    return ReleasableButtonState.Pressed;
                }
                else if (mLastControllerState.IsKeyDown(Keys.C) && mCurrentControllerState.IsKeyUp(Keys.C))
                {
                    return ReleasableButtonState.Clicked;
                }

                return ReleasableButtonState.Released;
            }
        }

        public bool TargetAllies
        {
            get
            {
                return mLastControllerState.IsKeyDown(Keys.Q) && mCurrentControllerState.IsKeyUp(Keys.Q);
            }
        }

        public bool TargetEnemies
        {
            get
            {
                return mLastControllerState.IsKeyDown(Keys.E) && mCurrentControllerState.IsKeyUp(Keys.E);
            }
        }

        public float Sword
        {
            get
            {
                return mLastControllerState.IsKeyDown(Keys.LeftShift) &&
                        mCurrentControllerState.IsKeyUp(Keys.LeftShift) ? 1 : 0;
            }
        }

        public Vector2 Movement
        {
            get
            {
                Vector2 move = new Vector2();
                if(mLastControllerState.IsKeyDown(Keys.Right)){
                    move.X = 1;
                }
                else if(mLastControllerState.IsKeyDown(Keys.Left)){
                    move.X = -1;
                }

                if(mLastControllerState.IsKeyDown(Keys.Up)){
                    move.Y = -1;
                }
                else if(mLastControllerState.IsKeyDown(Keys.Down)){
                    move.Y = 1;
                }

                return move;
            }
        }
        #endregion
    }
}
