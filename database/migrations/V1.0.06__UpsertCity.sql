IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.UpsertCity'))
	DROP PROCEDURE dbo.UpsertCity;

GO

CREATE PROCEDURE dbo.UpsertCity
	@Name VARCHAR(255),
	@CategoryId VARCHAR(255)
AS
	BEGIN TRY

	IF NOT EXISTS(SELECT * FROM [dbo].[PricingCategories] WHERE [Id] = @CategoryId)
		RAISERROR ('Category %d absent in DB', 16, 1, @CategoryId)

	INSERT INTO [dbo].[Cities]([Name], [CategoryId])
		SELECT [Name] = @Name, [CategoryId] = @CategoryId

	END TRY

	BEGIN CATCH
		IF ERROR_NUMBER() IN (2601, 2627) 
			UPDATE [dbo].[Cities] SET [CategoryId] = @CategoryId
				WHERE [Name] = @Name
	END CATCH

GO