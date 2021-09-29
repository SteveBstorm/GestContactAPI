CREATE PROCEDURE [dbo].[UpdateContact]
	@Id int,
	@LastName NVARCHAR(75),
	@FirstName NVARCHAR(75),
	@Email NVARCHAR(75),
	@Phone NVarchar(20),
	@BirthDate DateTime2,
	@UserId INT
AS
BEGIN
	UPDATE [Contact] Set [LastName] = @LastName, [FirstName] = @FirstName, [Email] = @Email, [Phone] = @Phone, [BirthDate] = @BirthDate WHERE Id = @Id and UserId = @UserId;
	RETURN 0
END
