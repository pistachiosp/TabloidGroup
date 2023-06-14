using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class JournalMenuManager : IUserInterfaceManager 
    {
        private IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalMenuManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI=parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString=connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journals");
            Console.WriteLine(" 2) Add Journal");
            Console.WriteLine(" 3) Edit Journal");
            Console.WriteLine(" 4) Remove Journal");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.WriteLine(">");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    return this;
                case "2":
                    Console.WriteLine("Add Journal");

                    Console.Write("Title of Journal: ");
                    string JournalTitle = Console.ReadLine();
                    Console.Write("Write your journal: ");
                    string JournalContent = Console.ReadLine();

                    Journal AddJournal = new Journal
                    {
                        Title = JournalTitle,
                        Content = JournalContent,
                        CreateDateTime = DateTime.Now
                    };

                    _journalRepository.Insert(AddJournal);
                    Console.WriteLine("Your journal was successfully added!");
                    return this;
                case "3":
                    return this;
                case "4":
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
