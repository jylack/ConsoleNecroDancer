using NecroDancer;
using System;
using System.Diagnostics;
using System.Threading;


namespace ConsoleNecroDancer
{


    internal class Program
    {
       

        public static bool isGame = true;


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //아에 게임 종료버튼을 처음에 눌렀으면 들어오지도 않게 제어.
            if (GameStart() == false)
            {
                Environment.Exit(0);
            }


            GameManager gameManager = new GameManager();
            BattleManager battleManager = new BattleManager();

            //프레임 시간조절용
            Stopwatch frameWatch = new Stopwatch();
            //비트 시간조절용
            Stopwatch beatWatch = new Stopwatch();
            //몬스터 시간조절용
            Stopwatch monsterWarch = new Stopwatch();

            //초기화 부분.
            gameManager.Init();
            battleManager.Init();

            Console.CursorVisible = false;

            frameWatch.Start();
            beatWatch.Start();
            monsterWarch.Start();

            //각 매니저당 쓸 타이머를 가져가는중
            gameManager.SetTimer(beatWatch);
            battleManager.SetTimer(monsterWarch);



            while (isGame)
            {
                //프레임은 0.1초당 작동할 예정.
                if (frameWatch.ElapsedMilliseconds > 100)
                {
                    gameManager.Update();
                    //플레이어가 비트에 맞게 움직이는 키를 눌렀는지 체크
                    battleManager.SetAction(gameManager.isAction);

                    battleManager.Update();
                    //렌더를 그려주러 가기전에 한번 싹지우는 메소드
                    gameManager.ConSoleClear();

                    frameWatch.Restart();
                }

                battleManager.Render();
                gameManager.Render();

                

            }
            //사용을 다한 stopWarch를 정지.
            monsterWarch.Stop();
            beatWatch.Stop();
            frameWatch.Stop();
            //게임이 끝났을때 호출해줄 메소드
            gameManager.End();

            Thread.Sleep(2000);
        }

        //게임시작시 작동할 메소드
        static public bool GameStart()
        {
            //게임 시작여부 물어불 bool문
            bool isGamePlay = false;

            //로고 string이 너무 커서 메소드로 따로빼둠.
            LogoPrint();

            //임시로쓸 키값.
            ConsoleKeyInfo inputKey;
            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth / 3, 5 + 16);

            Console.WriteLine("\t게임을 시작 하시겠습니까?");
            Console.SetCursorPosition(Console.WindowWidth / 3 + 5, 5 + 17);

            Console.WriteLine("1.게임시작\t\t 2.게임 종료");

            //입력 올바른값 체킹용
            bool isGame = true;

            while (isGame)
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        isGamePlay = true;
                        isGame = false;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        isGamePlay = false;
                        isGame = false;
                        break;
                }
            }

            return isGamePlay;
        }


        static void LogoPrint()
        {
            string[] strings = new string[15];

            strings[0]  = "\t╔────────────────────────────────────────────────────╗\r";
            strings[1]  = "\t│ ███╗   ██╗███████╗ ██████╗██████╗  ██████╗        │\r";
            strings[2]  = "\t│ ████╗  ██║██╔════╝██╔════╝██╔══██╗██╔═══██╗       │\r";
            strings[3]  = "\t│ ██╔██╗ ██║█████╗  ██║     ██████╔╝██║   ██║       │\r";
            strings[4]  = "\t│ ██║╚██╗██║██╔══╝  ██║     ██╔══██╗██║   ██║       │\r";
            strings[5]  = "\t│ ██║ ╚████║███████╗╚██████╗██║  ██║╚██████╔╝       │\r";
            strings[6]  = "\t│ ╚═╝  ╚═══╝╚══════╝ ╚═════╝╚═╝  ╚═╝ ╚═════╝        │\r";
            strings[7]  = "\t│                                                   │\r";
            strings[8]  = "\t│ ██████╗  █████╗ ███╗   ██╗ ██████╗███████╗██████╗ │\r";
            strings[9]  = "\t│ ██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔══██╗│\r";
            strings[10] = "\t│ ██║  ██║███████║██╔██╗ ██║██║     █████╗  ██████╔╝│\r";
            strings[11] = "\t│ ██║  ██║██╔══██║██║╚██╗██║██║     ██╔══╝  ██╔══██╗│\r";
            strings[12] = "\t│ ██████╔╝██║  ██║██║ ╚████║╚██████╗███████╗██║  ██║│\r";
            strings[13] = "\t│ ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝╚══════╝╚═╝  ╚═╝│\r";
            strings[14] = "\t╚────────────────────────────────────────────────────╝";


            for (int i = 0; i < strings.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, 5+i);
                Console.WriteLine(strings[i]);

            }

        }
    }
}
