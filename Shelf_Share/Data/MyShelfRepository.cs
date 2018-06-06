using Shelf_Share.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Shelf_Share.Data
{
    public class MyShelfRepository : IMyShelfRepository
    {
        public List<Book> GetUserShelf(string userName)
        {

            using (SqlConnection connection = SqlConnect.GetSqlConnection())
            {

                List<Book> userShelf = new List<Book>();

                string query = @"GetUserShelf";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userName", userName);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //task parallel library, datareader
                    while (reader.Read())
                    {

                        var book = new Book();
                        var author = new Author();
                        var genre = new Genre();


                        //book.Id = Int32.Parse(reader["Id"].ToString());
                        book.Title = reader["Title"].ToString();
                       
                        author.FirstName = reader["FirstName"].ToString();
                        author.LastName = reader["LastName"].ToString();

                        genre.Name = reader["Name"].ToString();


                        book.Genre = genre;
                        book.Author = author;
                        userShelf.Add(book);
                    }

                }



                return userShelf;
            }
        }
    }
}
