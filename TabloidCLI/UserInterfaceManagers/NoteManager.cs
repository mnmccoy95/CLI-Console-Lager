using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private string _connectionString;
        private NoteRepository _noteRepository;
        public NoteManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _noteRepository = new NoteRepository(connectionString);
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Note");
            Console.WriteLine(" 2) Add Note");
            Console.WriteLine(" 3) Remove Note");
            Console.WriteLine(" 0) Go Back");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    //                    List();
                    Console.WriteLine("Not yet implemented!");
                    return this;
                case "2":
                    //Add();
                    Console.WriteLine("Not yet implemented!");
                    return this;
                case "3":
                    //                    Remove();
                    Console.WriteLine("Not yet implemented!");
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
            List<Note> notes = _noteRepository.GetAll();
        }
    }
}
