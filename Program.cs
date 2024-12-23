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
            gameManager.Init();


            while (true)
            {
                gameManager.Update();
            }
        }

        
    }
}
