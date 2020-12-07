using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class ColorManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private string _connectionString;

        public ColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine(" 1) Red");
            Console.WriteLine(" 2) Dark Yellow");
            Console.WriteLine(" 3) Yellow");
            Console.WriteLine(" 4) Green");
            Console.WriteLine(" 5) Blue");
            Console.WriteLine(" 6) Magenta");
            Console.WriteLine(" 7) Black");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Clear(); // Buffer must be clear
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Clear(); // Buffer must be clear
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Clear(); // Buffer must be clear
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Clear(); // Buffer must be clear
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear(); // Buffer must be clear
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.Clear(); // Buffer must be clear
                    return this;
                case "7":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear(); // Buffer must be clear
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
}