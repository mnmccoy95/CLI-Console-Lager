using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalEntryManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalEntryRepository _journalEntryRepository;
        private string _connectionString;

        public JournalEntryManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalEntryRepository = new JournalEntryRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Management Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add Journal Entry");
            Console.WriteLine(" 3) Edit Journal Entry");
            Console.WriteLine(" 4) Remove Journal Entry");
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
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<JournalEntry> allJournals = _journalEntryRepository.GetAll();
            foreach (JournalEntry journal in allJournals)
            {
                Console.WriteLine("");
                Console.WriteLine($"{journal.Id}) {journal.Title} - {journal.CreateDateTime}");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"{journal.Content}");
                Console.WriteLine("");
            }

        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            JournalEntry journalEntry = new JournalEntry();

            Console.Write("Title: ");
            journalEntry.Title = Console.ReadLine();

            Console.Write("Content: ");
            journalEntry.Content = Console.ReadLine();

            journalEntry.CreateDateTime = DateTime.Now;

            _journalEntryRepository.Insert(journalEntry);
        }

        private JournalEntry Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Entry:";
            }

            Console.WriteLine(prompt);

            List<JournalEntry> entries = _journalEntryRepository.GetAll();

            for (int i = 0; i < entries.Count; i++)
            {
                JournalEntry entry = entries[i];
                Console.WriteLine($" {i + 1}) {entry.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return entries[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void Edit()
        {
            JournalEntry journalToEdit = Choose("Which entry would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                journalToEdit.Title = title;
            }
            Console.Write("New content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journalToEdit.Content = content;
            }

            _journalEntryRepository.Update(journalToEdit);
        }
    }
}