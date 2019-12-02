IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCities'))
	DROP PROCEDURE dbo.GetAllCities;

GO

IF type_id('[dbo].[IdsList]') IS NULL
	CREATE TYPE [dbo].[IdsList] AS TABLE
		(
			Id INT
		)

GO

CREATE PROCEDURE dbo.GetAllCities
	@CityIds AS [dbo].[IdsList] READONLY
AS
	BEGIN

	SELECT [Id] = c.Id, [Name] = c.Name, [CategoryId] = pc.Id, [CategoryName] = pc.Name
		FROM dbo.Cities AS c INNER JOIN dbo.PricingCategories AS pc
		ON c.CategoryId = pc.Id
		WHERE NOT EXISTS(SELECT 1 FROM @CityIds) OR c.[Id] IN (SELECT Id FROM @CityIds)

	END

GO