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
-- Example: DECLARE @PI_OUT INT ; EXEC uspValidarCredencialesLogin 'vendedor', '123456*', @PI_OUT OUT; PRINT @PI_OUT
-- =============================================
ALTER PROCEDURE uspValidarCredencialesLogin
@PI_Usuario varchar(50),
@PI_Contrasena varchar(50),
@PI_IdUser int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @PI_IdUser = ISNULL( (SELECT top 1 ID FROM USUARIO WITH(NOLOCK) WHERE upper(NOMBREUSUARIO) = upper(@PI_Usuario) AND CONSTRASENA= @PI_Contrasena),0)
END
GO
