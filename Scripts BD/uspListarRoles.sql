USE SistemaComercial
GO
-- =============================================
-- Author:		Leyddi Borjas
-- Create date: 04/06/2022
-- Description:	Registrar Roles
-- Example: EXEC uspListarRoles
-- =============================================
CREATE PROCEDURE uspListarRoles
AS
BEGIN	
	
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ID, DESCRIPCION FROM ROL WITH(NOLOCK)
	
END
GO
