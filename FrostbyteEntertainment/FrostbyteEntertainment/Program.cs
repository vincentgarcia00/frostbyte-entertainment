using System;

namespace Frostbyte
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            /*This.Game = new Game();
            This.Game.Run();*/
            Controller c = new Controller(Microsoft.Xna.Framework.PlayerIndex.One);
            while (true)
            {
                c.Update();
                if (c.Earth == ReleasableButtonState.Clicked)
                {
                    Console.WriteLine("Earth!");
                }
                if (c.Water == ReleasableButtonState.Clicked)
                {
                    Console.WriteLine("Water!");
                }
                if (c.Fire == ReleasableButtonState.Clicked)
                {
                    Console.WriteLine("Fire!");
                }
                if (c.Lightning == ReleasableButtonState.Clicked)
                {
                    Console.WriteLine("Lightning!");
                }
                if (c.Movement.X > 0 || c.Movement.Y > 0)
                {
                    Console.WriteLine(String.Format("Movement: ({0}, {1})", c.Movement.X, c.Movement.Y));
                }
                if (c.Sword > 0)
                {
                    Console.WriteLine(String.Format("Sword: {0}", c.Sword));
                }
                if (c.TargetAllies)
                {
                    Console.WriteLine("Targeting next ally.");
                }
                if (c.TargetEnemies)
                {
                    Console.WriteLine("Targeting next enemy.");
                }
                if (c.CancelTargeting == ReleasableButtonState.Clicked)
                {
                    Console.WriteLine("Releasing target");
                }
            }
        }
    }
#endif
}

