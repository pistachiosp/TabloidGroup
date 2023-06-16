using System;
using TabloidCLI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Add Blog");
            Console.WriteLine(" 3) Edit Blog");
            Console.WriteLine(" 4) Remove Blog");
            Console.WriteLine(" 5) Blog Details");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List<Blog> blogs = _blogRepository.GetAll();
                    foreach(Blog b in blogs)
                    {
                        Console.WriteLine($"{b.Id}- {b.Title} - {b.Url}");
                    }
                    return this;
                case "2":
                    Console.WriteLine("Add a Blog");

                    Console.Write("Title of Blog: ");
                    string blogTitle = Console.ReadLine();

                    Console.Write("Add URL for the Blog: ");
                    string blogUrl = Console.ReadLine();

                    Blog addBlog = new Blog()
                    {
                        Title = blogTitle,
                        Url = blogUrl
                    };

                    _blogRepository.Insert(addBlog);
                    Console.WriteLine("Blog added successfully!");
                    return this;
                case "3":
                    Console.WriteLine("Edit Blog");
                    List<Blog> BlogTwo = _blogRepository.GetAll();
                    foreach (Blog b in BlogTwo)
                    {
                        Console.WriteLine($"{b.Id} - {b.Title} - {b.Url}.");
                    }
                    Console.Write("Which blog would you like to edit? ");
                    string blogId = Console.ReadLine();
                    if (blogId == "")
                    {
                        return this;
                    }
                    else
                    {
                        Console.Write("What would you like the new title to be?");
                        string newTitle = Console.ReadLine();
                        Console.Write("What would you like the new Url to be? ");
                        string newUrl = Console.ReadLine();

                        Blog blogEdit = new Blog()
                        {
                            Id = int.Parse(blogId),
                            Title = newTitle,
                            Url = newUrl
                        };

                        _blogRepository.Update(blogEdit);

                        Console.WriteLine("Your blog has been updated!");
                    }
                    return this;
                case "4":
                    List<Blog> blogsToDelete = _blogRepository.GetAll();
                    foreach (Blog b in blogsToDelete)
                    {
                        Console.WriteLine($"{b.Id}- {b.Title} - {b.Url}");
                    }
                    Console.Write("Which blog do you want to remove?: ");
                    int deletedBlog = int.Parse(Console.ReadLine());
                    try
                    {
                        _blogRepository.Delete(deletedBlog);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Console.WriteLine("Your blog was successfully deleted!");
                    return this;
                    case "5":
                    Blog blog = Choose();
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
    }
}
