using NecroDancer;
using System;
using System.Diagnostics;

namespace ConsoleNecroDancer
{
    enum Difficulty
    {
        Easy = 5,
        Normal = 3,
        Hard = 1
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth / 2 - 3;

            Hart hart = new Hart(new Point(width, 0), (int)Difficulty.Normal);

            Stopwatch sw = new Stopwatch();
            Stopwatch bitWatch = new Stopwatch();

            Console.CursorVisible = false;

            ConsoleKeyInfo input;

            input = new ConsoleKeyInfo();



            int gameSpeed = 600; // 0.6초당 박자 움직이기

            sw.Start();

            bitWatch.Start();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    input = Console.ReadKey(true);
                    if (input.Key == ConsoleKey.Spacebar)
                    {
                        if (hart.IsBits() && hart.IsHit())
                        {
                            hart.RemoveBits();
                        }
                    }

                }

                if (sw.ElapsedMilliseconds > gameSpeed)
                {

                    hart.AddBit(new Bit(new Point(0, 0), 1, true));
                    hart.AddBit(new Bit(new Point(Console.WindowWidth, 0), 1, false));


                    sw.Restart();
                }

                if (bitWatch.ElapsedMilliseconds > 100)
                {
                    Console.Clear();

                    if (hart.IsBits())
                    {
                        hart.BitMove();

                        if (hart.IsHit())
                        {
                            hart.RemoveBits();
                        }
                    }

                    bitWatch.Restart();

                    hart.Print();

                }


            }
            bitWatch.Stop();

            sw.Stop();
        }
    }
}
