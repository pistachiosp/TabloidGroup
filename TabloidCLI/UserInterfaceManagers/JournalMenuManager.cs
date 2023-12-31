﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                    List<Journal> journals =  _journalRepository.GetAll();
                    foreach(Journal j in journals)
                    {
                        Console.WriteLine($"{j.Title} has an Id of {j.Id}. It was posted on {j.CreateDateTime}. This is the content: {j.Content}");
                    }
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
                    Console.WriteLine("Edit Journal Entry");
                    List<Journal> journalTwo = _journalRepository.GetAll();
                    foreach (Journal j in journalTwo)
                    {
                        Console.WriteLine($"{j.Id} - {j.Title}.");
                    }
                    Console.Write("Which journal entry would you like to edit? ");
                    string journalId = Console.ReadLine();
                    if (journalId == "")
                    {
                        return this;
                    } else
                    {
                    Console.Write("What would you like the new title to be?");
                    string newTitle = Console.ReadLine();
                    Console.Write("What would you like the journal content to be? ");
                    string newContent = Console.ReadLine();

                    Journal journalEdit = new Journal
                    {
                        Id = int.Parse(journalId),
                        Title = newTitle,
                        Content = newContent
                    };

                    _journalRepository.Update(journalEdit);

                    Console.WriteLine("Your journal entry has been updated!");
                    }
                    return this;
                case "4":
                    List<Journal> journalsDelete = _journalRepository.GetAll();
                    foreach (Journal j in journalsDelete)
                    {
                        Console.WriteLine($"{j.Id} - {j.Title}");
                    }
                    Console.Write("Which journal do you want to delete?");
                    int journalToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        _journalRepository.Delete(journalToDelete);
                    }
                    catch
                    {
                        Console.WriteLine("Cannot Delete");
                    }
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
