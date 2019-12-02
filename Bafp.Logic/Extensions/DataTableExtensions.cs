using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;

namespace Bafp.Logic.Extensions
{
    public static class DataTableExtensions
    {
        public static SqlMapper.ICustomQueryParameter AsTableValued<T>(this IEnumerable<T> objs)
        {
            var dataTable = new DataTable();
            var instanceType = typeof(T);

            var properties = instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead);
            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var obj in objs)
            {
                dataTable.Rows.Add(properties.Select(p => p.GetValue(obj)).ToArray());
            }

            return dataTable.AsTableValuedParameter();
        }
    }
}