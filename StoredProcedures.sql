USE [ShelfShare]
GO
/****** Object:  StoredProcedure [dbo].[AddAuthor]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAuthor]

@AuthorName NVARCHAR(256)

AS

INSERT INTO Authors (AuthorName)
VALUES (@AuthorName)
SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[AddBookToUserShelf]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddBookToUserShelf]

@userName NVARCHAR(256),
@bookId INT

AS
--SELECT * FROM Users_Books WHERE User_Id=(SELECT Id FROM AspNetUsers WHERE UserName=@userName)
--AND Book_Id=@bookId

IF NOT EXISTS(SELECT * FROM Users_Books WHERE User_Id=(SELECT Id FROM AspNetUsers WHERE UserName=@userName)
AND Book_Id=@bookId)

INSERT INTO Users_Books (User_Id, Book_Id)
VALUES((SELECT Id FROM AspNetUsers WHERE UserName=@userName),@bookId)
GO
/****** Object:  StoredProcedure [dbo].[AddBookWithExistingAuthor]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddBookWithExistingAuthor]

@Title VARCHAR(256),
@Author INT,
@ISBN VARCHAR(25)

AS

INSERT INTO Books (Title, Author, ISBN)
VALUES (@Title, @Author, @ISBN)
GO
/****** Object:  StoredProcedure [dbo].[CheckDatabaseForAuthor]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckDatabaseForAuthor]

@AuthorName VARCHAR(256)

AS

SELECT Id, AuthorName from Authors
WHERE AuthorName LIKE @AuthorName
GO
/****** Object:  StoredProcedure [dbo].[ConfirmFollower]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConfirmFollower]

@FollowerUserName NVARCHAR(256),
@FolloweeUserName NVARCHAR(450)

AS
BEGIN

IF NOT EXISTS(SELECT * FROM Follows WHERE Follower = (SELECT Id FROM AspNetUsers WHERE UserName=@FollowerUserName)
AND Followee=(SELECT Id FROM AspNetUsers WHERE UserName=@FolloweeUserName))

INSERT INTO Follows (Follower, Followee)
VALUES((SELECT Id FROM AspNetUsers WHERE UserName=@FollowerUserName),(SELECT Id FROM AspNetUsers WHERE UserName=@FolloweeUserName))

DELETE FROM PendingFollowRequests
WHERE (SELECT Id FROM AspNetUsers WHERE UserName=@FollowerUserName)=Follower AND (SELECT Id FROM AspNetUsers WHERE UserName=@FolloweeUserName)= Followee

END
GO
/****** Object:  StoredProcedure [dbo].[CreatePendingFollowRequest]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreatePendingFollowRequest]

@FollowerUserName NVARCHAR(256),
@FolloweeUserName NVARCHAR(450)

AS

IF NOT EXISTS(SELECT * FROM PendingFollowRequests WHERE Follower = (SELECT Id FROM AspNetUsers WHERE UserName=@FollowerUserName)
AND Followee=(SELECT Id FROM AspNetUsers WHERE UserName=@FolloweeUserName))

INSERT INTO PendingFollowRequests (Follower, Followee)
VALUES((SELECT Id FROM AspNetUsers WHERE UserName=@FollowerUserName),(SELECT Id FROM AspNetUsers WHERE UserName=@FolloweeUserName))

GO
/****** Object:  StoredProcedure [dbo].[DeleteFromPendingFollows]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteFromPendingFollows]

@FollowerUserName NVARCHAR(256),
@FolloweeUserName NVARCHAR(450)
AS
DELETE FROM PendingFollowRequests
WHERE (SELECT Id FROM AspNetUsers WHERE UserName=@FollowerUserName)=Follower AND (SELECT Id FROM AspNetUsers WHERE UserName=@FolloweeUserName)= Followee
GO
/****** Object:  StoredProcedure [dbo].[GetBookById]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBookById]

@Id INT

AS

SELECT b.Id, a.Id, g.Id, b.Title, g.GenreName, a.AuthorName, b.ISBN FROM Books as b
JOIN Authors as a ON a.Id = b.Author
FULL JOIN Genres as g ON g.Id = b.Genre
WHERE b.Id = @Id

GO
/****** Object:  StoredProcedure [dbo].[GetBooksByAuthor]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBooksByAuthor]

@AuthorName VARCHAR(256)

AS

SELECT b.Id, b.Title, b.ISBN, a.Id, g.Id, a.AuthorName, g.GenreName FROM Books as b
JOIN Authors as a ON b.Author = a.Id
FULL JOIN Genres as g ON g.Id= b.Genre
WHERE a.AuthorName LIKE @AuthorName
GO
/****** Object:  StoredProcedure [dbo].[GetBooksByTitle]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBooksByTitle]

@TitleSearchParam NVARCHAR(256)

AS

SELECT b.Id, a.Id, g.Id, b.Title, g.GenreName, a.AuthorName, b.ISBN FROM Books as b
FULL JOIN Authors as a ON a.Id = b.Author
FULL JOIN Genres as g ON g.Id = b.Genre
WHERE b.Title LIKE '%'+@TitleSearchParam+'%'
--WHERE FREETEXT(b.Title, @TitleSearchParam) OR b.Title LIKE '%'+@TitleSearchParam+'%'


GO
/****** Object:  StoredProcedure [dbo].[GetFollowRequests]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFollowRequests]

@FolloweeUserName NVARCHAR(256)

AS

SELECT u.Id, u.UserName, u.Email FROM AspNetUsers as u
JOIN PendingFollowRequests as f on u.Id = f.Follower
WHERE f.Followee = (SELECT Id FROM AspNetUsers WHERE UserName=@FolloweeUsername)
ORDER BY u.UserName
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUser]

@Email NVARCHAR(256)

AS

SELECT Id, Email, UserName FROM AspNetUsers
WHERE Email = @Email
GO
/****** Object:  StoredProcedure [dbo].[GetUserShelf]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserShelf]

@userName NVARCHAR(256)

AS

SELECT b.Id, b.Title, u.UserName, a.AuthorName, g.GenreName FROM Books as b
JOIN Users_Books as ub on ub.Book_Id = b.Id
JOIN AspNetUsers as u on ub.User_Id = u.Id
JOIN Authors as a on a.Id = b.Author
FULL JOIN Genres as g on g.Id = b.Genre
WHERE u.UserName = @userName
ORDER BY b.Title
GO
/****** Object:  StoredProcedure [dbo].[GetUsersIFollow]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsersIFollow]
@FollowerUserName NVARCHAR(256)

AS

SELECT u.Id, u.UserName, u.Email FROM AspNetUsers as u
JOIN Follows as f on u.Id = f.Followee
WHERE f.Follower = (SELECT Id FROM AspNetUsers WHERE UserName=@FollowerUsername)
ORDER BY u.UserName

GO
/****** Object:  StoredProcedure [dbo].[ListFollowsRequestedByUser]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ListFollowsRequestedByUser]

@UserName NVARCHAR(256)

AS

SELECT u.Id, u.UserName, u.Email FROM AspNetUsers as u
JOIN PendingFollowRequests as f on u.Id = f.Followee
WHERE f.Follower = (SELECT Id FROM AspNetUsers WHERE UserName=@UserName)
ORDER BY u.UserName
GO
/****** Object:  StoredProcedure [dbo].[RemoveBookFromUserShelf]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveBookFromUserShelf]

@userName NVARCHAR(256),
@bookId INT

AS

IF EXISTS(SELECT * FROM Users_Books WHERE User_Id=(SELECT Id FROM AspNetUsers WHERE UserName=@userName)
AND Book_Id=@bookId)

DELETE FROM Users_Books 
WHERE User_Id=(SELECT Id FROM AspNetUsers WHERE UserName=@userName) AND Book_Id=@bookId
GO
/****** Object:  StoredProcedure [dbo].[UploadProfilePhoto]    Script Date: 7/19/2018 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UploadProfilePhoto]

@ProfilePhoto varbinary(max),
@UserName varchar(512)

AS

UPDATE AspNetUsers SET ProfilePhoto=@ProfilePhoto
WHERE UserName=@UserName

GO
