IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCities'))
	DROP PROCEDURE dbo.GetAllCities;

GO

CREATE PROCEDURE dbo.GetAllCities
AS
	BEGIN

	SELECT [Id] = c.Id, [Name] = c.Name, [CategoryName] = pc.Name
		FROM dbo.Cities AS c INNER JOIN dbo.PricingCategories AS pc
		ON c.CategoryId = pc.Id

	END

GO