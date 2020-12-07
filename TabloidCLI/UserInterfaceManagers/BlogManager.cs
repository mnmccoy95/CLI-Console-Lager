using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
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

            Console.WriteLine("1) Add a blog");
            Console.WriteLine("2) List all blogs");
            Console.WriteLine("3) Delete a blog");
            Console.WriteLine("4) Edit a blog");
            Console.WriteLine("5) View Blog Details");
            Console.WriteLine("0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Add();
                    return this; 
                case "2":
                    List();
                    return this;
                case "3":
                    Delete();
                    return this;
                case "4": 
                    Update();
                    return this;
                case "5":
                    ViewDetails();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

           
        }

        private void ViewDetails()
        {
            List();
            Console.Write("Select a blog to view the details: ");
            int focusBLog = int.Parse(Console.ReadLine());
            Blog blog = _blogRepository.Get(focusBLog);
            Console.WriteLine($"{blog.Title} - {blog.Url}");
            Console.WriteLine("1) View");
            Console.WriteLine("2) Add Tag");
            Console.WriteLine("3) Remove Tag");
            Console.WriteLine("4) View Posts");
            Console.WriteLine("0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    throw new NotImplementedException();
                case "2":
                    throw new NotImplementedException();
                case "3":
                    throw new NotImplementedException();
                case "4":
                    throw new NotImplementedException();
                case "0":
                    return;
            }

        }

        private void Update()
        {
            List();
            Console.Write("Enter the ID of the blog to edit ");
            int id = int.Parse(Console.ReadLine());

            string title = "";
            while (title == "")
            {
                Console.Write("Enter the new title: ");
                title = Console.ReadLine();
            }

            string url = "";
            while (url == "")
            {
                Console.Write("Enter the new URL: ");
                url = Console.ReadLine();
            }            

            Blog blog = new Blog()
            {
                Title = title,
                Url = url,
                Id = id
            };

            _blogRepository.Update(blog);
        }

        private void Delete()
        {
            List();
            Console.Write("Enter the ID of the blog to delete: ");
            int blogToDelete = int.Parse(Console.ReadLine());
            _blogRepository.Delete(blogToDelete);

        }

        private void List()
        {
            List<Blog> allBlogs = _blogRepository.GetAll();
            foreach (Blog blog in allBlogs)
            {
                Console.WriteLine($"{blog.Id}) {blog.Title} - {blog.Url}");
            }
        }

        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.Write("Enter Blog Title: ");
            blog.Title = Console.ReadLine();

            Console.Write("Enter Blog URL: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);
        }

    }
}
