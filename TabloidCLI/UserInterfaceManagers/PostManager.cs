using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
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
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Post Details");
            Console.WriteLine(" 6) Manage Notes");
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
                case "5":
                    Post post = ChooseP();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                case "6": return new NoteManager(this, _connectionString);
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Author Name: {post.Author.FullName}");
                Console.WriteLine($"From Blog: {post.Blog.Title}");
                Console.WriteLine($"Link: {post.Url}");
                Console.WriteLine($"Pushblished: {post.PublishDateTime.ToString("dd-MM-yyyy")}");
                DisplayTags(post.Author.Tags);
                Console.WriteLine("----------------------------------------------------------");
            }
        }
        private void DisplayTags(List<Tag> tags)
        {
            string text = "Tags: ";
            foreach (Tag tag in tags)
            {
                text += $"{tag.Name} ";
            }
            Console.WriteLine(text);
        }

        private Post ChooseP(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseA(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Blog ChooseB(string prompt = null)
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

        public static DateTime PromptDateTime()
        {
            Post post = new Post();
            post.PublishDateTime = new DateTime(0, 0, 0);
            while (post.PublishDateTime.Day == 0)
            {
                Console.WriteLine("Day: ");
                int day = Convert.ToInt32(Console.ReadLine());
                if (day > 0 && day < 31)
                {
                    post.PublishDateTime = new DateTime(0, 0, day);
                }
            }
            while (post.PublishDateTime.Month == 0)
            {
                Console.WriteLine("Month: ");
                int month = Convert.ToInt32(Console.ReadLine());
                if (month > 0 && month < 13)
                {
                    post.PublishDateTime = new DateTime(0, month, post.PublishDateTime.Day);
                }

            }
            while (post.PublishDateTime.Year == 0)
            {
                Console.WriteLine("Year: ");
                int year = Convert.ToInt32(Console.ReadLine());
                if (year > 1752 && year < 10000)
                {
                    post.PublishDateTime = new DateTime(year, post.PublishDateTime.Month, post.PublishDateTime.Day);
                }
            }
            return post.PublishDateTime;
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("Url: ");
            post.Url = Console.ReadLine();

            Console.Write("PublishDateTime (pick a year between 1753 and 9999): ");
            post.PublishDateTime = PromptDateTime();

            Console.Write("Choose an Author");
            post.Author = ChooseA();

            Console.Write("Choose a Blog");
            post.Blog = ChooseB();

            _postRepository.Insert(post);
        }

        private void Edit()
        {
            Post postToEdit = ChooseP("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string Title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Title))
            {
                postToEdit.Title = Title;
            }

            Console.Write("New URL (blank to leave unchanged: ");
            string Url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Url))
            {
                postToEdit.Url = Url;
            }

            Console.Write("New PublishDateTime (Do you want to change the publish date? y/n: ");
            string changeDateTime = Console.ReadLine().ToLower();
            if (changeDateTime == "y")
            {
                postToEdit.PublishDateTime = PromptDateTime();
            }

            Console.Write("New Author (Do you what to change the Author? y/n: ");
            Author changeAuthor = ChooseA();
            if (changeAuthor != null)
            {
                postToEdit.Author = changeAuthor;
            }

            Console.Write("New Blog (Do you want to change the Blog? y/n: ");
            Blog changeBlog = ChooseB();
            if (changeBlog != null)
            {
                postToEdit.Blog = changeBlog;
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = ChooseP("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }     
}

