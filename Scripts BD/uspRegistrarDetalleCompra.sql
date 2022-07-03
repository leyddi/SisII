USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Registrar Detalle de venta
-- Example:
-- =============================================
CREATE PROCEDURE uspRegistrarDetalleCompra
@PI_CANTIDAD DECIMAL(6,2),
@PI_PVP DECIMAL(6,2),
@PI_ID_PRODUCTO INT,
@PI_ID_COMPRA INT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO DETALLE_COMPRAS(CANTIDAD, ID_PRODUCTO, ID_COMPRAS,PVP, PVP_TOTAL) VALUES(@PI_CANTIDAD,@PI_ID_PRODUCTO,@PI_ID_COMPRA,@PI_PVP,@PI_PVP*@PI_CANTIDAD)

	UPDATE PRODUCTOS SET CANTIDAD = CANTIDAD + @PI_CANTIDAD, PVP = @PI_PVP WHERE ID = @PI_ID_PRODUCTO
	
	COMMIT 
END TRY  
BEGIN CATCH  
   ROLLBACK
END CATCH 
END
GO
