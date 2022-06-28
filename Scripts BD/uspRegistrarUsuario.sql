USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 04/06/2022
-- Description:	Registrar Usuario nuevo
-- Example: EXEC uspRegistrarUsuario
-- =============================================
CREATE PROCEDURE uspRegistrarUsuario
@PI_USUARIO VARCHAR(50),
@PI_CONTRASENA VARCHAR(50),
@PI_NOMBRES VARCHAR(100),
@PI_APELLIDOS VARCHAR(100),
@PI_ID_ROL INT,
@PI_ID INT OUT
AS
BEGIN	
	BEGIN TRY  
    BEGIN TRAN	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO USUARIO(NOMBREUSUARIO, CONSTRASENA, NOMBRES, APELLIDOS, FECHAREGISTRO, ID_ROL) VALUES(@PI_USUARIO,@PI_CONTRASENA,@PI_NOMBRES,@PI_APELLIDOS,GETDATE(),@PI_ID_ROL)
	SET @PI_ID = SCOPE_IDENTITY()

	COMMIT 
END TRY  
BEGIN CATCH  
SET @PI_ID = -1
   ROLLBACK
END CATCH 
END
GO
