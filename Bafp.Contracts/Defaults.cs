using System;

namespace Bafp.Contracts
{
    public static class Defaults
    {
        public static readonly Func<object> ParameterResolver = () => new object();
    }

    public static class Constants
    {
        public static class PolicyNames
        {
            public const string AllowUi = "AllowUi";
        }

        public class DatabaseNames
        {
            public const string MsSql = "mssql";
        }

        public static class StoredProcedureNames
        {
            public const string GetAllCities = "dbo.GetAllCities";
            public const string GetCitiesTotal = "dbo.GetCitiesTotal";
            public const string GetCityCourses = "dbo.GetCityCourses";
            public const string GetAllCourses = "dbo.GetAllCourses";
            public const string GetAllCityCourses = "dbo.GetAllCityCourses";
            public const string GetAllCityCoursesByCourse = "dbo.GetAllCityCoursesByCourse";
            public const string GetCoursePriceList = "dbo.GetCoursePriceList";
            public const string GetCoursePriceListByCourse = "dbo.GetCoursePriceListByCourse";
            public const string GetAllPricingCategories = "dbo.GetAllPricingCategories";
            public const string UpsertCity = "dbo.UpsertCity";
            public const string UpsertCityCourse = "dbo.UpsertCityCourse";
            public const string UpsertCoursePricing = "dbo.UpsertCoursePricing";
            public const string InsertCourse = "dbo.InsertCourse";
            public const string DeleteCourse = "dbo.DeleteCourse";
        }
    }
}
