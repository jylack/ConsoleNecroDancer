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
                

                //겜매니저의 하트 액션값을 넘겨줌
                //잘들어가나 테스트
                //Console.SetCursorPosition(20, 15);
                //Console.WriteLine(gameManager.isAction);

                battleManager.SetAction(gameManager.isAction);

                battleManager.Update();
                
                //GameManager.ConSoleClear();

                battleManager.Render();
                gameManager.Render();
                
            }

            
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
