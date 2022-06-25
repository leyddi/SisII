USE SistemaComercial
GO

-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 04/06/2022
-- Description:	Listar todos los productos
-- Example:  EXEC uspListarProductos 
-- =============================================
CREATE PROCEDURE uspListarProductos
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ID, CODIGO, DESCRIPCION,PVP, CANTIDAD, FECHAREGISTRO,MARCA,MODELO FROM PRODUCTOS WITH(NOLOCK)

END
GO
