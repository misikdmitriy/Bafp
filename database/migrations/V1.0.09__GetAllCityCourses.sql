IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCityCourses'))
	DROP PROCEDURE dbo.GetAllCityCourses;

GO

CREATE PROCEDURE dbo.GetAllCityCourses
	@CityId INT
AS
	BEGIN

	SELECT [CityId] = c.[CityId], [CourseId] = c.[CourseId], Count = c.[Count]
		FROM [dbo].[CityCourses] AS c
		WHERE c.[CityId] = @CityId AND c.[Count] > 0 

	END

GO