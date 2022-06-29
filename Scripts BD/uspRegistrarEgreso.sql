USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Registrar Egreso
-- Example: EXEC
-- =============================================
CREATE PROCEDURE uspRegistrarEgreso
@PI_MONTO DECIMAL(6,2),
@PI_ID_MONEDA INT,
@PI_MONTO_BS DECIMAL(6,2),
@PI_TIPO_CAMBIO DECIMAL(6,2),
@PI_TIPO VARCHAR(200),
@PI_ID INT OUT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @V_ID_CAJA INT

	SELECT @V_ID_CAJA = ID FROM CAJA WITH(NOLOCK) WHERE FECHA_CIERRE IS NULL;

	INSERT INTO DETALLE_CAJA (ID_MONEDA,MONTO, MONTO_BS, TIPO_CAMBIO,ID_CAJA, TIPO) VALUES (@PI_ID_MONEDA,@PI_MONTO,@PI_MONTO_BS,@PI_TIPO_CAMBIO, @V_ID_CAJA, @PI_TIPO)
	SET @PI_ID = SCOPE_IDENTITY();

	COMMIT 
END TRY  
BEGIN CATCH  
SET @PI_ID = -1
   ROLLBACK
END CATCH 
END
GO
