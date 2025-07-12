using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Core
{
    public class CommandsManager
    {

        public GameCore gameCore(GameCore game)
        {
            m_core = game;
            return m_core;
        }
        public bool isReadingCommands = false;


        private string[] commands = {"!help","!restart","!exit","!setrounds","!stop", "!start"};

        private GameCore m_core;


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
                        m_core.gameManager.RestartTheGame();
                        break;


                    case "!exit":
                        Environment.Exit(0);
                        break;


                    case "!setrounds":
                        if (m_core.gameManager.IsGameStarting)
                        {
                            Console.WriteLine("You can't change rounds while game is running");
                        }
                        else
                        {
                            Console.WriteLine("How many rounds you want");
                            string input = m_core.ReadPlayerInput();
                            m_core.gameManager.SetRounds(int.Parse(input));
                        }
                        break;


                    case "!stop":
                        if (m_core.gameManager.IsGameStarting)
                        {
                            m_core.gameManager.StopTheGame();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Game has been stopped");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;


                    case "!start":
                        if (!m_core.gameManager.IsGameStarting)
                        {
                            m_core.gameManager.StartGame();
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
