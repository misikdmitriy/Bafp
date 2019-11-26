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