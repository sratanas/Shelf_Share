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
                       
                        author.Name = reader["Name"].ToString();

                        genre.Name = reader["Name"].ToString();


                        book.Genre = genre;
                        book.Author = author;
                        userShelf.Add(book);
                    }

                }



                return userShelf;
            }
        }

        public List<Book> GetBooksByAuthor(string authorName)
        {
            using (SqlConnection connection = SqlConnect.GetSqlConnection())
            {
                List<Book> authorBookList = new List<Book>();


                string query = @"GetBooksByAuthor";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AuthorName", authorName);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        var book = new Book();
                        var author = new Author();
                        var genre = new Genre();

                        book.Id = Int32.Parse(reader["Id"].ToString());
                        book.Title = reader["Title"].ToString();


                        author.Name = reader["Name"].ToString();
                        

                        genre.Id = Int32.Parse(reader["Id"].ToString());
                        genre.Name = reader["Name"].ToString();

                        book.Genre = genre;
                        book.Author = author;

                        authorBookList.Add(book);
                    }

                }


                return authorBookList;
            }
        }
    }
}
