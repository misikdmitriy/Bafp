IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetCoursePriceList'))
	DROP PROCEDURE GetCoursePriceList;

GO

CREATE PROCEDURE dbo.GetCoursePriceList
	@CategoryId VARCHAR(255)
AS
  BEGIN

SELECT [CourseId] = pcc.[CourseId], [CategoryId] = pcc.[CategoryId], [Price] = ISNULL(cp.[Price], 0)
	FROM (SELECT [CourseId] = c.[Id], [CourseName] = c.[Name], [CategoryId] = pc.[Id], [CategoryName] = pc.[Name] 
  FROM [dbo].[Courses] AS c, [dbo].[PricingCategories] AS pc) AS pcc
	LEFT JOIN [dbo].[CoursePricing] AS cp
		ON pcc.[CategoryId] = cp.[CategoryId] AND pcc.[CourseId] = cp.[CourseId]
  WHERE pcc.[CategoryId] = @CategoryId

  END

GO