USE SistemaComercial
GO

CREATE PROCEDURE uspEliminarUsuario
@ID INT
AS
BEGIN

	DELETE FROM USUARIO 
	WHERE ID = @ID


END
GO