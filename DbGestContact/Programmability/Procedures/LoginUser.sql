CREATE PROCEDURE [dbo].[LoginUser]
	@Email NVARCHAR(384),
	@Passwd NVARCHAR(20)
AS
BEGIN
	SELECT [Id], LastName, FirstName, Email
	FROM [User] 
	WHERE Email = @Email
	AND Passwd = HASHBYTES('SHA2_512', dbo.GetPreSalt() + @Passwd + dbo.GetPostSalt());
	RETURN 0;
END
