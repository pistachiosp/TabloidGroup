using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _tagRepository = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine($"{tag.Id} - {tag.Name}");
            }
        }

        private void Add()
        {
            Console.WriteLine("Add a new tag!");
            Tag tag = new Tag();

            Console.Write("Tag name: ");
            tag.Name = Console.ReadLine();

            _tagRepository.Insert(tag);

            Console.WriteLine("Your new tag was added!");
        }

        private void Edit()
        {
            Console.WriteLine("Tag to Edit");
            List<Tag> editTags = _tagRepository.GetAll();
            foreach (Tag t in editTags)
            {
                Console.WriteLine($"{t.Id} - {t.Name}");
            }
            Console.Write("Which tag would you like to edit? ");
            string editTagId = Console.ReadLine();
            if (editTagId == "")
            {
                return;
            }
            else 
            {
                Console.Write("Pick a new tag name: ");
                string newTagName = Console.ReadLine();

                Tag tagToEdit = new Tag
                {
                    Id = int.Parse(editTagId),
                    Name = newTagName
                };

                _tagRepository.Update(tagToEdit);

                Console.WriteLine("Your tag was edited!");
            }
        }

        private void Remove()
        {
            Console.WriteLine("Remove Tag");
            List<Tag> removeTags = _tagRepository.GetAll();
            foreach (Tag rT in removeTags)
            {
                Console.WriteLine($"{rT.Id} - {rT.Name}");
            }
            Console.Write("Which tag would you like to remove? ");
            int removeTagId = int.Parse(Console.ReadLine());
            try
            {

                _tagRepository.Delete(removeTagId);

                Console.WriteLine("Tag was successfully removed!");
            }
            catch
            {
                Console.WriteLine("Cannot remove tag.");
            }

        }
    }
}
