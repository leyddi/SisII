USE SistemaComercial
GO

CREATE PROCEDURE uspEliminarProducto
@ID INT
AS
BEGIN

	DELETE FROM PRODUCTOS 
	WHERE ID = @ID


END
GO