using System;
using System.Text;
using System.Threading;

namespace Life.ConsoleUI
{
    class Program
    {
        private const int Width = 40;
        private const int Height = 20;

        static void Main()
        {
            var position = GenerateRandomPosition();
            var game = new Game(position);
            for(;;)
            {
                Console.Clear();
                Console.Write(game.ToString());
                Thread.Sleep(500);
                game.NextGeneration();
            }
        }

        private static string GenerateRandomPosition()
        {
            var rng = new Random();
            var sb = new StringBuilder();
            for (var row = 0; row < Height; row++)
            {
                sb.Append("\n");
                for (var col = 0; col < Width; col++)
                {
                    sb.Append(rng.NextDouble() < 0.25f ? '*' : '.');
                }
            }
            return sb.ToString().Substring(1);
        }
    }
}
