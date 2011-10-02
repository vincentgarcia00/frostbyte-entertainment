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

        internal ReleasableButtonState Earth
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > 0.6)
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

        internal ReleasableButtonState Fire
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > 0.6)
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

        internal ReleasableButtonState Water
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > 0.6)
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

        internal ReleasableButtonState Lightning
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left > 0.6)
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

        internal ReleasableButtonState CancelTargeting
        {
            get
            {
                // Only trigger spells when Left Trigger is pressed
                if (mCurrentControllerState.Triggers.Left <= 0.6)
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

        internal float Sword
        {
            get
            {
                return mCurrentControllerState.Triggers.Right;
            }
        }

        internal bool TargetAllies
        {
            get
            {
                return LastButtons.LeftShoulder == ButtonState.Pressed &&
                    CurrentButtons.LeftShoulder == ButtonState.Released;
            }
        }

        internal bool TargetEnemies
        {
            get
            {
                return LastButtons.RightShoulder == ButtonState.Pressed &&
                    CurrentButtons.RightShoulder == ButtonState.Released;
            }
        }

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
