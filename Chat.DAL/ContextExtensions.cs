using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Text.RegularExpressions;

namespace Chat.DAL
{
    public static class ContextExtensions
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetTableName<T>();
        }

        public static string GetTableName<T>(this ObjectContext context) where T : class
        {
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Match match = Regex.Match(sql, "FROM (?<table>.*) AS");

            string table = match.Groups["table"].Value;
            return table;
        }
    }
}