using System;
using System.Diagnostics;

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
        start,
        Up,
        Down,
        Left,
        Right,
        end
    }

    public class GameManager : IManagerInterFace
    {

        static int height;
        static int width;

        Hart hart;
        Stopwatch sw = new Stopwatch();
        Stopwatch beatWatch = new Stopwatch();

        public static ConsoleKeyInfo input;


        public static bool isGameStart = true;

        int beatSpeed = 1;

        int combo = 0;

        int gameSpeed = 600; // 0.6초당 박자 움직이기

        public bool isAction = false;

        static string strClear = "";

        public void Init()
        {

            height = 20;
            width = Console.WindowWidth / 2 - 3;
            hart = new Hart(new Point(width, height), (int)Difficulty.Normal);

            for (int i = 0; i < Console.WindowWidth-1; i++)
            {
                strClear += " ";
            }

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

            isGameStart = false;

        }

        public void Update()
        {

            input = new ConsoleKeyInfo();


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
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        KeyInputAction();

                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        KeyInputAction();

                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        KeyInputAction();

                        break;


                }

            }

            

            hart.Update();
        }

        public void Render()
        {
            hart.Render();

        }

        public static void ConSoleClear()
        {
            
            for(int i = height; i <= height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(strClear);
            }

        }

    }
}
