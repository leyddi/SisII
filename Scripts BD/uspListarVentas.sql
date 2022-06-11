USE SistemaComercial
GO

-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 04/06/2022
-- Description:	Listar todas las ventas
-- Example:  EXEC uspListarVentas 
-- =============================================
CREATE PROCEDURE uspListarVentas
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT VEN.ID,ISNULL(VEN.COMPROBANTE,'') AS COMPROBANTE, VEN.FECHAREGISTRO,VEN.TOTAL, CLI.NOMBRES as CLIENTE, USU.NOMBRES AS USUARIO FROM VENTAS VEN WITH(NOLOCK) 
		INNER JOIN CLIENTE CLI WITH(NOLOCK) ON CLI.ID = VEN.ID_CLIENTE
		INNER JOIN USUARIO USU WITH(NOLOCK) ON USU.ID = VEN.ID_USUARIO
END
GO
