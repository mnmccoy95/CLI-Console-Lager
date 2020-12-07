using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class JournalEntryRepository : DatabaseConnector, IRepository<JournalEntry>
    {
        public JournalEntryRepository(string connectionString) : base(connectionString) { }

        public void Insert(JournalEntry journalEntry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime )
                                                     VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue("@title", journalEntry.Title);
                    cmd.Parameters.AddWithValue("@content", journalEntry.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", journalEntry.CreateDateTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<JournalEntry> GetAll()
        {
            List<JournalEntry> journalEntries = new List<JournalEntry>();
            return journalEntries;
        }

        public JournalEntry Get(int id)
        {
            JournalEntry journalEntry = new JournalEntry();
            return journalEntry;
        }

        public void Update(JournalEntry journalEntry)
        {
            
        }

        public void Delete(int id)
        {
            
        }
    }
}