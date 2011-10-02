using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BodilyInfection.Engine;
namespace BodilyInfection.Levels
{
    class StageClear
    {
        static readonly TimeSpan RequiredWaitTime = new TimeSpan(0, 0, 0, 0, 0);
        static TimeSpan LevelInitTime = TimeSpan.MinValue;
        private static bool levelCompleted = false;

        internal static void Load()
        {
            LevelInitTime = TimeSpan.MinValue;
            levelCompleted = false;

            Level l = This.Game.CurrentLevel != This.Game.NextLevel && This.Game.NextLevel != null ? This.Game.NextLevel : This.Game.CurrentLevel;
            l.AddAnimation(new BackgroundAnimation("stageclear.anim"));

            l.Background = new Background("stageclear", "stageclear.anim");

            /** load music */
            This.Game.AudioManager.AddBackgroundMusic("win");
            This.Game.AudioManager.PlayBackgroundMusic("win");
        }

        internal static void Update()
        {
            GameTime gameTime = This.gameTime;
            if (LevelInitTime == TimeSpan.MinValue)
            {
                LevelInitTime = gameTime.TotalGameTime;
            }
            Level l = This.Game.CurrentLevel;

            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            if (currentState.IsConnected)
            {
                if (currentState.Buttons.Start == ButtonState.Pressed)
                {
                    // Go to next
                    // Make awesome sound
                    levelCompleted = true;
                }
            }
            else /* Move with arrow keys */
            {
                KeyboardState keys = Keyboard.GetState();
                if (keys.IsKeyDown(Keys.Enter))
                {
                    // Go to next
                    // Make awesome sound
                    levelCompleted = true;
                }
            }
        }

        internal static bool CompletionCondition()
        {
            return levelCompleted;
        }

        internal static void Unload()
        {
            This.Game.SetCurrentLevel(BodilyInfectionLevel.LevelProgression[BodilyInfectionLevel.NextLevel()]);
        }
    }
}
