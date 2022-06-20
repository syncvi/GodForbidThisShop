DROP PROCEDURE insertCategory;
DROP PROCEDURE updateCategory;
DROP PROCEDURE deleteCategory;


GO
CREATE PROCEDURE [dbo].[insertCategory]
	@ShortName VARCHAR(50),
	@LongName VARCHAR(100),
	@Description VARCHAR(200)

	AS
	INSERT INTO Category (ShortName, LongName, Description) VALUES (@ShortName, @LongName, @Description)


GO
CREATE PROCEDURE [dbo].[deleteCategory]
	@Id int

	AS
	DELETE FROM Category WHERE Id=@Id


GO
CREATE PROCEDURE [dbo].[updateCategory]
	@ShortName VARCHAR(50),
	@longName VARCHAR(100),
	@Description VARCHAR(200),
	@Id int

	AS
	UPDATE Category SET ShortName=@ShortName, LongName=@LongName, Description=@Description WHERE Id=@Id
