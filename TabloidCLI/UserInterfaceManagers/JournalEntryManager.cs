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
    }
}