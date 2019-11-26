using System;

namespace Bafp.Contracts
{
    public static class Defaults
    {
        public static Func<object> DefaultParameterResolver = () => new object();
    }

    public static class Constants
    {
        public static class StoredProcedureNames
        {
            public const string GetAllCities = "dbo.GetAllCities";
            public const string GetAllCourses = "dbo.GetAllCourses";
            public const string GetAllCityCourses = "dbo.GetAllCityCourses";
            public const string InsertCity = "dbo.InsertCity";
            public const string AddCityCourse = "dbo.AddCityCourse";
            public const string InsertCourse = "dbo.InsertCourse";
        }
    }
}
