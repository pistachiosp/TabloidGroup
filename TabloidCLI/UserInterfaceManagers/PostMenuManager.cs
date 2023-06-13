using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostMenuManager : IUserInterfaceManager 
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
    }

    public PostMenuManager(IUserInterfaceManager parentUI, string conncectionString)
    {
        _parentUI = parentUI;
        _postRepository = new PostRepository(conncectionString);
    }

    public IUserInterfaceManager Execute()
    {
        Console.WriteLine("Post Menu");
        Console.WriteLine("List Post");
        Console.WriteLine("Add Post");
        Console.WriteLine("Edit Post");
        Console.WriteLine("Remove Post");
        Console.WriteLine("Note Management");
        Console.WriteLine("Return to Main Menu");
    }
}
