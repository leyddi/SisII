USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Valida las credenciales
-- Example: DECLARE @PI_OUT INT ; EXEC uspValidarCredencialesLogin 'vendedor', '123456*', @PI_OUT OUT; PRINT @PI_OUT
-- =============================================
CREATE PROCEDURE uspObtenerProducto
@PI_CODIGO varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ID, CODIGO, DESCRIPCION,PVP,CANTIDAD,FECHAREGISTRO,MARCA,MODELO FROM PRODUCTOS WITH(NOLOCK) WHERE CODIGO = @PI_CODIGO
END
GO
