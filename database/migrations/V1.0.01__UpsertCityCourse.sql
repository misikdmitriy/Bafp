IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.UpsertCityCourse'))
	DROP PROCEDURE UpsertCityCourse;

GO

CREATE PROCEDURE dbo.UpsertCityCourse
	@CityId INT,
	@CourseId INT,
	@Count INT
AS
	BEGIN TRY

	IF NOT EXISTS(SELECT * FROM [dbo].[Cities] WHERE [Id] = @CityId)
		RAISERROR ('City %d absent in DB', 16, 1, @CityId)

	IF NOT EXISTS(SELECT * FROM [dbo].[Courses] WHERE [Id] = @CourseId)
		RAISERROR ('Course %d absent in DB', 16, 1, @CourseId)
	
	IF @Count <= 0
		DELETE FROM [dbo].[CityCourses] 
			WHERE  [CityId] = CityId AND [CourseId] = @CourseId
	ELSE
		INSERT INTO [dbo].[CityCourses](CityId, CourseId, [Count])
			VALUES(@CityId, @CourseId, @Count)

	END TRY

	BEGIN CATCH
		IF ERROR_NUMBER() IN (2601, 2627) 
			UPDATE [dbo].[CityCourses] SET [Count] = @Count
				WHERE  [CityId] = @CityId AND [CourseId] = @CourseId
	END CATCH

GO