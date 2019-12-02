IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetCityCourses'))
	DROP PROCEDURE GetCityCourses;

GO

CREATE PROCEDURE dbo.GetCityCourses
AS
  BEGIN

    SELECT [CityId], [CourseId], [Count]
	    FROM [dbo].[CityCourses]
      WHERE [Count] > 0

  END

GO