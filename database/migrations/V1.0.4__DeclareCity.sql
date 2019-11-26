IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.DeclareCity'))
	DROP PROCEDURE dbo.DeclareCity;

GO

CREATE PROCEDURE dbo.DeclareCity
	@Name VARCHAR(255),
	@CategoryName VARCHAR(255)
AS
	BEGIN

	INSERT INTO [dbo].[Cities]([Name], [CategoryId])
		SELECT [Name] = @Name, [CategoryId] = pc.Id
			FROM dbo.PricingCategories AS pc
			WHERE pc.[Name] = @CategoryName

	END

GO