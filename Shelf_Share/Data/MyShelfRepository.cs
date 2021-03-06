﻿using Microsoft.Extensions.Configuration;
using Shelf_Share.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Shelf_Share.Data
{
    public class MyShelfRepository : IMyShelfRepository
    {

        private readonly IConfiguration _config;
        public  MyShelfRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public SqlConnection GetSqlConnection()
        {

            string ConnectionString = _config["TheDefaultConnection"];
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();



            return conn;
        }

        public List<Book> GetUserShelf(string userName)
        {
            
            using (SqlConnection connection = GetSqlConnection())
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
                        var user = new ApplicationUser();


                        book.Id = Int32.Parse(reader["Id"].ToString());
                        book.Title = reader["Title"].ToString();

                        author.AuthorName = reader["AuthorName"].ToString();

                        genre.GenreName = reader["GenreName"].ToString();

                        user.Id = reader["Id"].ToString();


                        book.Genre = genre;
                        book.Author = author;
                        book.User = user;
                        userShelf.Add(book);
                    }

                }



                return userShelf;
            }
        }

        public List<Book> GetBooksByAuthor(string authorName)
        {
            using (SqlConnection connection = GetSqlConnection())
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


                        author.AuthorName = reader["AuthorName"].ToString();


                        genre.Id = Int32.Parse(reader["Id"].ToString());
                        genre.GenreName = reader["GenreName"].ToString();

                        book.Genre = genre;
                        book.Author = author;

                        authorBookList.Add(book);
                    }
                }

                return authorBookList;
            }
        }

        public List<Book> GetBooksByTitle(string title)
        {
            using (SqlConnection connection = GetSqlConnection())

            {
                List<Book> bookList = new List<Book>();

                string query = @"GetBooksByTitle";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TitleSearchParam", title);

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

                        author.Id = Int32.Parse(reader["Id"].ToString());
                        author.AuthorName = reader["AuthorName"].ToString();


                        genre.Id = Int32.Parse(reader["Id"].ToString());
                        genre.GenreName = reader["GenreName"].ToString();

                        book.Genre = genre;
                        book.Author = author;

                        bookList.Add(book);
                    }
                }

                return bookList;
            }
        }

        public Book GetBookById(int id)
        {
            using (SqlConnection connection = GetSqlConnection())

            {
                var book = new Book();

                string query = @"GetBookById";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var author = new Author();
                        var genre = new Genre();

                        book.Id = Int32.Parse(reader["Id"].ToString());
                        book.Title = reader["Title"].ToString();
                        book.ISBN = reader["ISBN"].ToString();


                        author.Id = Int32.Parse(reader["Id"].ToString());
                        author.AuthorName = reader["AuthorName"].ToString();


                        genre.Id = Int32.Parse(reader["Id"].ToString());
                        genre.GenreName = reader["GenreName"].ToString();

                        book.Genre = genre;
                        book.Author = author;

                    }
                }

                return book;
            }
        }

        public void AddBookToShelfShare(Book book)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
                //var book = new Book();

                string query = @"CheckDatabaseForAuthor";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AuthorName", book.Author.AuthorName);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                var author = new Author();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        author.Id = Int32.Parse(reader["Id"].ToString());
                        author.AuthorName = reader["AuthorName"].ToString();
                    }

                    //Code to add book with author.Id
                    string query2 = @"AddBookWithExistingAuthor";

                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.CommandType = System.Data.CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@Title", book.Title);
                    command2.Parameters.AddWithValue("@Author", author.Id);
                    command2.Parameters.AddWithValue("@ISBN", book.ISBN);
                    //add parameter to add genre
                    command2.ExecuteNonQuery();

                }

                else
                {

                    reader.Close();
                    //code to add author and book at the same time
                    SqlTransaction tx = connection.BeginTransaction();

                    string query3 = @"AddAuthor";

                    SqlCommand command3 = new SqlCommand(query3, connection, tx);
                    command3.CommandType = System.Data.CommandType.StoredProcedure;
                    command3.Parameters.AddWithValue(@"AuthorName", book.Author.AuthorName);

                    object id = command3.ExecuteScalar();
                    var intId = Convert.ToInt32(id);

                    //Takes returned id from AddAuthor and inserts into Books.Author
                    string query4 = @"AddBookWithExistingAuthor";
                    SqlCommand command4 = new SqlCommand(query4, connection, tx);
                    command4.CommandType = System.Data.CommandType.StoredProcedure;

                    command4.Parameters.AddWithValue("@Title", book.Title);
                    command4.Parameters.AddWithValue("@Author", intId);
                    command4.Parameters.AddWithValue("@ISBN", book.ISBN);

                    command4.ExecuteNonQuery();

                    tx.Commit();



                }
            }
        }

        public void AddBookToUserShelf(string userName, Book book)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
  
                    string query = @"AddBookToUserShelf";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@bookId", book.Id);

                    command.ExecuteNonQuery();

            }
        }

        public void RemoveBookFromUserShelf(string userName, Book book)
        {
            using (SqlConnection connection =  GetSqlConnection())
            {

                string query = @"RemoveBookFromUserShelf";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@bookId", book.Id);

                command.ExecuteNonQuery();

            }
        }

        //In progress
        public ApplicationUser GetUser(string email)
        {
            using (SqlConnection connection =  GetSqlConnection())
            {
                var user = new ApplicationUser();

                string query = @"GetUser";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", email);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.Id = reader["Id"].ToString();
                        user.UserName = reader["UserName"].ToString();
                        user.Email = reader["Email"].ToString();


                    }
                }

                return user;
            }
        }

        public void CreatePendingFollowRequest(string followerUserName, string followeeUserName)
        {
            using (SqlConnection connection = GetSqlConnection())
            {

                string query = @"CreatePendingFollowRequest";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FollowerUserName", followerUserName);
                command.Parameters.AddWithValue("@FolloweeUserName", followeeUserName);

                command.ExecuteNonQuery();

            }
        }

        public void ConfirmFollower(string followerUserName, string followeeUserName)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
                SqlTransaction tx = connection.BeginTransaction();

                

                string query = @"ConfirmFollower";

                SqlCommand command = new SqlCommand(query, connection, tx);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FollowerUserName", followerUserName);
                command.Parameters.AddWithValue("@FolloweeUserName", followeeUserName);

                command.ExecuteNonQuery();



                string query2 = @"DeleteFromPendingFollows";

                SqlCommand command2 = new SqlCommand(query2, connection, tx);
                command2.CommandType = System.Data.CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@FollowerUserName", followerUserName);
                command2.Parameters.AddWithValue("@FolloweeUserName", followeeUserName);

                command2.ExecuteNonQuery();


                tx.Commit();
            }
        }

       

        public List<ApplicationUser> GetPendingFollowers(string userName)
        {
            using (SqlConnection connection = GetSqlConnection())
            {

                List<ApplicationUser> pendingFollowers = new List<ApplicationUser>();

                string query = @"GetFollowRequests";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FolloweeUserName", userName);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new ApplicationUser();

                        user.Id = reader["Id"].ToString();
                        user.UserName = reader["UserName"].ToString();
                        user.Email = reader["Email"].ToString();

                        pendingFollowers.Add(user);
                    }
                }

                return pendingFollowers;
            }
        }

        public List<ApplicationUser> ListFollowsRequestedByUser(string userName)
        {
            using (SqlConnection connection =  GetSqlConnection())
            {

                List<ApplicationUser> pendingFollowers = new List<ApplicationUser>();

                string query = @"ListFollowsRequestedByUser";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", userName);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new ApplicationUser();

                        user.Id = reader["Id"].ToString();
                        user.UserName = reader["UserName"].ToString();
                        user.Email = reader["Email"].ToString();

                        pendingFollowers.Add(user);
                    }
                }

                return pendingFollowers;
            }
        }

        public List<ApplicationUser> GetUsersIFollow(string userName)
        {
            using (SqlConnection connection = GetSqlConnection())
            {

                List<ApplicationUser> peopleIFollow = new List<ApplicationUser>();

                string query = @"GetUsersIFollow";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FollowerUserName", userName);

                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new ApplicationUser();

                        user.Id = reader["Id"].ToString();
                        user.UserName = reader["UserName"].ToString();
                        user.Email= reader["Email"].ToString();

                        peopleIFollow.Add(user);
                    }
                }

                return peopleIFollow;
            }
        }

        public void UploadProfilePicture(byte[] picture, string userName)
        {
            using (SqlConnection connection = GetSqlConnection())
            {

                string query = @"UploadProfilePhoto";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProfilePicture", picture);
                command.Parameters.AddWithValue("@UserName", userName);

                command.ExecuteNonQuery();

            }
        }
    }
}
