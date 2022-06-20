DROP PROCEDURE insertProduct;
DROP PROCEDURE updateProduct;
DROP PROCEDURE deleteProduct;


GO
CREATE PROCEDURE insertProduct
	@Name VARCHAR(50),
	@Price MONEY,
	@Description VARCHAR(200),
	@ProducerId int
AS
	INSERT INTO Product (Name, Price, Description, ProducerId) VALUES (@Name, @Price, @Description, @ProducerId);



GO
CREATE PROCEDURE updateProduct
	@Name VARCHAR(50),
	@Price MONEY,
	@Description VARCHAR(200),
	@Id int,
	@ProducerId int
AS
	UPDATE Product SET Name=@Name, Price=@Price, Description=@Description, ProducerId=@ProducerId WHERE Id=@Id;



GO
CREATE PROCEDURE deleteProduct
	@Id int
AS
	DELETE FROM Product WHERE Id=@Id
