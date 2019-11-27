IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.DeleteCourse'))
	DROP PROCEDURE DeleteCourse;

GO

CREATE PROCEDURE dbo.DeleteCourse
	@CourseId INT
AS
  BEGIN

    DELETE FROM [dbo].[Courses]
        WHERE [Id] = @CourseId

  END

GO