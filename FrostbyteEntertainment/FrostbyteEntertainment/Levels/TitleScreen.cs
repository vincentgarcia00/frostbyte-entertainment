using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BodilyInfection.Engine;

namespace BodilyInfection.Levels
{
    internal static class TitleScreen
    {
        static readonly TimeSpan RequiredWaitTime = new TimeSpan(0, 0, 0, 0, 0);
        static TimeSpan LevelInitTime = TimeSpan.MinValue;
        private static bool levelCompleted = false;

        internal static void Load()
        {
            LevelInitTime = TimeSpan.MinValue;
            levelCompleted = false;

            Level l = This.Game.CurrentLevel != This.Game.NextLevel && This.Game.NextLevel != null ? This.Game.NextLevel : This.Game.CurrentLevel;
            l.AddAnimation(new BackgroundAnimation("title.anim"));

            l.Background = new Background("title", "title.anim");

            /** load music */
            This.Game.AudioManager.AddBackgroundMusic("title");
            This.Game.AudioManager.PlayBackgroundMusic("title");

            GameData.Score = 0;
            GameData.NumberOfLives = GameData.DefaultNumberOfLives;
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
                if (This.Game.mLastPadState.Buttons.Start == ButtonState.Released && currentState.Buttons.Start == ButtonState.Pressed )
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
            string levelname = BodilyInfectionLevel.LevelProgression[0];
            This.Game.LoadLevel(levelname);

            /// \todo display some stuff here about loading probably put it in a shared space


            This.Game.SetCurrentLevel(levelname);
        }
    }
}
