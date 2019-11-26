IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCityCourses'))
	DROP PROCEDURE dbo.GetAllCityCourses;

GO

CREATE PROCEDURE dbo.GetAllCityCourses
	@CityName VARCHAR(255)
AS
	BEGIN

	SELECT CityName = ct.[Name], CourseName = cr.[Name], Count = cc.[Count]
		FROM [dbo].[Cities] AS ct, [dbo].[Courses] AS cr, [dbo].[CityCourses] AS cc
		WHERE ct.[Id] = cc.[CityId] AND cr.[Id] = cc.[CourseId] 
			AND ct.[Name] = @CityName

	END

GO