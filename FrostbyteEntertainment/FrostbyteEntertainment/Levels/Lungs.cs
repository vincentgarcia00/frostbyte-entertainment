using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BodilyInfection.Engine;

namespace BodilyInfection.Levels
{
    class Lungs
    {
        #region Timer Variables
        internal static TimeSpan SpawnWaitTime = new TimeSpan(0, 0, 0, 4, 0);
        internal static TimeSpan PreviousSpawn = new TimeSpan(0, 0, 0, 0, 0);
        #endregion Timer Variables

        private static int EnemiesDefeatedWinCondition = 1500;

        internal static void Load()
        {
            BodilyInfectionLevel l = (This.Game.CurrentLevel != This.Game.NextLevel && This.Game.NextLevel != null ? This.Game.NextLevel : This.Game.CurrentLevel) as BodilyInfectionLevel;

            l.EnemiesDefeated = 0;
            l.waveNumber = 4;

            /// load background
            l.Background = new Background("lungs", "lungs.anim");

            /** load animations */
            l.AddAnimation(new Animation("rbc.anim"));
            l.AddAnimation(new Animation("virusPulse.anim"));
            l.AddAnimation(new Animation("infected.anim"));
            l.AddAnimation(new Animation("antibody.anim"));
            l.AddAnimation(new Animation("shield.anim"));
            l.AddAnimation(new Animation("ship.anim"));
            l.AddAnimation(new Animation("cannon.anim"));
            l.AddAnimation(new Animation("xplosion17.anim"));
            l.AddAnimation(new Animation("BlueExplosion2.anim"));
            l.AddAnimation(new Animation("vulnerable.anim"));
            l.AddAnimation(new Animation("RedExplosion2.anim"));
            
            /** load sprites */

            // Spawn initial RedBloodCells and Viruses
            LevelFunctions.Spawn(delegate()
            {
                Actor rbcActor = new Actor(l.GetAnimation("rbc.anim"));
                rbcActor.Animations.Add(l.GetAnimation("infected.anim"));
                rbcActor.Animations.Add(l.GetAnimation("vulnerable.anim"));
                rbcActor.Animations.Add(l.GetAnimation("RedExplosion2.anim"));
                Sprite rbc = new RedBloodCell("rbc", rbcActor);
                return rbc;
            }, 5);
            LevelFunctions.Spawn(delegate()
            {
                Actor virusActor = new Actor(l.GetAnimation("virusPulse.anim"));
                virusActor.Animations.Add(l.GetAnimation("BlueExplosion2.anim"));
                return new Virus("virus", virusActor);
            }, 0);


            // Load ship
            Actor shipActor = new Actor(l.GetAnimation("ship.anim"));
            shipActor.Animations.Add(l.GetAnimation("xplosion17.anim"));
            Sprite ship = new Ship("ship", shipActor);

            l.PlayerSpawnPoint = new Vector2(1000, 400);
            ship.Pos = l.PlayerSpawnPoint;

            LevelFunctions.MakeHUD();

            /** load music */
            var audioMan = This.Game.AudioManager;

            //ship spawn
            audioMan.AddSoundEffect("ship_spawn");
            //ship explode
            audioMan.AddSoundEffect("ship_explosion");
            //gun
            audioMan.AddSoundEffect("gun1");
            //rbc die
            audioMan.AddSoundEffect("rbc_die");
            //rbc infected
            audioMan.AddSoundEffect("rbc_infect");
            //virus explode
            audioMan.AddSoundEffect("virus_explode");

            //bg
            audioMan.AddBackgroundMusic("heart_bg");
            audioMan.PlayBackgroundMusic("heart_bg");
            audioMan.BackgroundMusicVolume(.150f);
        }

        internal static void Update()
        {
            GameTime gameTime = This.gameTime;
            if (gameTime.TotalGameTime >= SpawnWaitTime + PreviousSpawn)
            {
                int waveNumber = (This.Game.CurrentLevel as BodilyInfectionLevel).waveNumber++;

                LevelFunctions.Spawn(delegate()
                {
                    Actor virusActor = new Actor(This.Game.CurrentLevel.GetAnimation("virusPulse.anim"));
                    virusActor.Animations.Add(This.Game.CurrentLevel.GetAnimation("BlueExplosion2.anim"));
                    return new Virus("virus", virusActor);
                }, (int)(.75*(Math.Sin((double)waveNumber) + waveNumber)));

                PreviousSpawn = gameTime.TotalGameTime;

                if (This.Game.CurrentLevel.mSprites.FindAll(delegate(WorldObject obj) { return obj.Name == "rbc"; }).Count < 8)
                {
                    LevelFunctions.Spawn(delegate()
                    {
                        Actor rbcActor = new Actor(This.Game.CurrentLevel.GetAnimation("rbc.anim"));
                        rbcActor.Animations.Add(This.Game.CurrentLevel.GetAnimation("infected.anim"));
                        rbcActor.Animations.Add(This.Game.CurrentLevel.GetAnimation("vulnerable.anim"));
                        rbcActor.Animations.Add(This.Game.CurrentLevel.GetAnimation("RedExplosion2.anim"));
                        Sprite rbc = new RedBloodCell("rbc", rbcActor);
                        return rbc;
                    }, waveNumber % 2);
                }
            }
        }

        internal static bool CompletionCondition()
        {
            return (This.Game.CurrentLevel as BodilyInfectionLevel).EnemiesDefeated >= EnemiesDefeatedWinCondition;
        }
    }
}
