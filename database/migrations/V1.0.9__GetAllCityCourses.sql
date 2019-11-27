IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCityCourses'))
	DROP PROCEDURE dbo.GetAllCityCourses;

GO

CREATE PROCEDURE dbo.GetAllCityCourses
	@CityId INT
AS
	BEGIN

	SELECT [CityId] = ct.Id, [CityName] = ct.[Name], [CourseId] = cr.[Id], CourseName = cr.[Name], Count = cc.[Count]
		FROM [dbo].[Cities] AS ct, [dbo].[Courses] AS cr, [dbo].[CityCourses] AS cc
		WHERE ct.[Id] = cc.[CityId] AND cr.[Id] = cc.[CourseId] 
			AND ct.[Id] = @CityId

	END

GO