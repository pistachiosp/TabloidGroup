using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostMenuManager : IUserInterfaceManager 
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostMenuManager(IUserInterfaceManager parentUI, string connectionString)
        {
        _parentUI = parentUI;
        _postRepository = new PostRepository(connectionString);
          _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

       
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Post");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.WriteLine(">");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Post List");
                    List<Post> posts = _postRepository.GetAll();
                    foreach (Post post in posts)
                    {
                        Console.WriteLine($"{post.Id}: {post.Title} {post.Url} ");
                    }
                    return this;
                case "2":
                    Console.WriteLine("Add post");
              
                    Console.Write("Title of post: ");
                    string postTitle = Console.ReadLine();
                    Console.Write("Enter URL: ");
                    string postUrl = Console.ReadLine();
                    List<Author> authors = _authorRepository.GetAll();
                    foreach (Author a in authors)
                    {
                        Console.WriteLine($"{a.Id} - {a.FirstName} {a.LastName}");
                    }
                    Console.Write("Which author wrote this post? ");
                    int PostAuthorId = int.Parse(Console.ReadLine());
                    
                    Console.WriteLine();
                    List<Blog> blogs = _blogRepository.GetAll();
                    foreach (Blog b in blogs)
                    {
                        Console.WriteLine($"{b.Id} - {b.Title}");
                    }
                    Console.Write("Which blog did this post come from? ");
                    int postBlogId = int.Parse(Console.ReadLine());

                    Post addPost = new Post()
                    {
                        Title = postTitle,
                        Url = postUrl,
                        PublishDateTime = DateTime.Now,
                        Author = new Author
                        {
                            Id = PostAuthorId
                        },
                        Blog = new Blog
                        {
                            Id = postBlogId
                        }
                    };

                    _postRepository.Insert(addPost);

                    Console.WriteLine($"Your post was successfully added!");
                    return this;
                case "3":
                    return this;
                case "4":
                    return this;
                case "5":
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
