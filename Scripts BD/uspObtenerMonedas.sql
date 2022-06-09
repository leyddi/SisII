USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Valida las credenciales
-- Example: EXEC uspObtenerMonedas
-- =============================================
CREATE PROCEDURE uspObtenerMonedas
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ID, DESCRIPCION FROM MONEDA WITH(NOLOCK)

	END
GO
