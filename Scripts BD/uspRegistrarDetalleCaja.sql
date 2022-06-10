USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Registrar Detalle de caja
-- Example:
-- =============================================
CREATE PROCEDURE uspRegistrarDetalleCaja
@PI_MONTO DECIMAL(6,2),
@PI_ID_MONEDA INT,
@PI_MONTO_BS DECIMAL(6,2),
@PI_TIPO_CAMBIO DECIMAL(6,2),
@PI_ID_CAJA INT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @V_ID_DETALLE_CAJA INT
	INSERT INTO DETALLE_CAJA (ID_MONEDA,MONTO, MONTO_BS, TIPO_CAMBIO,ID_CAJA) VALUES (@PI_ID_MONEDA,@PI_MONTO,@PI_MONTO_BS,@PI_TIPO_CAMBIO, @PI_ID_CAJA)
	SET @V_ID_DETALLE_CAJA = SCOPE_IDENTITY();

	
	COMMIT 
END TRY  
BEGIN CATCH  
   ROLLBACK
END CATCH 
END
GO
