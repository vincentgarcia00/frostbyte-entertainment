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

    class Controller
    {
        #region Constructors
        internal Controller(PlayerIndex ix)
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
        internal void Update()
        {
            mLastControllerState = mCurrentControllerState;
            mCurrentControllerState = GamePad.GetState(PlayerIndex.One);
        }

        internal GamePadState getRawState()
        {
            return mCurrentControllerState;
        }
        #endregion

        #region Properties
        private GamePadButtons LastButtons { get { return mLastControllerState.Buttons; } }
        private GamePadButtons CurrentButtons { get { return mCurrentControllerState.Buttons; } }

        internal bool IsConnected { get { return mCurrentControllerState.IsConnected; } }

        /// <summary>
        /// Determines whether or not the Earth element was selected on the controller
        /// </summary>
        internal ReleasableButtonState Earth
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

        /// <summary>
        /// Determines whether or not the Fire element was selected on the controller
        /// </summary>
        internal ReleasableButtonState Fire
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

        /// <summary>
        /// Determines whether or not the Water element was selected on the controller
        /// </summary>
        internal ReleasableButtonState Water
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

        /// <summary>
        /// Determines whether or not the Lightning element was selected by the controller.
        /// </summary>
        internal ReleasableButtonState Lightning
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

        /// <summary>
        /// Determines whether or not the button for cancelling targeting was pressed
        /// </summary>
        internal ReleasableButtonState CancelTargeting
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

        /// <summary>
        /// Determines whether or not the button for interacting with the environment was pressed.
        /// </summary>
        internal ReleasableButtonState Interact
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

        /// <summary>
        /// Provides a float representing how far down the sword trigger is pressed.
        /// </summary>
        internal float Sword
        {
            get
            {
                return mCurrentControllerState.Triggers.Right;
            }
        }

        /// <summary>
        /// Determines whether or not the button for targeting allies was pressed
        /// </summary>
        internal bool TargetAllies
        {
            get
            {
                return LastButtons.LeftShoulder == ButtonState.Pressed &&
                    CurrentButtons.LeftShoulder == ButtonState.Released;
            }
        }

        /// <summary>
        /// Determines whether or not the button for targeting enemies was pressed
        /// </summary>
        internal bool TargetEnemies
        {
            get
            {
                return LastButtons.RightShoulder == ButtonState.Pressed &&
                    CurrentButtons.RightShoulder == ButtonState.Released;
            }
        }

        /// <summary>
        /// Returns the value of the left joystick
        /// </summary>
        internal Vector2 Movement
        {
            get
            {
                return mCurrentControllerState.ThumbSticks.Left;
            }
        }

        #endregion
    }
}
