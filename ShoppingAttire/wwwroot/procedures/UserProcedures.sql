DROP PROCEDURE deleteUser;

GO
CREATE PROCEDURE deleteUser
	@Id int	
AS
	DELETE FROM [dbo].[User] WHERE Id=@Id

GO
CREATE PROCEDURE updateUser
	@Id int,
	@UserName NVARCHAR (50),
	@Password NVARCHAR (150),
	@Role  NVARCHAR (20)

	AS
	UPDATE [dbo].[User] SET UserName=@UserName, LongName=@LongName, Description=@Description WHERE Id=@Id
