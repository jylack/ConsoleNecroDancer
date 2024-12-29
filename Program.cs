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
        
            GameManager gameManager = new GameManager();
            BattleManager battleManager = new BattleManager();
            
            gameManager.Init();
            battleManager.Init();
            
            
            Console.CursorVisible = false;

            LogoPrint();

            while (true)
            {
                               
                    gameManager.Update();

                    battleManager.SetAction(gameManager.isAction);
                
                    battleManager.Update();

                    GameManager.ConSoleClear();

                    battleManager.Render();
                    gameManager.Render();
                

            }

            gameManager.End();

            
        }

        static void LogoPrint()
        {
            string str =   "\t╔──────────────────────────────────────────────────╗\r\n" +
                           "\t│███╗   ██╗███████╗ ██████╗██████╗  ██████╗        │\r\n" +
                           "\t│████╗  ██║██╔════╝██╔════╝██╔══██╗██╔═══██╗       │\r\n" +
                           "\t│██╔██╗ ██║█████╗  ██║     ██████╔╝██║   ██║       │\r\n" +
                           "\t│██║╚██╗██║██╔══╝  ██║     ██╔══██╗██║   ██║       │\r\n" +
                           "\t│██║ ╚████║███████╗╚██████╗██║  ██║╚██████╔╝       │\r\n" +
                           "\t│╚═╝  ╚═══╝╚══════╝ ╚═════╝╚═╝  ╚═╝ ╚═════╝        │\r\n" +
                           "\t│                                                  │\r\n" +
                           "\t│██████╗  █████╗ ███╗   ██╗ ██████╗███████╗██████╗ │\r\n" +
                           "\t│██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔══██╗│\r\n" +
                           "\t│██║  ██║███████║██╔██╗ ██║██║     █████╗  ██████╔╝│\r\n" +
                           "\t│██║  ██║██╔══██║██║╚██╗██║██║     ██╔══╝  ██╔══██╗│\r\n" +
                           "\t│██████╔╝██║  ██║██║ ╚████║╚██████╗███████╗██║  ██║│\r\n" +
                           "\t│╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝╚══════╝╚═╝  ╚═╝│\r\n" +
                           "\t╚──────────────────────────────────────────────────╝";

            Console.Write(str);

        }
    }
}
