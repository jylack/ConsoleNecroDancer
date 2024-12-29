﻿using NecroDancer;
using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleNecroDancer
{


    internal class Program
    {




        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GameManager gameManager = new GameManager();
            BattleManager battleManager = new BattleManager();

            Stopwatch frameWatch = new Stopwatch();
            Stopwatch hartWarch = new Stopwatch();

            gameManager.Init();
            battleManager.Init();


            Console.CursorVisible = false;

            LogoPrint();
            Thread.Sleep(500);

            frameWatch.Start();



            while (true)
            {

                    gameManager.Update();

                    battleManager.SetAction(gameManager.isAction);

                    battleManager.Update();

                    GameManager.ConSoleClear();

                    frameWatch.Restart();

                

                battleManager.Render();
                gameManager.Render();



            }

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
