using NecroDancer;
using System;
using System.Diagnostics;

namespace ConsoleNecroDancer
{
   

    internal class Program
    {

       


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string str = "╔──────────────────────────────────────────────────╗\r\n" +
                         "│███╗   ██╗███████╗ ██████╗██████╗  ██████╗        │\r\n" +
                         "│████╗  ██║██╔════╝██╔════╝██╔══██╗██╔═══██╗       │\r\n" +
                         "│██╔██╗ ██║█████╗  ██║     ██████╔╝██║   ██║       │\r\n" +
                         "│██║╚██╗██║██╔══╝  ██║     ██╔══██╗██║   ██║       │\r\n" +
                         "│██║ ╚████║███████╗╚██████╗██║  ██║╚██████╔╝       │\r\n" +
                         "│╚═╝  ╚═══╝╚══════╝ ╚═════╝╚═╝  ╚═╝ ╚═════╝        │\r\n" +
                         "│                                                  │\r\n" +
                         "│██████╗  █████╗ ███╗   ██╗ ██████╗███████╗██████╗ │\r\n" +
                         "│██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔══██╗│\r\n" +
                         "│██║  ██║███████║██╔██╗ ██║██║     █████╗  ██████╔╝│\r\n" +
                         "│██║  ██║██╔══██║██║╚██╗██║██║     ██╔══╝  ██╔══██╗│\r\n" +
                         "│██████╔╝██║  ██║██║ ╚████║╚██████╗███████╗██║  ██║│\r\n" +
                         "│╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝╚══════╝╚═╝  ╚═╝│\r\n" +
                         "╚──────────────────────────────────────────────────╝";

            GameManager gameManager = new GameManager();
            BattleManager battleManager = new BattleManager();

            battleManager.Init();
            gameManager.Init();
            Console.CursorVisible = false;

            Console.Write(str);

            while (true)
            {
                gameManager.Update();
                battleManager.Update();
                battleManager.Render();
            }
        }

        
    }
}
