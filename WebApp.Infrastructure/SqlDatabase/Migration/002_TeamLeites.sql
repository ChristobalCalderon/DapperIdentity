CREATE TABLE [dbo].[Event]
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	EventType int NOT NULL,
    Describition varchar(255) NOT NULL,
    Date Datetime NOT NULL,
	Location varchar(255) NOT NULL
)

CREATE TABLE [dbo].[Diary]
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	UserId nvarchar(255) NOT NULL,
    Entry varchar(255) NOT NULL,
    Timestamp Datetime NOT NULL,
	Subject varchar(255) NOT NULL
)


GO

CREATE INDEX [IX_Diary_UserId] ON [dbo].[Diary] ([UserId])

CREATE TABLE Autogiro (
    ID int IDENTITY(1,1) PRIMARY KEY,
	Reporter nvarchar(255) NOT NULL,
	Email varchar(255) NOT NULL,
    FirstName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
	PersonalIdentityNumber varchar(255) NOT NULL,
    Bank varchar(255) NOT NULL,
	ClearingNumber int NOT NULL,
    AccountNunmber varchar(255) NOT NULL,
	Debtor varchar(255) NOT NULL,
    TimeStamp Datetime NOT NULL,
	Location varchar(255) NOT NULL,
	Signature nvarchar(max) NOT NULL,
	PostalCode varchar(255) NOT NULL,
	Street varchar(255) NOT NULL,
    PostalAddress varchar(255) NOT NULL,
);

CREATE TABLE Attendant (
    ID int IDENTITY(1,1) PRIMARY KEY,
    UserId nvarchar(255) NOT NULL,
    Location varchar(255) NOT NULL,
    Date Datetime NOT NULL,
	Attend bit NOT NULL
);

GO

CREATE INDEX [IX_Attendant_Username] ON [dbo].[Attendant] (UserId)
