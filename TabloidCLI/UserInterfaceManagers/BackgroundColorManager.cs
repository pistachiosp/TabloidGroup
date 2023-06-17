using System;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BackgroundColorManager : IUserInterfaceManager
    {
        private string _connectionString;
        private readonly IUserInterfaceManager _parentUI;

        public BackgroundColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Choose your Background Color");
            Console.WriteLine(" 1) Black");
            Console.WriteLine(" 2) Blue");
            Console.WriteLine(" 3) Cyan");
            Console.WriteLine(" 4) Dark Gray");
            Console.WriteLine(" 5) Dark Red");
            Console.WriteLine(" 6) Dark Yellow");
            Console.WriteLine(" 7) White");
            Console.WriteLine(" 8) Green");
            Console.WriteLine();
            Console.WriteLine("Choose your Text Color");
            Console.WriteLine(" 9) Blue");
            Console.WriteLine(" 10) White");
            Console.WriteLine(" 11) Black");
            Console.WriteLine(" 12) Red");
            Console.WriteLine(" 0) Go Back ");


            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Black;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Blue; 
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    return this;
                case "7":
                    Console.BackgroundColor = ConsoleColor.White;
                    return this;
                case "8":
                    Console.BackgroundColor = ConsoleColor.Green;
                    return this;


                case "9":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    return this;
                case "10":
                    Console.ForegroundColor = ConsoleColor.White; 
                    return this;
                case "11":
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "12":
                    Console.ForegroundColor = ConsoleColor.Red;
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
