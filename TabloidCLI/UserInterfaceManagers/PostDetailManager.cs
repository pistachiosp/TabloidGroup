using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private BlogRepository _blogRepository;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Return");

            Console.Write("> ");
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
                    NoteManagement();
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
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Url: {post.Url}");
            Console.WriteLine($"Publish Date: {post.PublishDateTime}");
        }

        private void AddTag()
        {
            throw new NotImplementedException();
        }

        private void RemoveTag()
        {
            throw new NotImplementedException();
        }

        private void NoteManagement()
        {
            throw new NotImplementedException();
        }
    }
}
