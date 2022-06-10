USE SistemaComercial
GO
--==============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Obtener Cliente
-- Example: EXEC uspObtenerCliente '1', '21219922'
-- =============================================
CREATE PROCEDURE uspObtenerCliente
@PI_TIPO_DOCUMENTO int,
@PI_NUMERO_DOCUMENTO VARCHAR(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ID, NOMBRES, NUMERODOCUMENTO, ID_TIPODOCUMENTO 
	FROM CLIENTE WITH(NOLOCK) 
	WHERE NUMERODOCUMENTO = @PI_NUMERO_DOCUMENTO AND ID_TIPODOCUMENTO = @PI_TIPO_DOCUMENTO

END
GO
