namespace LambdaSharp.Maybe
{
    public static class Maybe
    {
        private static readonly Nothing nothing = new Nothing();

        public static IMaybe<T> Nothing<T>()
        {
            return (IMaybe<T>) nothing;
        }

        public static IMaybe<T> Just<T>(T value)
        {
            return new Just<T>(value);
        }

        public static IMaybe<T> Of<T>(T value)
        {
            return Just(value);
        }

        public static IMaybe<T> Empty<T>()
        {
            return Nothing<T>();
        }

        public static IMaybe<T> Zero<T>()
        {
            return Nothing<T>();
        }

        public static IMaybe<T> FromNullable<T>(T value)
        {
            return value is null ? Nothing<T>() : Just(value);
        }

        public static IMaybe<T> FromPredicate<T>(Predicate<T> predicate, T value)
        {
            return predicate(value) ? Just(value) : Nothing<T>();
        }

        public static IMaybe<T> Encase<T>(Func<T> throwsF)
        {
            try
            {
                return Just(throwsF());       
            }
            catch
            {
                return Nothing<T>();
            }
        }
    }
}
