using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Frostbyte
{
    internal static class This
    {
        internal static Game Game;
        internal static GameTime gameTime;

        internal static Cheats Cheats = new Cheats();
    }

    class Cheats
    {
        public interface ICheat{
            bool Enabled { get; }
            void Enable();
            void Disable();
            void Toggle();
        }

        internal class DefaultCheat : ICheat
        {
            bool _enabled;

            public bool Enabled
            {
                get
                {
                    return _enabled;
                }
            }

            public void Enable()
            {
                _enabled = true;
            }

            public void Disable()
            {
                _enabled = false;
            }

            public void Toggle()
            {
                if (Enabled)
                {
                    Disable();
                }
                else
                {
                    Enable();
                }
            }
        }

        internal DefaultCheat SpawnEnemies = new DefaultCheat();
    }
}
