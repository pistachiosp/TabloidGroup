using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private BlogRepository _blogRepository;
        private int _blogId;

        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _blogId = blogId;
        }

        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"{blog.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) View Post");
            Console.WriteLine(" 0) Return");

            Console.WriteLine("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "4":
                    ViewPost();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;

            }
        }
        private void View()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"Title: {blog.Title}");
            Console.WriteLine($"Url: {blog.Url}");
            Console.WriteLine("Tags: ");
            foreach (Tag tag in blog.Tags)
            {
                Console.WriteLine(" " + tag);
            }
            Console.WriteLine();
        }
        private void AddTag()
        {
            Blog blog = _blogRepository.Get( _blogId);

            Console.WriteLine($"What tag would you like to add to {blog.Title}?");
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) + {tag.Name}");
            }
            Console.WriteLine(">");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _blogRepository.InsertTag(blog, tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Thats not a correct choice obviously");
            }
        }
        private void RemoveTag()
        {
            Blog blog = _blogRepository.Get(_blogId);

            Console.WriteLine($"Which tag would you like to remove from {blog.Title}?");
            List<Tag> tags = blog.Tags;

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _blogRepository.DeleteTag(blog.Id, tag.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't remove any tags.");
            }
        }
        private void ViewPost()
        {
            throw new NotImplementedException();
        }

    }
}
