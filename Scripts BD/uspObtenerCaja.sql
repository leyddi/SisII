USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 23/06/2022
-- Description:	Visualizar detalle de caja
-- Example: EXEC uspObtenerCaja
-- =============================================
CREATE PROCEDURE uspObtenerCaja
AS
BEGIN	


	SELECT FECHA_APERTURA,CAJ.SALDO_INICIAL,MON.DESCRIPCION, SUM(MONTO) AS TOTAL
	FROM CAJA CAJ WITH(NOLOCK) 
	INNER JOIN DETALLE_CAJA DCA WITH(NOLOCK) ON CAJ.ID = DCA.ID_CAJA
	INNER JOIN MONEDA MON WITH(NOLOCK) ON MON.ID = DCA.ID_MONEDA
	WHERE CAJ.FECHA_CIERRE IS NULL
	GROUP BY FECHA_APERTURA,CAJ.SALDO_INICIAL, MON.DESCRIPCION
END