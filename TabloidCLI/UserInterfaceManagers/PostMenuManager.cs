using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using System.ComponentModel;

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
                    Console.WriteLine("Edit Post Entry");
                    List<Post> postEdit = _postRepository.GetAll();
                    foreach (Post p in postEdit)
                    {
                        Console.WriteLine($"{p.Id} - {p.Title}");
                    }
                    Console.WriteLine("Pick your post to edit ");
                    string postId = Console.ReadLine();
                    if (postId == "")
                    {
                        return this;
                    } else
                    {
                        Console.Write("Pick a new title ");
                        string newPostTitle = Console.ReadLine();
                        Console.Write("change URL ");
                        string newPostUrl = Console.ReadLine();

                        List<Author> authorList = _authorRepository.GetAll();
                        foreach (Author author in authorList)
                        {
                            Console.WriteLine($"{author.Id} -           {author.FullName}");
                        }
                        Console.Write("Pick an Author ");
                        int newauthorId = int.Parse(Console.ReadLine());
                        List<Blog> blogList = _blogRepository.GetAll();
                        foreach (Blog blog in blogList)
                        {
                            Console.WriteLine($"{blog.Id} - {blog.Title}");
                        }
                        Console.Write("Pick a Blog ");
                        int newblogId = int.Parse(Console.ReadLine());

                        Post postToEdit = new Post
                        {
                            Id = int.Parse(postId),
                            Title = newPostTitle,
                            Url = newPostUrl,
                            Author = new Author
                            {
                                Id = newauthorId,
                            },
                            Blog = new Blog
                            { Id = newblogId }

                        };
                        _postRepository.Update(postToEdit);
                        Console.WriteLine("Your post has been updated!");
                    }
                    return this;
                case "4":
                    List<Post> postsToDelete = _postRepository.GetAll();
                    foreach (Post pDelete in postsToDelete)
                    {
                        Console.WriteLine($"{pDelete.Id} - {pDelete.Title}");
                    }
                    Console.Write("Which post would you like to delete?: ");
                    int postToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        _postRepository.Delete(postToDelete);
                    }
                    catch
                    {
                        Console.WriteLine("Cannot Delete");
                    }
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
