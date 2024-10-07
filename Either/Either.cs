namespace LambdaSharp.Either
{
    public static class Either
    {
        public static IEither<object, T> Of<T>(T value) => new Right<object, T>(value);

        public static IEnumerable<L> Lefts<L,R>(IEnumerable<IEither<L,R>> list)
        {
            foreach (var item in list)
            {
                if (item is Left<L,R> left) 
                {
                    yield return (L) left.Extract();
                }
            }
        }

        public static IEnumerable<R> Rights<L, R>(IEnumerable<IEither<L, R>> list)
        {
            foreach (var item in list)
            {
                if (item is Right<L, R> right)
                {
                    yield return (R) right.Extract();
                }
            }
        }


        public static IEither<L, R> Encase<L, R>(Func<R> throwsF) where L : Exception
        {
            try
            {
                return new Right<L,R>(throwsF());
            }
            catch (L e)
            {
                {
                    return new Left<L, R>(e);
                }
            }
        }

        public static IEither<L, R[]> Sequence<L,R>(IEnumerable<IEither<L,R>> list) 
        {
            List<R> result = new List<R>();
            foreach (var item in list)
            {
                if (item is Left<L,R> left)
                {
                    return new Left<L, R[]>((L)left.Extract());
                }
                else
                {
                    result.Add((R)item.Extract());
                }
            }

            return new Right<L, R[]>(result.ToArray());
        }

        public static bool IsEither<L, R>(object x) => x is IEither<L, R>;
    }
}