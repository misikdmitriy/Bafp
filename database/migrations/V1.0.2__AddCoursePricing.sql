IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.AddCoursePricing'))
	DROP PROCEDURE dbo.AddCoursePricing;

GO

CREATE PROCEDURE dbo.AddCoursePricing
	@CourseName VARCHAR(255),
	@CategoryName VARCHAR(255),
	@Price SMALLMONEY
AS
	BEGIN

	INSERT INTO [dbo].[CoursePricing](CourseId, CategoryId, Price)
		SELECT CourseId = cr.Id, CategoryId = pc.Id, [Count] = @Price
		FROM [dbo].[Courses] AS cr, [dbo].[PricingCategories] AS pc
		WHERE cr.Name = @CourseName AND pc.Name = @CategoryName

	END

GO