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
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Write your name/nickname");
        string userName = GameCore.ReadPlayerInput();
        if (userName.Length > 0 && userName[0] != '!')
        {
            manager.SetUserName(userName);
        }

        Console.WriteLine("How many rounds you want ? ");
        string roundsWant = GameCore.ReadPlayerInput();
        if(roundsWant.Length < 3)
        {
            manager.SetRounds(int.Parse(roundsWant));
        }else
        {
            Console.WriteLine("Only 2 digits please");
        }
        manager.SetSetupState(true);
        manager.StartGame();
    }

}
