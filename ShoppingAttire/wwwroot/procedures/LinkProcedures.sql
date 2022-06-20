DROP PROCEDURE establishLink;
DROP PROCEDURE purgeLink;
DROP PROCEDURE wipeProductLinks;


GO
CREATE PROCEDURE establishLink
	@CategoryId int,
	@ProductId int
	
	
AS
	INSERT INTO CategoryProduct (CategoryId, ProductId) VALUES (@CategoryId, @ProductId)
	
GO
CREATE PROCEDURE purgeLink
	@CategoryId int,
	@ProductId int
AS
	DELETE FROM CategoryProduct WHERE CategoryId=@CategoryId AND ProductId=@ProductId

GO
CREATE PROCEDURE wipeProductLinks
	@ProductId int
AS
	DELETE FROM CategoryProduct WHERE ProductId=@ProductId
