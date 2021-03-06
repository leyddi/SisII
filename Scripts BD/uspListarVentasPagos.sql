USE SistemaComercial
GO

-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 04/06/2022
-- Description:	Listar todas las ventas a detalle
-- Example:  EXEC uspListarVentas 
-- =============================================
 CREATE PROCEDURE uspListarVentasPagos
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT VEN.ID,ISNULL(VEN.COMPROBANTE,'') AS COMPROBANTE, VEN.FECHAREGISTRO, CLI.NOMBRES AS CLIENTE, USU.NOMBRES AS VENDEDOR,VEN.TOTAL, MON.DESCRIPCION, DEJ.MONTO, DEJ.TIPO_CAMBIO, DEJ.MONTO_BS,
		CASE WHEN (DEJ.MONTO_BS<0) THEN 'Vuelto' ELSE 'Ingreso' END AS TIPO
		FROM VENTAS VEN WITH(NOLOCK) 
		INNER JOIN CLIENTE CLI WITH(NOLOCK) ON CLI.ID = VEN.ID_CLIENTE
		INNER JOIN USUARIO USU WITH(NOLOCK) ON USU.ID = VEN.ID_USUARIO
		INNER JOIN DETALLE_PAGO DEP WITH(NOLOCK) ON DEP.ID_PAGO = VEN.ID_PAGO
		INNER JOIN DETALLE_CAJA DEJ WITH(NOLOCK) ON DEJ.ID = DEP.ID_DETALLE_CAJA
		INNER JOIN MONEDA MON WITH(NOLOCK) ON MON.ID = DEJ.ID_MONEDA
		ORDER BY VEN.FECHAREGISTRO DESC
END
GO
