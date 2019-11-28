IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetCoursePriceListByCourse'))
	DROP PROCEDURE GetCoursePriceListByCourse;

GO

CREATE PROCEDURE dbo.GetCoursePriceListByCourse
	@CourseId VARCHAR(255)
AS
  BEGIN

SELECT [CourseId] = pcc.[CourseId], [CategoryId] = pcc.[CategoryId], [Price] = ISNULL(cp.[Price], 0)
	FROM (SELECT [CourseId] = c.[Id], [CourseName] = c.[Name], [CategoryId] = pc.[Id], [CategoryName] = pc.[Name] 
  FROM [dbo].[Courses] AS c, [dbo].[PricingCategories] AS pc) AS pcc
	LEFT JOIN [dbo].[CoursePricing] AS cp
		ON pcc.[CategoryId] = cp.[CategoryId] AND pcc.[CourseId] = cp.[CourseId]
  WHERE pcc.[CourseId] = @CourseId

  END

GO