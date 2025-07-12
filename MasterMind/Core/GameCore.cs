namespace MasterMind.Core
{
    public class GameCore
    {
        // We make a global instance of Commands Manager
        public CommandsManager commandsManager { get; } = new CommandsManager();



        // We make a global instace of Game Manager to give access for other scripts
        public GameManager gameManager { get; } = new GameManager();


        public void Validate()
        {
            gameManager.gameCore(this);
            commandsManager.gameCore(this);
        }
        /// <summary>
        /// This function will called on every value we want from player
        /// We make sure is the player calling a command even in the game
        /// </summary>
        /// <returns></returns>
        public string ReadPlayerInput()
        {
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) return string.Empty;
            if(input ==null)
            {
                Environment.Exit(0);
            }

            if (!int.TryParse(input, out int id) && gameManager.IsGameStarting && !input.StartsWith("!"))
            {
                return string.Empty;
            }
            if(input == "!stop")
            {
                commandsManager.ProccessCommand(input);
                return string.Empty;
            }
            if (input.StartsWith("!") && gameManager.IsSetUpCompleted && !gameManager.IsGameStarting)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                commandsManager.ProccessCommand(input);
                return string.Empty;
            }



            if (!input.StartsWith("!") && !gameManager.IsGameStarting && gameManager.IsSetUpCompleted)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Try !restart or !start");
                return string.Empty;
            }


            return input;


        }

        /// <summary>
        /// This function called once the app start
        /// We make sure everything is goind well
        /// Reading player name and how many rounds he wants to play
        /// </summary>
        public void SetUp()
        {

            gameManager.gameCore(this);
            gameManager.SetSetupState(false);

            gameManager.Instruction();


            Console.ForegroundColor = ConsoleColor.White;
            while (!gameManager.IsSetUpCompleted)
            {
                if (!gameManager.IsSetUpCompleted)
                    Console.WriteLine("How many rounds you want ? ");

                string roundsWant = ReadPlayerInput();



                if (int.TryParse(roundsWant, out int rounds) && rounds <= 10)
                {
                    gameManager.SetRounds(rounds);

                    gameManager.SetSetupState(true);

                    Console.WriteLine("Write !start to start the game");

                    Console.WriteLine("You can write !help to get all commands.");


                }
                else
                {
                    Console.WriteLine("You can choose 10 or below rounds also just number no letters ");
                }


            }

            ReadPlayerInput();


        }

    }
}
