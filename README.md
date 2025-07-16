# MasterMind
Master mind is gussing 4 numbers game, the AI will generate number of 4 digits, the player needs to guess the correct number with correct digit placed.

# Instructions
- Installing
  ``` bash
  git clone https://github.com/SL0-DEV/MasterMind.git
  ```

- Runnung
  - You have couple of ways to run the game
    
   You can run by terminal to the path "MasterMind/MasterMind"
  ``` bash
  dotnet run
  ```
  
# Commands 
- !help : to show all the commands in the game
- !setrounds : to modify the number of attempt
- !restart : to restart the game even if you are in the game
- !stop : to force the to be stopped
- !start : to start the game, but you can't call it in the game

# Technical Explanation & Main Mechanics
[GameCore.cs](https://github.com/SL0-DEV/MasterMind/blob/main/MasterMind/Core/GameCore.cs)

We made everything ready to the player by this function
```csharp
     gameManager.Instruction();


            Console.ForegroundColor = ConsoleColor.White;
            while (!gameManager.IsSetUpCompleted)
            {
                if (!gameManager.IsSetUpCompleted)
                    Console.WriteLine("How many rounds you want ? ");

                string roundsWanted = ReadPlayerInput();



                if (int.TryParse(roundsWanted, out int rounds) && rounds <= 10)
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
```

We handle the player input and check if the player wants to call commands.

```csharp
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

```
[CommandsManager.cs](https://github.com/SL0-DEV/MasterMind/blob/main/MasterMind/Core/CommandsManager.cs)

We proccess the commands by this function
``` csharp

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
```
[Game Manager.cs](https://github.com/SL0-DEV/MasterMind/blob/main/MasterMind/Core/GameManager.cs)
We call this function to run the game
``` csharp
        /// <summary>
        /// This function will called by another scripts to start the game
        /// We make sure the game is started to avoid overstart and crashes
        /// </summary>
        public void StartGame()
        {
            if (IsGameStarting) return;


            //Generating the password
            GeneratePassword();


            //Starting the game
            GameLoop();


            m_core.ReadPlayerInput();


        }
```
This function give player instruction and example of game
```csharp
        /// <summary>
        /// This function will give player instructions and introducing the game to him
        /// </summary>
        public void Instruction()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Welcome to MasterMind game");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*=============*\nMaster mind game is about guessing the 4 digit numbers generated by AI");


            Console.WriteLine("You have rounds to guess, when you guess the number I will give you hint by", ConsoleColor.Green);


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Correct digit but it's on wrong place", ConsoleColor.Yellow);

            Console.WriteLine("Correct digit in correct place\n*=============*", ConsoleColor.Yellow);


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("This is an example of the game\n 4592\nWell-placed pieces: 3\nMisplaced pieces: 1", ConsoleColor.Green);

            Console.ForegroundColor = ConsoleColor.White;
        }
```
We generate random password by this function
```csharp
private void GeneratePassword()
        {
            for (int i = 0; i < NumbersToGuess.Length; i++)
            {
                char newValue = char.Parse(m_random.Next(0, 9).ToString());

                if (NumbersToGuess.Contains(newValue))
                {

                    while (NumbersToGuess.Contains(newValue))
                    {

                        newValue = char.Parse(m_random.Next(0, 9).ToString());
                    }
                }

                NumbersToGuess = NumbersToGuess.Replace(NumbersToGuess[i], newValue);


            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("A new paswword has been generated\nGood Luck!");
        }
```
This function handle the gameloop
```csharp
private void GameLoop()
        {
            while (!IsGameOver)
            {
                Update();
            }
        }
        private void Update()
        {
            m_isGameStarting = true;

            // We make sure the player has played all rounds
            if (m_currentRound >= Rounds)
            {
                m_isGameOver = true;

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Game Over\nyou've lost");


                Console.WriteLine($"The password was {NumbersToGuess}");

                m_isGameStarting = false;
                // Stopping the game
                return;
            }


            Console.WriteLine($"Round {m_currentRound}");

            m_CorrectNumberOutPlace = 0;

            m_CorrectNumberInPlace = 0;
            //Reading player input
            string userInput = m_core.ReadPlayerInput();

            //Making sure the input is correct and it's not null
            if (userInput.Length < 5 && userInput != null)
            {
                for (int i = 0; i < userInput.Length; i++){

                    if (userInput[i] == NumbersToGuess[i]){

                        m_CorrectNumberInPlace += 1;
                    }

                    if (userInput[i] != NumbersToGuess[i] && NumbersToGuess.Contains(userInput[i])){

                        int countOfNumber = userInput.Count(number =>  number==userInput[i]);

                        m_CorrectNumberOutPlace += countOfNumber < 2 ? 1 :0;
                    }
                }

                if (m_CorrectNumberInPlace == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine($"Congratz!! You made it (claps *ig*)");

                    m_isGameOver = true;

                    m_isGameStarting = false;
                }
                else
                {
                    Console.WriteLine($"Well-placed pieces: {m_CorrectNumberInPlace}\nMisplaced pieces: {m_CorrectNumberOutPlace}");
                }

                m_currentRound++;
            }
            else
            {


                //Once we know the player have reached above 4 digits we make msg to the player
                Console.WriteLine("Please just 4 digits!!!");
            }

        }
```

# Contact me
[![X](https://img.shields.io/badge/X-1DA1F2?style=for-the-badge&logo=twitter&logoColor=white)](https://x.com/slo_dev)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://www.instagram.com/sl0.dev)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/salem-albanaqi-4914a8302/)
[![Linktree](https://img.shields.io/badge/Linktree-43E660?style=for-the-badge&logo=linktree&logoColor=white)](https://linktr.ee/SLODEV)
