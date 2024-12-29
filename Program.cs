using NecroDancer;
using System;
using System.Diagnostics;
using System.Threading;


namespace ConsoleNecroDancer
{

       
    internal class Program
    {
        static public bool GameStart()
        {

            bool isGamePlay = false;

            LogoPrint();
            ConsoleKeyInfo inputKey;
            Console.WriteLine();
            Console.WriteLine("\t\t\t게임을 시작 하시겠습니까?");
            Console.WriteLine("\t\t\t1.게임시작\t 2.게임 종료");

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



        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            if (GameStart() == false)
            {
                Environment.Exit(0);
            }



            GameManager gameManager = new GameManager();
            BattleManager battleManager = new BattleManager();

            Stopwatch frameWatch = new Stopwatch();
            Stopwatch beatWatch = new Stopwatch();
            Stopwatch monsterWarch = new Stopwatch();           
            
            gameManager.Init();
            battleManager.Init();


            Console.CursorVisible = false;

            

            frameWatch.Start();
            beatWatch.Start();
            monsterWarch.Start();

            gameManager.SetTimer(beatWatch);
            battleManager.SetTimer(monsterWarch);


            while (Player.Life > 0)
            {
                if (frameWatch.ElapsedMilliseconds > 100)
                {
                    gameManager.Update();

                    battleManager.SetAction(gameManager.isAction);

                    battleManager.Update();

                    GameManager.ConSoleClear();

                    frameWatch.Restart();
                }

                battleManager.Render();
                gameManager.Render();



            }

            monsterWarch.Stop();
            beatWatch.Stop();
            frameWatch.Stop();

            gameManager.End();


        }

        static void LogoPrint()
        {
            string str = "\t╔────────────────────────────────────────────────────╗\r\n" +
                            "\t│ ███╗   ██╗███████╗ ██████╗██████╗  ██████╗        │\r\n" +
                            "\t│ ████╗  ██║██╔════╝██╔════╝██╔══██╗██╔═══██╗       │\r\n" +
                            "\t│ ██╔██╗ ██║█████╗  ██║     ██████╔╝██║   ██║       │\r\n" +
                            "\t│ ██║╚██╗██║██╔══╝  ██║     ██╔══██╗██║   ██║       │\r\n" +
                            "\t│ ██║ ╚████║███████╗╚██████╗██║  ██║╚██████╔╝       │\r\n" +
                            "\t│ ╚═╝  ╚═══╝╚══════╝ ╚═════╝╚═╝  ╚═╝ ╚═════╝        │\r\n" +
                            "\t│                                                   │\r\n" +
                            "\t│ ██████╗  █████╗ ███╗   ██╗ ██████╗███████╗██████╗ │\r\n" +
                            "\t│ ██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔══██╗│\r\n" +
                            "\t│ ██║  ██║███████║██╔██╗ ██║██║     █████╗  ██████╔╝│\r\n" +
                            "\t│ ██║  ██║██╔══██║██║╚██╗██║██║     ██╔══╝  ██╔══██╗│\r\n" +
                            "\t│ ██████╔╝██║  ██║██║ ╚████║╚██████╗███████╗██║  ██║│\r\n" +
                            "\t│ ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝╚══════╝╚═╝  ╚═╝│\r\n" +
                            "\t╚────────────────────────────────────────────────────╝";

            Console.Write(str);

        }
    }
}
