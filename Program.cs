using NecroDancer;
using System;
using System.Diagnostics;

namespace ConsoleNecroDancer
{
   

    internal class Program
    {
        
        
        
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            BattleManager battleManager = new BattleManager();

            battleManager.Init();
            gameManager.Init();
            Console.CursorVisible = false;

            while (true)
            {
                gameManager.Update();
                battleManager.Update();
                battleManager.Render();
            }
        }

        
    }
}
