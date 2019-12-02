IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetCoursePriceList'))
	DROP PROCEDURE GetCoursePriceList;

GO

IF type_id('[dbo].[IdsList]') IS NULL
	CREATE TYPE [dbo].[IdsList] AS TABLE
		(
			Id INT
		)

GO

CREATE PROCEDURE dbo.GetCoursePriceList
	@CategoryIds AS [dbo].[IdsList] READONLY
AS
  BEGIN

SELECT [CourseId] = pcc.[CourseId], [CategoryId] = pcc.[CategoryId], [Price] = ISNULL(cp.[Price], 0)
	FROM (SELECT [CourseId] = c.[Id], [CourseName] = c.[Name], [CategoryId] = pc.[Id], [CategoryName] = pc.[Name] 
  FROM [dbo].[Courses] AS c, [dbo].[PricingCategories] AS pc) AS pcc
	LEFT JOIN [dbo].[CoursePricing] AS cp
		ON pcc.[CategoryId] = cp.[CategoryId] AND pcc.[CourseId] = cp.[CourseId]
  WHERE NOT EXISTS(SELECT 1 FROM @CategoryIds) OR pcc.[CategoryId] IN (SELECT Id FROM @CategoryIds)

  END

GO