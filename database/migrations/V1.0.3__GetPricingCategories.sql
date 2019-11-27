IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllPricingCategories'))
	DROP PROCEDURE dbo.GetAllPricingCategories;

GO

CREATE PROCEDURE dbo.GetAllPricingCategories
AS
	BEGIN

	SELECT [Id], [Name]
    FROM [dbo].[PricingCategories]

	END

GO