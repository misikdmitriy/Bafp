IF OBJECT_ID('dbo.CoursePricing', 'U') IS NOT NULL 
	DROP TABLE dbo.CoursePricing;

IF OBJECT_ID('dbo.CityCourses', 'U') IS NOT NULL 
	DROP TABLE dbo.CityCourses;

IF OBJECT_ID('dbo.Cities', 'U') IS NOT NULL 
	DROP TABLE dbo.Cities;

IF OBJECT_ID('dbo.PricingCategories', 'U') IS NOT NULL 
	DROP TABLE dbo.PricingCategories;

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