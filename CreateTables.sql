﻿CREATE TABLE Books (
Id INT IDENTITY PRIMARY KEY,
Title VARCHAR(200),
ISBN VARCHAR(25),
);

CREATE TABLE Authors(
Id INT IDENTITY PRIMARY KEY,
LastName VARCHAR(50),
FirstName VARCHAR(50),
)

CREATE TABLE Locations(
Id INT IDENTITY PRIMARY KEY,
Name VARCHAR(100)
)

CREATE TABLE Genres(
Id INT IDENTITY PRIMARY KEY,
Name VARCHAR(80)
)

ALTER TABLE Books
ADD Author INT REFERENCES Authors(Id)

ALTER TABLE Books
ADD Genre INT REFERENCES Genres(Id)


CREATE TABLE Users_Books (
Id INT IDENTITY PRIMARY KEY,
User_Id nvarchar(450) REFERENCES AspNetUsers(Id),
Book_Id INT REFERENCES Books(Id)
)