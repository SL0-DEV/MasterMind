using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Core
{
    public class CommandsManager
    {

        public bool isReadingCommands = false;


        private string[] commands = {"!help","!restart","!exit","!setrounds","!stop", "!start"};



        /// <summary>
        /// This function called from other scripts to process any commands in the game
        /// Will handles the commands to control the game
        /// </summary>
        /// <param name="command"></param>
        public void ProccessCommand(string command)
        {
            if (commands.Contains(command))
            {
                switch (command)
                {


                    case "!help":
                        Console.ForegroundColor=  ConsoleColor.Green;
                        Console.WriteLine("!help\n!restart : to restart the whole game even you are ingame\n!exit : to exit the app\n!setrounds : to set rounds of the game\n!stop : to force the game to be stopped\n!start : to start the game");
                        break;


                    case "!restart":
                        GameCore.GameManager.RestartTheGame();
                        break;


                    case "!exit":
                        Environment.Exit(0);
                        break;


                    case "!setrounds":
                        if (GameCore.GameManager.IsGameStarting)
                        {
                            Console.WriteLine("You can't change rounds while game is running");
                        }
                        else
                        {
                            Console.WriteLine("How many rounds you want");
                            string input = GameCore.ReadPlayerInput();
                            GameCore.GameManager.SetRounds(int.Parse(input));
                        }
                        break;


                    case "!stop":
                        if(GameCore.GameManager.IsGameStarting)
                        GameCore.GameManager.StopTheGame();
                        break;


                    case "!start":
                        if (!GameCore.GameManager.IsGameStarting)
                        {
                            GameCore.GameManager.StartGame();
                        }
                        break;
                }
            }else
            {
                
                Console.WriteLine("Command not found! try !help");
            }



            isReadingCommands = true;

        }

    }
}
