USE SistemaComercial
GO


CREATE PROCEDURE uspObtenerProductoId
@ID int
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM PRODUCTOS 
	WHERE ID = @ID 
END
GO