USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 09/06/2022
-- Description:	Registrar Cliente
-- Example: declare @PI_ID_CLIENTE int; EXEC uspRegistrarCliente '1', '21219922', 'Leyddi Borjas', @PI_ID_CLIENTE out; print @PI_ID_CLIENTE
-- =============================================
ALTER PROCEDURE uspRegistrarProveedor
@PI_TIPO_DOCUMENTO INT,
@PI_NUMERO_DOCUMENTO VARCHAR(50),
@PI_DESCRIPCION VARCHAR(100),
@PI_ID_PROVEEDOR INT OUT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO PROVEEDOR(DESCRIPCION, NUMERODOCUMENTO, ID_TIPODOCUMENTO, DIRECCION) VALUES(@PI_DESCRIPCION,@PI_NUMERO_DOCUMENTO,@PI_TIPO_DOCUMENTO,'')
	SET @PI_ID_PROVEEDOR = SCOPE_IDENTITY();

	COMMIT 
END TRY  
BEGIN CATCH  
   ROLLBACK
END CATCH 
END
GO
