IF OBJECT_ID('dbo.CoursePricing', 'U') IS NOT NULL 
	DROP TABLE dbo.CoursePricing;

IF OBJECT_ID('dbo.CityCourses', 'U') IS NOT NULL 
	DROP TABLE dbo.CityCourses;

IF OBJECT_ID('dbo.PricingCategories', 'U') IS NOT NULL 
	DROP TABLE dbo.PricingCategories;

IF OBJECT_ID('dbo.Cities', 'U') IS NOT NULL 
	DROP TABLE dbo.Cities;

IF OBJECT_ID('dbo.Courses', 'U') IS NOT NULL 
	DROP TABLE dbo.Courses;

-----------------------------------------------------------------------------------

CREATE TABLE dbo.Courses (
	[Id] INT PRIMARY KEY IDENTITY (1, 1),
	[Name] VARCHAR(255) NOT NULL UNIQUE
)

CREATE TABLE dbo.Cities (
	[Id] INT PRIMARY KEY IDENTITY (1, 1),
	[Name] VARCHAR(255) NOT NULL UNIQUE,
	[CategoryId] INT NOT NULL,
)

CREATE TABLE dbo.PricingCategories (
	[Id] INT PRIMARY KEY IDENTITY (1, 1),
	[Name] VARCHAR(255) NOT NULL UNIQUE
)

ALTER TABLE dbo.Cities ADD CONSTRAINT FK_Cities_CategoryId_Id FOREIGN KEY (CategoryId)
      REFERENCES PricingCategories (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE

CREATE TABLE dbo.CityCourses (
	[CityId] INT NOT NULL,
	[CourseId] INT NOT NULL,
	[Count] INT NOT NULL
	PRIMARY KEY ([CityId], [CourseId])
)

CREATE INDEX IX_CityId ON dbo.CityCourses(CityId)
CREATE INDEX IX_CourseId ON dbo.CityCourses(CourseId)

ALTER TABLE dbo.CityCourses ADD CONSTRAINT FK_CityCourses_CityId_Id FOREIGN KEY (CityId)
      REFERENCES Cities (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE

ALTER TABLE dbo.CityCourses ADD CONSTRAINT FK_CityCourses_CourseId_Id FOREIGN KEY (CourseId)
      REFERENCES Courses (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE

CREATE TABLE dbo.CoursePricing (
	[CourseId] INT NOT NULL,
	[CategoryId] INT NOT NULL,
	[Price] SMALLMONEY,
	PRIMARY KEY ([CourseId], [CategoryId])
)

CREATE INDEX IX_CourseId ON dbo.CoursePricing(CourseId)
CREATE INDEX IX_CategoryId ON dbo.CoursePricing(CategoryId)

ALTER TABLE dbo.CoursePricing ADD CONSTRAINT FK_CoursePricing_CourseId_Id FOREIGN KEY (CourseId)
      REFERENCES Courses (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE

ALTER TABLE dbo.CoursePricing ADD CONSTRAINT FK_CoursePricing_CategoryId_Id FOREIGN KEY (CategoryId)
      REFERENCES PricingCategories (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE

-----------------------------------------------------------------------------------

IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.AddCityCourse'))
	DROP PROCEDURE AddCityCourse;

GO

CREATE PROCEDURE dbo.AddCityCourse
	@CityName VARCHAR(255),
	@CourseName VARCHAR(255),
	@Count INT
AS
	BEGIN

	INSERT INTO [dbo].[CityCourses](CityId, CourseId, [Count])
		SELECT CityId = ct.Id, CourseId = cr.Id, [Count] = @Count
		FROM [dbo].[Cities] AS ct, [dbo].[Courses] AS cr
		WHERE ct.Name = @CityName AND cr.Name = @CourseName 

	END

GO

-----------------------------------------------------------------------------------

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

-----------------------------------------------------------------------------------

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

-----------------------------------------------------------------------------------

IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.DeclareCity'))
	DROP PROCEDURE dbo.DeclareCity;

GO

CREATE PROCEDURE dbo.DeclareCity
	@CityName VARCHAR(255),
	@CategoryName VARCHAR(255)
AS
	BEGIN

	INSERT INTO [dbo].[Cities]([Name], [CategoryId])
		SELECT [Name] = @CityName, [CategoryId] = pc.Id
			FROM dbo.PricingCategories AS pc
			WHERE pc.[Name] = @CategoryName

	END

GO

-----------------------------------------------------------------------------------

IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.GetAllCities'))
	DROP PROCEDURE dbo.DeclareCity;

GO

CREATE PROCEDURE dbo.GetAllCities
AS
	BEGIN

	SELECT [CityId] = c.Id, [CityName] = c.Name, [CategoryName] = pc.Name
		FROM dbo.Cities AS c INNER JOIN dbo.PricingCategories AS pc
		ON c.CategoryId = pc.Id

	END

GO