IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetCitiesTotal'))
	DROP PROCEDURE GetCitiesTotal;

GO

CREATE PROCEDURE dbo.GetCitiesTotal
AS
  BEGIN

    SELECT [CityId] = c.[Id], [Total] = SUM(cc.[Count] * cp.[Price])
	FROM [dbo].[Cities] AS c LEFT JOIN [dbo].[CoursePricing] AS cp
		ON c.[CategoryId] = cp.[CategoryId]
	LEFT JOIN [dbo].[CityCourses] AS cc
		ON c.[Id] = cc.[CityId] AND cp.[CourseId] = cc.[CourseId]
	GROUP BY c.[Id]

  END

GO