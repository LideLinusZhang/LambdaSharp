using LambdaSharp.Maybe;

namespace LambdaSharp.List
{
    // TODO: Implement currying
    public static class List
    {
        public static IMaybe<T> LSAt<T>(int index, IList<T> list) 
        {
            if (index >= list.Count) return Maybe.Maybe.Nothing<T>();

            return new Just<T>(list[index]);
        }

        public static IMaybe<T> LSHead<T>(IList<T> list) 
        {
            if (list is null || list.Count == 0) return Maybe.Maybe.Nothing<T>();

            return new Just<T>(list.First());
        }

        public static IMaybe<T> LSLast<T>(IList<T> list)
        {
            if (list is null || list.Count == 0) return Maybe.Maybe.Nothing<T>();

            return new Just<T>(list.Last());
        }

        public static IMaybe<IList<T>> LSTail<T>(IList<T> list)
        {
            if (list is null || list.Count == 0) return Maybe.Maybe.Nothing<IList<T>>();

            return new Just<IList<T>>(list.Skip(1).ToList());
        }

        public static IMaybe<IList<T>> LSInit<T>(IList<T> list)
        {
            if (list is null || list.Count == 0) return Maybe.Maybe.Nothing<IList<T>>();

            return new Just<IList<T>>(list.SkipLast(1).ToList());
        }

        public static IMaybe<T> LSFind<T>(Func<T,bool> func,IList<T> list)
        {
            foreach(T item in list) 
            {
                if(func(item)) return new Just<T>(item);
            }

            return Maybe.Maybe.Nothing<T>();
        }

        public static IMaybe<int> LSFindIndex<T>(Func<T, bool> func, IList<T> list)
        {
            for(int i = 0; i < list.Count(); i++)
            {
                if (func(list[i])) return new Just<int>(i);
            }

            return Maybe.Maybe.Nothing<int>();
        }

        public static IMaybe<Tuple.Tuple<T, IList<T>>> LSUncons<T>(IList<T> list)
        {
            if (list is null || list.Count == 0) return Maybe.Maybe.Nothing<Tuple.Tuple<T, IList<T>>>();

            return new Just<Tuple.Tuple<T, IList<T>>>(new Tuple.Tuple<T, IList<T>>(list.First(), list.Skip(1).ToList()));
        }

        public static int LSSum<T>(IList<int> list)
        {
            return list.Sum();
        }

        public static IList<T> LSSort<T>(Comparer<T> compareFunc, IList<T> list)
        {
            return list.OrderBy(x => x, compareFunc).ToList();
        }
    }
}
