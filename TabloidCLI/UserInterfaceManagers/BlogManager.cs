using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

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
            Console.WriteLine("--------------------");
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
            Console.WriteLine("--------------------");
            List();
            Console.Write("Select a blog to view the details: ");
            int focusBLog = int.Parse(Console.ReadLine());
            Console.WriteLine("--------------------");
            Blog blog = _blogRepository.Get(focusBLog);
            
            

            Console.WriteLine($"{blog.Title} - {blog.Url}");
            
           

            Console.WriteLine(" ");

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
                    List<Tag> blogTags = _blogRepository.GetLinkedTags(blog.Id);
                    Console.WriteLine("--------------------");
                    Console.WriteLine($"{blog.Title}");
                    Console.WriteLine("Tags: ");
                    foreach (Tag tag in blogTags)
                    {
                        Console.WriteLine($"{tag.Name} ");
                    }
                    //TagList();
                    return;
                case "2":
                    AddTag(blog);
                    return;
                case "3":
                    RemoveTag(blog);
                    return;
                case "4":
                    ViewBlogPosts(blog);
                    return;
                case "0":
                    return;
            }

        }

        private void ViewBlogPosts(Blog blog)
        {
            List<Post> blogPosts = _blogRepository.GetLinkedPosts(blog.Id);
            foreach (Post post in blogPosts)
            {
                Console.WriteLine($"{post.Id}) {post.Title} - {post.Url} - {post.PublishDateTime}");
            }
        }

        private void RemoveTag(Blog blog)
        {
            List<Tag> linkedTags = _blogRepository.GetLinkedTags(blog.Id);
            foreach (Tag tag in linkedTags)
            {
                Console.WriteLine($"{tag.Id}) {tag.Name}");
            }
            Console.Write("Enter the ID of the tag you wish to delete: ");
            int tagToDelete = int.Parse(Console.ReadLine());
            _blogRepository.DeleteTag(tagToDelete);
        }

        private void AddTag(Blog blog)
        {
            Console.WriteLine("--------------------");
            TagList();
            Console.Write("Select a tag to add: ");
            int tagId = int.Parse(Console.ReadLine());
            BlogTag newTag = new BlogTag
            {
                BlogId = blog.Id,
                TagId = tagId
            };
            _blogRepository.InsertTag(newTag);
        }

        public void TagList()
        {
            List<Tag> allTags = _blogRepository.GetAllTags();
            foreach (Tag tag in allTags)
            {
                Console.WriteLine($"{tag.Id}) {tag.Name}");
            }
        }

        private void Update()
        {
            List();
            Console.Write("Enter the ID of the blog to edit ");
            int id = int.Parse(Console.ReadLine());
            Blog oldBlog = _blogRepository.Get(id);
                      
            Console.Write("Enter the new title: ");
            string title = Console.ReadLine();
                       
            Console.Write("Enter the new URL: ");
            string url = Console.ReadLine();
                 
            if (title == "")
            {
                title = oldBlog.Title;
            }

            if (url == "")
            {
                url = oldBlog.Url;
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
