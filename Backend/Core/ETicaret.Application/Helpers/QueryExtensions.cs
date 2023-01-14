namespace ETicaret.Application.Helpers
{
    public static class QueryExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query , bool condition ,Func<T,bool> filter)
        {
            return condition ? query.AsQueryable() : query.Where(filter).AsQueryable();
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> filter)
        {
            return condition ? query.AsQueryable() : query.Where(filter).AsQueryable();
        }

        public static IQueryable<T> PageBy<T>(this IQueryable<T> query , int pageSize ,int skipCount)
        {
            pageSize = pageSize <= 0 ? 0 : pageSize - 1;
            return query.Skip(pageSize * skipCount).Take(skipCount);
        }
    }
}
