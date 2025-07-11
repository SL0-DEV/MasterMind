using System;
using MasterMind.Core;


public static class Program
{
    private static GameManager manager = GameCore.GameManager;
    private static void Main()
    {
        SetUp();
    }
    /// <summary>
    /// This function called once the app start
    /// We make sure everything is goind well
    /// Reading player name and how many rounds he wants to play
    /// </summary>
    private static void SetUp()
    {
        manager.SetSetupState(false);

        manager.Instruction();
        
        
        Console.ForegroundColor = ConsoleColor.White;
        while (true)
        {
            if(!manager.IsSetUpCompleted)
            Console.WriteLine("How many rounds you want ? ");

            string roundsWant = GameCore.ReadPlayerInput();



            if (int.TryParse(roundsWant,out int rounds) && rounds <= 10)
            {
                manager.SetRounds(rounds);

                manager.SetSetupState(true);

                Console.WriteLine("Write !start to start the game");

                Console.WriteLine("You can write !help to get all commands.");


            }
            else
            {
                Console.WriteLine("You can choose 10 or below rounds also just number no letters ");
            }


        }



    }

}
