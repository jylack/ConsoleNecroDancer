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

    //방향
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

        public static int height;
        public static int width;

        Hart hart;

        Stopwatch beatWatch = new Stopwatch();

        public static ConsoleKeyInfo input;


        public static bool isGameStart = true;

        public bool isAction = false;

        static string strClear = "";


        public void SetTimer(Stopwatch stopwatch)
        {
            beatWatch = stopwatch;
        }


        public void Init()
        {

            height = 20;
            width = Console.WindowWidth / 2 - 3;
            hart = new Hart(new Point(width, height), (int)Difficulty.Normal);

            for (int i = 0; i < Console.WindowWidth - 1; i++)
            {
                strClear += " ";
            }

        }

        public void KeyInputAction()
        {
            isGameStart = false;

            //비트가 있고, 하트가 맞지 않았는데 플레이어가 눌렀을때
            if (hart.Isbeats() && hart.IsCheckHit() == false)
            {
                hart.combo = 0;
                hart.Removebeats();
                isAction = false;
                return;
            }


            //비트가 있고 하트가 맞았을때
            if (hart.Isbeats() && hart.IsCheckHit())
            {
                hart.combo++;
                hart.Removebeats();
                isAction = true;
            }



        }

        public void Update()
        {

            input = new ConsoleKeyInfo();

            //키를 입력받았을때만 들어오라는 의미.
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);

                switch (input.Key)
                {
                    //디버깅용 모든멥 밝히는 치트키
                    case ConsoleKey.Spacebar:
                        TileManager.isViewAll = !TileManager.isViewAll;
                        break;

                    //방향 이동.
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
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

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        KeyInputAction();

                        break;
                }
                //사실 KeyInputAction()은 맨아래 case안에넣고,
                //나머진 break를 지워주면 전부 해결되는거긴한데...
                //가독성이 안좋아서 수정
            }

            //비트는 0.6초당 한번씩 생성된다.
            if (beatWatch.ElapsedMilliseconds > 600)
            {
                hart.Addbeat(hart.GetPos().Y);
                beatWatch.Restart();

            }

            hart.Update();

        }

        public void Render()
        {

            hart.Render();

        }

        //Console.Clear()대신 쓸 메소드
        //원래는 static이였으나 어차피 화면변화는 한번만해주면되길래 수정.
        public void ConSoleClear()
        {

            for (int i = 0; i <= height + 5; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(strClear);
            }

        }

        public void End()
        {
            if (Player.Life <= 0)
            {
                YouDiE();
            }
            else
            {
                YouWin();
            }
        }


        public void YouDiE()
        {
            string[] strings = new string[6];


            strings[0] = "██╗   ██╗ ██████╗ ██╗   ██╗    ██████╗ ██╗███████╗";
            strings[1] = "╚██╗ ██╔╝██╔═══██╗██║   ██║    ██╔══██╗██║██╔════╝";
            strings[2] = " ╚████╔╝ ██║   ██║██║   ██║    ██║  ██║██║█████╗  ";
            strings[3] = "  ╚██╔╝  ██║   ██║██║   ██║    ██║  ██║██║██╔══╝  ";
            strings[4] = "   ██║   ╚██████╔╝╚██████╔╝    ██████╔╝██║███████╗";
            strings[5] = "   ╚═╝    ╚═════╝  ╚═════╝     ╚═════╝ ╚═╝╚══════╝";

            for (int i = 0; i < strings.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, 5 + i);
                Console.WriteLine(strings[i]);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        public void YouWin()
        {
            string[] strings = new string[8];


            strings[0] = "########################################################";
            strings[1] = "#██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗██╗███╗   ██╗#";
            strings[2] = "#╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██║████╗  ██║#";
            strings[3] = "# ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║██╔██╗ ██║#";
            strings[4] = "#  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║██║╚██╗██║#";
            strings[5] = "#   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝██║██║ ╚████║#";
            strings[6] = "#   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝#";
            strings[7] = "########################################################";


            for (int i = 0; i < strings.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, 5 + i);
                Console.WriteLine(strings[i]);

            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
