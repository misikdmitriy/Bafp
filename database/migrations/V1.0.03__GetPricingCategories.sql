IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllPricingCategories'))
	DROP PROCEDURE dbo.GetAllPricingCategories;

GO

IF type_id('[dbo].[IdsList]') IS NULL
	CREATE TYPE [dbo].[IdsList] AS TABLE
		(
			Id INT
		)

GO

CREATE PROCEDURE dbo.GetAllPricingCategories
	@CategoryIds AS [dbo].[IdsList] READONLY
AS
	BEGIN

	SELECT pc.[Id], pc.[Name]
    FROM [dbo].[PricingCategories] AS pc
		WHERE NOT EXISTS(SELECT 1 FROM @CategoryIds) OR pc.[Id] IN (SELECT Id FROM @CategoryIds)

	END

GO