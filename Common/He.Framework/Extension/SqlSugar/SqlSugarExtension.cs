using He.Framework.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace He.Framework.Extension.SqlSugar
{
    /// <summary>
    /// SqlSugar启动服务
    /// </summary>
    public static class SqlSugarExtension
    {
        private static ILogger? logger;

        /// <summary>
        /// SqlSugar服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddSqlSugar(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            logger = services.BuildServiceProvider().GetService<ILogger<object>>();
            var connectionString = AppSettings.GetConnectionString("HeService");
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

            List<ConnectionConfig> connections = [new ConnectionConfig() { ConnectionString = connectionString, DbType = DbType.SqlServer, IsAutoCloseConnection = true }];
            SqlSugarScope scope = new SqlSugarScope(connections, db => { db.Aop.OnLogExecuting = SqlLogExecuting; db.Aop.OnError = SqlError; });
            services.AddSingleton<ISqlSugarClient>(scope);
        }

        /// <summary>
        /// 执行日志
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        public static void SqlLogExecuting(string sql, SugarParameter[] pars)
        {
#if DEBUG
            Console.WriteLine(sql); // 输出SQL, 查看执行SQL  性能无影响
            Console.WriteLine(string.Join(",", pars.Select(it => $"{it.ParameterName}:{it.Value}")));
            Console.WriteLine(UtilMethods.GetNativeSql(sql, pars)); // 获取原生SQL推荐 5.1.4.63  性能OK
            Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer, sql, pars)); // 获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
#endif
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="exp"></param>
        public static void SqlError(SqlSugarException exp)
        {
            ArgumentNullException.ThrowIfNull(logger);
            logger.LogError(exp.InnerException, $"异常：{exp.Message}，SQL：{exp.Sql}");
        }
    }
}
