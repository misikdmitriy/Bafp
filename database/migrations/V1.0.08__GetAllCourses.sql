IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCourses'))
	DROP PROCEDURE dbo.GetAllCourses;

GO

IF type_id('[dbo].[IdsList]') IS NULL
	CREATE TYPE [dbo].[IdsList] AS TABLE
		(
			Id INT
		)

GO

CREATE PROCEDURE dbo.GetAllCourses
	@CourseIds AS [dbo].[IdsList] READONLY
AS
	BEGIN

	SELECT [Id], [Name]
		FROM [dbo].[Courses] AS c
		WHERE NOT EXISTS(SELECT 1 FROM @CourseIds) OR c.[Id] IN (SELECT Id FROM @CourseIds)

	END

GO