﻿using System.Linq.Expressions;

namespace AlfaStoreCoreAPI.Files.HelperModel
{
    public class ExpressionCacheHelper
    {
        private static readonly Dictionary<int, Delegate> Cache = new Dictionary<int, Delegate>();
        public static Func<T, bool> GetPredicate<T>(Expression<Func<T, bool>> expression)
        {
            var key = expression.GetHashCode();
            if (Cache.TryGetValue(key, out var cachedDelegate))
            {
                return (Func<T, bool>)cachedDelegate;
            }
            var compiledDelegate = expression.Compile();
            Cache[key] = compiledDelegate;
            return compiledDelegate;
        }
    }
}
