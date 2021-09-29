CREATE PROCEDURE [dbo].[DeleteContact]
	@Id int
AS
BEGIN
	Delete From Contact Where Id = @Id
	RETURN 0
END

