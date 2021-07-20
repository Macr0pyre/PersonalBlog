create database PersonalBlog 

use PersonalBlog
create table Users
(
ID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
Login nvarchar(max) NOT NULL,
PasswordHash nvarchar(max) NOT NULL,
PageID int NOT NULL
)
create table Pages
(
ID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Pages PRIMARY KEY,
Name varchar(255) NOT NULL
)
create table Tags
(
ID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Tags PRIMARY KEY,
Name varchar(255) NOT NULL
)
create table Articles
(
ID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Articles PRIMARY KEY,
Title varchar(255) NOT NULL,
Text nvarchar(max) NOT NULL,
CreationDate datetime NOT NULL,
PageID int NOT NULL
)
create table ArticlesTags
(
ID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ArticlesTags PRIMARY KEY,
ArticleID int NOT NULL,
TagID int NOT NULL
)
create table Roles
(
ID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Roles PRIMARY KEY,
Name varchar(255) NOT NULL
)

ALTER TABLE Users 
ADD CONSTRAINT FK_Users_PageID FOREIGN KEY(PageID) REFERENCES Pages(ID)

ALTER TABLE Articles 
ADD CONSTRAINT FK_Articles_PageID FOREIGN KEY(PageID) REFERENCES Pages(ID)

ALTER TABLE ArticlesTags 
ADD CONSTRAINT FK_ArticlesTags_ArticleID FOREIGN KEY(ArticleID) REFERENCES Articles(ID),
CONSTRAINT FK_ArticlesTags_TagID FOREIGN KEY(TagID) REFERENCES Tags(ID)