IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.UpsertCoursePricing'))
	DROP PROCEDURE UpsertCoursePricing;

GO

CREATE PROCEDURE dbo.UpsertCoursePricing
	@CategoryId INT,
	@CourseId INT,
	@Price INT
AS
	BEGIN TRY

	IF NOT EXISTS(SELECT * FROM [dbo].[Courses] WHERE [Id] = @CourseId)
		RAISERROR ('Course %d absent in DB', 16, 1, @CourseId)

	IF NOT EXISTS(SELECT * FROM [dbo].[PricingCategories] WHERE [Id] = @CategoryId)
		RAISERROR ('Category %d absent in DB', 16, 1, @CategoryId)
	
	IF @Price <= 0
		RAISERROR ('Price cannot be negative', 16, 1)

    INSERT INTO [dbo].[CoursePricing](CategoryId, CourseId, [Price])
        VALUES(@CategoryId, @CourseId, @Price)

	END TRY

	BEGIN CATCH
		IF ERROR_NUMBER() IN (2601, 2627) 
			UPDATE [dbo].[CoursePricing] SET [Price] = @Price
				WHERE  [CategoryId] = @CategoryId AND [CourseId] = @CourseId
	END CATCH

GO