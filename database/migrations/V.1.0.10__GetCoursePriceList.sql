IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetCoursePriceList'))
	DROP PROCEDURE GetCoursePriceList;

GO

CREATE PROCEDURE dbo.GetCoursePriceList
	@CategoryName VARCHAR(255)
AS
  BEGIN

SELECT [CourseName] = pcc.[CourseName], [CategoryName] = pcc.[CategoryName], [Price] = ISNULL(cp.[Price], 0)
	FROM (SELECT [CourseId] = c.[Id], [CourseName] = c.[Name], [CategoryId] = pc.[Id], [CategoryName] = pc.[Name] 
  FROM [dbo].[Courses] AS c, [dbo].[PricingCategories] AS pc) AS pcc
	LEFT JOIN [dbo].[CoursePricing] AS cp
		ON pcc.[CategoryId] = cp.[CategoryId] AND pcc.[CourseId] = cp.[CourseId]
  WHERE pcc.[CategoryName] = @CategoryName

  END

GO