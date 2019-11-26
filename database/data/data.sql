INSERT INTO dbo.Cities(Name)
	VALUES ('Kyiv');

INSERT INTO dbo.Courses(Name)
	VALUES('UI/UX'),
		('Digital Marketing'),
		('Entrpreneurship'),
		('Advanced HR'),
		('English For IT'),
		('Backend'),
		('Project Management'),
		('Python'),
		('Frontend'),
		('WordPress'),
		('Advanced JS'),
		('Node JS'),
		('Sketch'),
		('Magento'),
		('Introduction To IT')

INSERT INTO dbo.PricingCategories(Name)
	VALUES ('X'),
		('Y'),
		('Z')

EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'Frontend', @Count = 4;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'UI/UX', @Count = 4;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'Digital Marketing', @Count = 1;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'Entrpreneurship', @Count = 1;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'Advanced HR', @Count = 2;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'English For IT', @Count = 1;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'Backend', @Count = 1;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'Project Management', @Count = 3;
EXEC dbo.AddCityCourse @CityName = 'Kyiv', @CourseName = 'Python', @Count = 3;

EXEC dbo.AddCoursePricing @CourseName = 'Frontend', @CategoryName = 'X', @Price = 720.00; 
EXEC dbo.AddCoursePricing @CourseName = 'UI/UX', @CategoryName = 'X', @Price = 720.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Backend', @CategoryName = 'X', @Price = 720.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Python', @CategoryName = 'X', @Price = 720.00; 
EXEC dbo.AddCoursePricing @CourseName = 'WordPress', @CategoryName = 'X', @Price = 720.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Advanced JS', @CategoryName = 'X', @Price = 400.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Node JS', @CategoryName = 'X', @Price = 400.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Sketch', @CategoryName = 'X', @Price = 400.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Magento', @CategoryName = 'X', @Price = 400.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Project Management', @CategoryName = 'X', @Price = 720.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Digital Marketing', @CategoryName = 'X', @Price = 533.00; 
EXEC dbo.AddCoursePricing @CourseName = 'English For IT', @CategoryName = 'X', @Price = 417.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Entrpreneurship', @CategoryName = 'X', @Price = 400.00; 
EXEC dbo.AddCoursePricing @CourseName = 'Introduction To IT', @CategoryName = 'X', @Price = 267.00; 

EXEC dbo.DeclareCityCategory @CityName = 'Kyiv', @CategoryName = 'X'

SELECT dbo.CountCityPrice('Kyiv');