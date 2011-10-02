using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frostbyte.Engine;

namespace Frostbyte
{
    internal static class LevelFunctions
    {
        internal delegate Sprite EnemyFactory();

        internal static readonly Random rand = new Random();

        internal static void DoNothing(GameTime gameTime) { }

        /// <summary>
        /// A behavior that sends the player to the Stage Clear screen once the level is over.
        /// </summary>
        internal static void ToStageClear()
        {
            This.Game.SetCurrentLevel("stageclear");
        }

        internal static void MakeHUD()
        {
            Text scoreText = new Text("scoreText", "Text", "Score:");
            scoreText.Pos = new Vector2(50, This.Game.GraphicsDevice.Viewport.Y);
            scoreText.DisplayColor = Color.AliceBlue;
            scoreText.Static = true;
            Text score = new Text("score", "Text", GameData.Score.ToString());
            score.Pos = new Vector2(scoreText.GetAnimation().Width + 50, This.Game.GraphicsDevice.Viewport.Y);
            score.DisplayColor = Color.AliceBlue;
            score.Static = true;
            score.UpdateBehavior = delegate()
            {
                score.Content = GameData.Score.ToString();
            };

            Text livesText = new Text("livesText", "Text", "Lives Remaining:");
            livesText.Pos = new Vector2(This.Game.GraphicsDevice.Viewport.X + This.Game.GraphicsDevice.Viewport.Width - livesText.GetAnimation().Width - 50,
                                        This.Game.GraphicsDevice.Viewport.Y);
            livesText.DisplayColor = Color.Red;
            livesText.Static = true;
            Text lives = new Text("lives", "Text", GameData.NumberOfLives.ToString());
            lives.Pos = new Vector2(This.Game.GraphicsDevice.Viewport.X + This.Game.GraphicsDevice.Viewport.Width - 50,
                                    This.Game.GraphicsDevice.Viewport.Y);
            lives.DisplayColor = Color.Red;
            lives.Static = true;
            lives.UpdateBehavior = delegate()
            {
                lives.Content = GameData.NumberOfLives.ToString();
            };
        }

        internal static void GoToGameOver()
        {
            Condition oldWin = This.Game.CurrentLevel.WinCondition;
            Behavior oldEnd = This.Game.CurrentLevel.EndBehavior;
            This.Game.CurrentLevel.WinCondition = delegate { return true; };
            This.Game.CurrentLevel.EndBehavior = delegate
            {
                // Replace the old win condition
                This.Game.CurrentLevel.WinCondition = oldWin;
                This.Game.CurrentLevel.EndBehavior = oldEnd;
                This.Game.SetCurrentLevel("GameOver");
            };
            This.Game.AudioManager.Stop();
        }

        /// <summary>
        /// Spawns Enemies created by the EnemyFactory at random locations on the screen
        /// </summary>
        /// <param name="constructEnemy">Function to define an enemy</param>
        /// <param name="numEnemies">Number of enemies</param>
        internal static void Spawn(EnemyFactory constructEnemy, int numEnemies)
        {
#if NOCHEATS
#else
            if (!This.Cheats)
            {
#endif
                FrostbyteLevel l = (This.Game.CurrentLevel != This.Game.NextLevel && This.Game.NextLevel != null ? This.Game.NextLevel : This.Game.CurrentLevel) as FrostbyteLevel;
                for (int i = 0; i < numEnemies; i++)
                {
                    Sprite virus = constructEnemy();
                    virus.Pos = l.Camera.Pos +
                        new Vector2(rand.Next(-500, This.Game.GraphicsDevice.Viewport.Width+500),
                            rand.Next(-500, This.Game.GraphicsDevice.Viewport.Height+500));
                }
#if NOCHEATS
#else
            }
#endif
        }

        /// <summary>
        /// Spawns Enemies created by the EnemyFactory centered within a radius 
        /// proportional to the number of enemies around a specified position
        /// </summary>
        /// <param name="constructEnemy"></param>
        /// <param name="numEnemies"></param>
        /// <param name="position"></param>
        internal static void Spawn(EnemyFactory constructEnemy, int numEnemies, Vector2 position)
        {
#if NOCHEATS
#else
            if (!This.Cheats)
            {
#endif
                double radius = 160f;
                double angleInc = (1.5 * Math.PI) / numEnemies;
                double startAngle = Math.PI * 2 * rand.NextDouble();
                for (int i = 0; i < numEnemies; i++)
                {
                    Sprite virus = constructEnemy();
                    virus.Pos = position + new Vector2((float)(Math.Cos(angleInc * i + startAngle) * radius), (float)(Math.Sin(angleInc * i + startAngle) * radius));
                }
#if NOCHEATS
#else
            }
#endif
        }
    }
}
