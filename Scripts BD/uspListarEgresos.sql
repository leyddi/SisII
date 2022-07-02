USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Listar egresos
-- Example: EXEC
-- =============================================
CREATE PROCEDURE uspListarEgresos
AS
BEGIN	
	SELECT CAJ.ID, FECHA_APERTURA, FECHA_CIERRE, MONTO, MONTO_BS, TIPO_CAMBIO, DET.APERTURA, DET.TIPO from CAJA CAJ WITH(NOLOCK)
	INNER JOIN DETALLE_CAJA DET WITH(NOLOCK) ON CAJ.ID =  DET.ID_CAJA 
	LEFT JOIN DETALLE_PAGO DEP WITH(NOLOCK) ON DEP.ID_DETALLE_CAJA = DET.ID
	WHERE DEP.ID IS NULL AND MONTO_BS<0
END
GO