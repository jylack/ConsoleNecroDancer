using NecroDancer;
using System;
using System.Diagnostics;
using System.Threading;


namespace ConsoleNecroDancer
{

       
    internal class Program
    {
        static public void GameStart()
        {
            LogoPrint();


        }



        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GameManager gameManager = new GameManager();
            BattleManager battleManager = new BattleManager();

            Stopwatch frameWatch = new Stopwatch();
            Stopwatch beatWatch = new Stopwatch();
            Stopwatch monsterWarch = new Stopwatch();           
            
            gameManager.Init();
            battleManager.Init();


            Console.CursorVisible = false;

            GameStart();
            

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
