using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class SearchMenuManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private SearchRepository _seaRepository;
        private string _connectionString;

        public SearchMenuManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _searchRepository = new SearchRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            public IUserInterfaceManager Execute()
            {
                Console.WriteLine("Search Menu");
                Console.WriteLine(" 1) Search Blogs");
                Console.WriteLine(" 2) Search Authors");
                Console.WriteLine(" 3) Search Posts");
                Console.WriteLine(" 4) Search All");
                Console.WriteLine(" 0) Return to Main Menu");

                Console.WriteLine(">");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        return this;
                    case "2":
                        return this;
                    case "3":
                        return this;
                    case "4":
                        return this;
                    case "0":
                        return _parentUI;
                    default: return this;
                }
            }
        }
    }
}


