using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloidCLI.Repositories
{
    internal class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            throw new NotImplementedException();
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Journal> GetByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT into Journal (Title, Content, CreateDateTime)
                                        OUTPUT INSERTED.Id
                                        VALUES(@title, @content, @createDateTime)"; 
                        
                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", journal.CreateDateTime);
                    int id = (int)cmd.ExecuteScalar();

                    journal.Id = id;
                }
            }
        }

        public void Update(Journal journal)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
