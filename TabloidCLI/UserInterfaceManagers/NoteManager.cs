using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class NoteManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private int _noteId;
        private string _connectionString;
    

    public NoteManager(IUserInterfaceManager parentUI, string connectionString, int noteId)
    {
        _parentUI = parentUI;
        _noteRepository = new NoteRepository(connectionString);
        _noteId = noteId;
        _connectionString = connectionString;
    }

        public IUserInterfaceManager Execute()
        {
            Note note = _noteRepository.Get(_noteId);
            Console.WriteLine("Notes Menu");
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Notes");
            Console.WriteLine(" 3) Remove Notes");
            Console.WriteLine(" 0) Return");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    
                    return this;
                case "2":
                    return this;
                case "3":
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
