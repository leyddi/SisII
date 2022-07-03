USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Registrar venta
-- Example:
-- =============================================
CREATE PROCEDURE uspRegistrarCompra
@PI_MONTO DECIMAL(6,2),
@PI_ID_PROVEEDOR INT,
@PI_ID_USUARIO INT,
@PI_COMPROBANTE VARCHAR(50),
@PO_ID INT OUT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO COMPRAS (FECHAREGISTRO, ID_USUARIO, TOTAL, ID_PROVEEDOR, SERIE) VALUES (GETDATE(),@PI_ID_USUARIO,@PI_MONTO,@PI_ID_PROVEEDOR,@PI_COMPROBANTE)
	SET @PO_ID = SCOPE_IDENTITY()
	COMMIT 
END TRY  
BEGIN CATCH  
   ROLLBACK
END CATCH 
END
GO
