USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Registrar Detalle de venta
-- Example:
-- =============================================
CREATE PROCEDURE uspRegistrarDetalleVenta
@PI_CANTIDAD DECIMAL(6,2),
@PI_ID_PRODUCTO INT,
@PI_ID_VENTAS INT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	DECLARE @V_PVP DECIMAL(6,2)
	SELECT @V_PVP = PVP FROM PRODUCTOS WITH(NOLOCK) WHERE ID = @PI_ID_PRODUCTO;

	INSERT INTO DETALLE_VENTAS(CANTIDAD, ID_PRODUCTO, ID_VENTAS,PVP, PVP_TOTAL) VALUES(@PI_CANTIDAD,@PI_ID_PRODUCTO,@PI_ID_VENTAS,@V_PVP,@V_PVP*@PI_CANTIDAD)

	UPDATE PRODUCTOS SET CANTIDAD = CANTIDAD - @PI_CANTIDAD WHERE ID = @PI_ID_PRODUCTO
	
	COMMIT 
END TRY  
BEGIN CATCH  
   ROLLBACK
END CATCH 
END
GO
