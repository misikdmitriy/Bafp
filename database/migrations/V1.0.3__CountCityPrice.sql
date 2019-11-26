IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.CountCityPrice'))
	DROP FUNCTION dbo.CountCityPrice;

GO

CREATE FUNCTION dbo.CountCityPrice(@CityName VARCHAR(255)) RETURNS INT
AS
	BEGIN

		DECLARE @sum INT;
		SELECT @sum = SUM(cp.Price * sc.[Count])
			FROM [dbo].PricingCategories AS pc,
				[dbo].Cities AS ct,
				[dbo].Courses AS c,
				[dbo].CityCourses AS sc,
				[dbo].CoursePricing AS cp
			WHERE pc.Id = cp.CategoryId 
						AND pc.Id = ct.CategoryId
						AND ct.CategoryId = cp.CategoryId
						AND c.Id = sc.CourseId 
						AND c.Id = cp.CourseId
						AND sc.CourseId = cp.CourseId
						AND ct.Name = @CityName

		RETURN @sum

	END
GO