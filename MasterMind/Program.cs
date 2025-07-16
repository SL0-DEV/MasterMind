using System;
using MasterMind.Core;


public static class Program
{
    private static void Main(string[] args)
    {
        GameCore gameCore = new GameCore();
        gameCore.Validate();
        gameCore.SetUp();

    }    

}
