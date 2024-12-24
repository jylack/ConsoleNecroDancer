using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    enum Difficulty
    {
        Easy = 5,
        Normal = 3,
        Hard = 1
    }

    public enum Fword
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class GameManager : IManagerInterFace
    {

        int height ;
        int width;

        Hart hart;

        Stopwatch sw = new Stopwatch();
        Stopwatch beatWatch = new Stopwatch();


        public static ConsoleKeyInfo input;


        public static bool isGameStart = true;

        int beatSpeed = 1;

        int combo = 0;

        int gameSpeed = 600; // 0.6초당 박자 움직이기

        bool isAction = false;


        public void Init()
        {

            height = Console.WindowHeight;
            width = Console.WindowWidth / 2 - 3;
            hart = new Hart(new Point(width, 20), (int)Difficulty.Normal);

        }

        public void KeyInputAction()
        {
            //비트가 있고 하트가 맞았을때
            if (hart.Isbeats() && hart.IsCheckHit())
            {
                combo++;
                hart.Removebeats();
                isAction = true;
            }
            //비트가 있고, 하트가 맞지 않았는데 플레이어가 눌렀을때
            else if (hart.Isbeats() && hart.IsCheckHit() == false)
            {
                combo = 0;
                hart.Removebeats();
                isAction = false;
            }
        }

        public void Update()
        {

            input = new ConsoleKeyInfo();

            sw.Start();

            beatWatch.Start();

            while (isGameStart)
            {

                if (Console.KeyAvailable)
                {
                    input = Console.ReadKey(true);

                    switch (input.Key)
                    {
                        case ConsoleKey.Spacebar:
                            
                            break;

                        //방향 이동.
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            KeyInputAction();
                            isGameStart = false;
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            isGameStart = false;
                            KeyInputAction();

                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            isGameStart = false;
                            KeyInputAction();

                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            isGameStart = false;
                            KeyInputAction();

                            break;


                    }

                }

                if(isAction) 
                {

                }

                //게임 스피드만큼 시간이 지나면 비트를 추가한다.
                if (sw.ElapsedMilliseconds > gameSpeed)
                {

                    hart.Addbeat(beatSpeed,hart.GetPos().Y);//비트가 움직일속도 지정                    

                    sw.Restart();
                }

                Render();


            }

            beatWatch.Stop();

            sw.Stop();
        }

        public void Render()
        {
            //0.1초당 화면 재생 비트이동도 0.1초당 함.
            if (beatWatch.ElapsedMilliseconds > 100)
            {
                //Console.Clear();
                ConSoleClear();

                if (hart.Isbeats())
                {
                    hart.beatMove();

                    //여기는 비트가 하트에 맞았고, 플레이어가 누르기전에 맞았을때
                    if (hart.IsNonCheckHit())
                    {
                        hart.Removebeats();
                        combo = 0;

                    }
                }

                beatWatch.Restart();

                hart.Print();
                Console.WriteLine();
                Console.WriteLine(combo);

            }
        }

        public void ConSoleClear()
        {
            Console.SetCursorPosition(0, hart.GetPos().Y);
            Console.WriteLine("                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            \n");
        }

    }
}
