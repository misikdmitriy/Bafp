IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.UpsertCoursePricing'))
	DROP PROCEDURE dbo.UpsertCoursePricing;

GO

CREATE PROCEDURE dbo.UpsertCoursePricing
	@CourseId INT,
	@CategoryId INT,
	@Price SMALLMONEY
AS
	BEGIN TRY

	IF NOT EXISTS(SELECT * FROM [dbo].[PricingCategories] WHERE [Id] = @CategoryId)
		RAISERROR ('Category %d absent in DB', 16, 1, @CategoryId)

	IF NOT EXISTS(SELECT * FROM [dbo].[Courses] WHERE [Id] = @CourseId)
		RAISERROR ('Course %d absent in DB', 16, 1, @CourseId)

	INSERT INTO [dbo].[CoursePricing](CourseId, CategoryId, Price)
	OUTPUT INSERTED.*
		VALUES(@CourseId, @CategoryId, @Price)

	END TRY

	BEGIN CATCH
		IF ERROR_NUMBER() IN (2601, 2627) 
			UPDATE [dbo].[CoursePricing] SET [Price] = @Price
			OUTPUT INSERTED.*
				WHERE CourseId = @CourseId AND CategoryId = @CategoryId
	END CATCH

GO