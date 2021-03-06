USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Registrar Detalle de caja
-- Example: EXEC
-- =============================================
CREATE PROCEDURE uspRegistrarDetalleCaja
@PI_MONTO DECIMAL(6,2),
@PI_ID_MONEDA INT,
@PI_MONTO_BS DECIMAL(6,2),
@PI_TIPO_CAMBIO DECIMAL(6,2),
@PI_ID_VENTA INT,
@PI_ID_PAGO INT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @V_ID_CAJA INT

	SELECT @V_ID_CAJA = ID FROM CAJA WITH(NOLOCK) WHERE FECHA_CIERRE IS NULL;

	DECLARE @V_ID_DETALLE_CAJA INT
	INSERT INTO DETALLE_CAJA (ID_MONEDA,MONTO, MONTO_BS, TIPO_CAMBIO,ID_CAJA) VALUES (@PI_ID_MONEDA,@PI_MONTO,@PI_MONTO_BS,@PI_TIPO_CAMBIO, @V_ID_CAJA)
	SET @V_ID_DETALLE_CAJA = SCOPE_IDENTITY();

	INSERT INTO DETALLE_PAGO (ID_DETALLE_CAJA,ID_PAGO) VALUES (@V_ID_DETALLE_CAJA,@PI_ID_PAGO)
	SELECT 1
	COMMIT 
END TRY  
BEGIN CATCH  
   ROLLBACK
END CATCH 
END
GO
