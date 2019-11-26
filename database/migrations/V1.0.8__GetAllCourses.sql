IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCourses'))
	DROP PROCEDURE dbo.GetAllCourses;

GO

CREATE PROCEDURE dbo.GetAllCourses
AS
	BEGIN

	SELECT [Id], [Name]
		FROM [dbo].[Courses]

	END

GO