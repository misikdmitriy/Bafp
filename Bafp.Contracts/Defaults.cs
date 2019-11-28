using System;

namespace Bafp.Contracts
{
    public static class Defaults
    {
        public static Func<object> DefaultParameterResolver = () => new object();
    }

    public static class Constants
    {
        public static class PolicyNames
        {
            public const string AllowUi = "AllowUi";
        }

        public static class StoredProcedureNames
        {
            public const string GetAllCities = "dbo.GetAllCities";
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
