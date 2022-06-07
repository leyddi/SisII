USE SistemaComercial
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 04/06/2022
-- Description:	Valida las credenciales
-- Example: DECLARE @PI_OUT INT ; EXEC uspValidarCredencialesLogin 'vendedor', '123456', @PI_OUT OUT; PRINT @PI_OUT
-- =============================================
CREATE PROCEDURE uspObtenerUsuario
@PI_Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT US.ID, NOMBREUSUARIO, NOMBRES, APELLIDOS, FECHAREGISTRO, ID_ROL, RO.DESCRIPCION AS ROL FROM USUARIO US WITH(NOLOCK)
	INNER JOIN ROL RO WITH(NOLOCK) ON RO.ID = US.ID
	WHERE US.ID = @PI_Id

END
GO
