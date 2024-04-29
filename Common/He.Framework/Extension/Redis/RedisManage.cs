using He.Common.Extension;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace He.Framework.Extension.Redis
{
    /// <summary>
    /// Redis管理类
    /// </summary>
    public class RedisManage : IRedisManage
    {
        private readonly ILogger<RedisManage> logger;
        private readonly IConnectionMultiplexer connection;
        private readonly IDatabase db;
        private readonly RedisValue token = Environment.MachineName;

        /// <summary>
        /// Redis管理类
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="connection"></param>
        public RedisManage(ILogger<RedisManage> logger, IConnectionMultiplexer connection)
        {
            this.logger = logger;
            this.connection = connection;
            db = connection.GetDatabase();
        }

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public bool Set(string key, string value)
        {
            return db.StringSet(key, value);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> SetAsync(string key, string value)
        {
            return await db.StringSetAsync(key, value);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        public bool Set(string key, object value, TimeSpan ts)
        {
            if (value is null) { return false; }
            if (value is string stringValue) { return db.StringSet(key, stringValue, ts); }
            if (value is bool boolValue) { return db.StringSet(key, boolValue, ts); }
            if (value is int intValue) { return db.StringSet(key, intValue, ts); }
            return db.StringSet(key, value.ToJson(), ts);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        public async Task<bool> SetAsync(string key, object value, TimeSpan ts)
        {
            if (value is null) { return false; }
            if (value is string stringValue) { return await db.StringSetAsync(key, stringValue, ts); }
            if (value is bool boolValue) { return await db.StringSetAsync(key, boolValue, ts); }
            if (value is int intValue) { return await db.StringSetAsync(key, intValue, ts); }
            return await db.StringSetAsync(key, value.ToJson(), ts);
        }
        #endregion

        #region 获取
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string? GetValue(string key)
        {
            return db.StringGet(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string?> GetValueAsync(string key)
        {
            return await db.StringGetAsync(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? Get<T>(string key) where T : class
        {
            var value = db.StringGet(key);
            if (!value.HasValue) { return null; }
            return value.ToString().ToObject<T>();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            var value = await db.StringGetAsync(key);
            if (!value.HasValue) { return null; }
            return value.ToString().ToObject<T>();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return db.KeyExists(key);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return await db.KeyExistsAsync(key);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return db.KeyDelete(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key)
        {
            return await db.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long Remove(IEnumerable<string> keys)
        {
            var redisKeys = keys.Select(P => new RedisKey(P)).ToArray();
            return db.KeyDelete(redisKeys);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<long> RemoveAsync(IEnumerable<string> keys)
        {
            var redisKeys = keys.Select(P => new RedisKey(P)).ToArray();
            return await db.KeyDeleteAsync(redisKeys);
        }
        #endregion

        #region 锁
        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool LockTake(string key, TimeSpan timeOut)
        {
            return db.LockTake(key, token, timeOut);
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<bool> LockTakeAsync(string key, TimeSpan timeOut)
        {
            return await db.LockTakeAsync(key, token, timeOut);
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool LockTake(string key, string token, TimeSpan timeOut)
        {
            return db.LockTake(key, token, timeOut);
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<bool> LockTakeAsync(string key, string token, TimeSpan timeOut)
        {
            return await db.LockTakeAsync(key, token, timeOut);
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool LockRelease(string key)
        {
            return db.LockRelease(key, token);
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> LockReleaseAsync(string key)
        {
            return await db.LockReleaseAsync(key, token);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() => connection?.Dispose();
    }
}
