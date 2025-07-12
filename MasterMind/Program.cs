using System;
using MasterMind.Core;


public static class Program
{
    private static void Main(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("-"))
            {

            }
        }
        GameCore gameCore = new GameCore();
        gameCore.Validate();
        gameCore.SetUp();

    }
    

}
