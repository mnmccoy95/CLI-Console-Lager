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
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, Title, Content, CreateDateTime FROM Journal";

                    List<JournalEntry> journals = new List<JournalEntry>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        JournalEntry journal = new JournalEntry()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };
                        journals.Add(journal);
                    }
                    reader.Close();
                    return journals;
                }
            }
        }

        public JournalEntry Get(int id)
        {
            JournalEntry journalEntry = new JournalEntry();
            return journalEntry;
        }

        public void Update(JournalEntry journalEntry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal 
                                           SET Title = @title,
                                               Content = @content,
                                               CreateDateTime = @createDateTime
                                         WHERE id = @id";

                    cmd.Parameters.AddWithValue("@title", journalEntry.Title);
                    cmd.Parameters.AddWithValue("@content", journalEntry.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", journalEntry.CreateDateTime);
                    cmd.Parameters.AddWithValue("@id", journalEntry.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Journal WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}