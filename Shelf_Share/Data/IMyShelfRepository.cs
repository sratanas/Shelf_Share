﻿using Shelf_Share.Models;
using System.Collections.Generic;

namespace Shelf_Share.Data
{
    public interface IMyShelfRepository
    {
        List<Book> GetUserShelf(string userId);
        List<Book> GetBooksByAuthor(string authorName);
        List<Book> GetBooksByTitle(string title);
        Book GetBookById(int id);
        void AddBookToShelfShare(Book book);
        void AddBookToUserShelf(string userName, Book book);
        void RemoveBookFromUserShelf(string userName, Book book);
        ApplicationUser GetUser(string email);
        void CreatePendingFollowRequest(string followerUserName, string followeeUserName);
        void ConfirmFollower(string followerUserName, string followeeUserName);
        List<ApplicationUser> GetUsersIFollow(string userName);
        List<ApplicationUser> GetPendingFollowers(string userName);
        List<ApplicationUser> ListFollowsRequestedByUser(string userName);
        void UploadProfilePicture(byte[] picture, string userName);

    }
}