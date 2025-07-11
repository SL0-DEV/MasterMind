using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Core
{
    public static class GameCore
    {
        // We make a global instance of Commands Manager
        public static CommandsManager CommandsManager { get; } = new CommandsManager();



        // We make a global instace of Game Manager to give access for other scripts
        public static GameManager GameManager { get; } = new GameManager();



        /// <summary>
        /// This function will called on every value we want from player
        /// We make sure is the player calling a command even in the game
        /// </summary>
        /// <returns></returns>
        public static string ReadPlayerInput()
        {


            while (true)
            {

                string? input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) continue;

                if (!int.TryParse(input, out int id) && GameManager.IsGameStarting && !input.StartsWith("!"))
                {
                    continue;
                }

                if (input.StartsWith("!") && GameManager.IsSetUpCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    CommandsManager.ProccessCommand(input);
                    continue;
                }



                if(!input.StartsWith("!") && !GameManager.IsGameStarting && GameManager.IsSetUpCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Try !restart or !start");
                    continue;
                }


                return input;
            }


        }

    }
}
