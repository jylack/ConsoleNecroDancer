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

            int beetSpeed = 1;

            int combo = 0;

            int gameSpeed = 600; // 0.6초당 박자 움직이기

            bool isAction = false;

            sw.Start();

            bitWatch.Start();

            while (true)
            {

                if (Console.KeyAvailable)
                {
                    input = Console.ReadKey(true);

                    switch (input.Key)
                    {
                        case ConsoleKey.Spacebar:
                            //비트가 있고 하트가 맞았을때
                            if (hart.IsBits() && hart.IsCheckHit())
                            {
                                combo++;
                                hart.RemoveBits();
                                isAction = true;


                            }
                            //비트가 있고, 하트가 맞지 않았는데 플레이어가 눌렀을때
                            else if (hart.IsBits() && hart.IsCheckHit() == false)
                            {
                                combo = 0;
                                hart.RemoveBits();
                                isAction = false;
                            }
                            break;

                            //방향 이동.
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            if (isAction)
                            {

                            }
                   
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            if (isAction)
                            {

                            }

                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            if (isAction)
                            {

                            }

                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            if (isAction)
                            {

                            }

                            break;


                    }


                }

                //게임 스피드만큼 시간이 지나면 비트를 추가한다.
                if (sw.ElapsedMilliseconds > gameSpeed)
                {

                    hart.AddBit(beetSpeed);//비트가 움직일속도 지정                    

                    sw.Restart();
                }

                //0.1초당 화면 재생 비트이동도 0.1초당 함.
                if (bitWatch.ElapsedMilliseconds > 100)
                {
                    Console.Clear();

                    if (hart.IsBits())
                    {
                        hart.BitMove();

                        //여기는 비트가 하트에 맞았고, 플레이어가 누르기전에 맞았을때
                        if (hart.IsNonCheckHit())
                        {
                            hart.RemoveBits();
                            combo = 0;
                            
                        }
                    }

                    bitWatch.Restart();

                    hart.Print();
                    Console.WriteLine();
                    Console.WriteLine(combo);

                }


            }
            bitWatch.Stop();

            sw.Stop();
        }
    }
}
