using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Cache.CacheKeys
{
    public static class ProductKey
    {
        public static string GetProductKey(int id)
        {
            return $"product:{id}";
        }

        public static string GetProductAllKey()
        {
            return $"products";
        }
    }
}
