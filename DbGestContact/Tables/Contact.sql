CREATE TABLE [dbo].[Contact]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](75) NOT NULL,
	[FirstName] [nvarchar](75) NOT NULL,
	[Email] [nvarchar](384) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[BirthDate] [DateTime2] NOT NULL,
	[UserId] INT NOT NULL, 
    CONSTRAINT [PK_Contact] PRIMARY KEY (Id),
	CONSTRAINT [FK_Contact_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])
)
