CREATE PROCEDURE [dbo].[AddContact]
	@LastName NVARCHAR(75),
	@FirstName NVARCHAR(75),
	@Email NVARCHAR(75),
	@Phone NVARCHAR(20),
	@BirthDate DateTime2,
	@UserId int
AS
BEGIN
	INSERT INTO [Contact] ([LastName], [FirstName], [Email],[Phone], [BirthDate], [UserId]) VALUES (@LastName, @FirstName, @Email, @Phone, @BirthDate, @UserId);
	RETURN 0
END
