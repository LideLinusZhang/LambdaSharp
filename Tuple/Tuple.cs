namespace LambdaSharp.Tuple
{
    public class Tuple<F, S>
    {
        private readonly F first;
        private readonly S second;

        public Tuple(F f, S s) => (first, second) = (f, s);

        public F fst() => first;

        public S snd() => second;

        public Tuple<F, S2> Ap<T, S2>(Tuple<T, Func<S, S2>> f) => new Tuple<F, S2>(first, f.second(second));

        public Tuple<F2, S2> Bimap<F2, S2>(Func<F, F2> f, Func<S, S2> g) => new Tuple<F2, S2>(f(first), g(second));

        public bool Equals(Tuple<F, S> tuple)
        {
            return (tuple.first is null && first is null) && (tuple.second is null && second is null)
                || (first is not null && second is not null && first.Equals(tuple.first) && second.Equals(tuple.second));
        }

        public static Tuple<FST, SND> Fanout<FST, SND, T>(Func<T, FST> f, Func<T, SND> g, T value) => new Tuple<FST, SND>(f(value), g(value));

        public static Tuple<FST, FST> FromArray<FST>(FST[] array)
        {
            if (array.Length != 2) throw new ArgumentException();

            return new Tuple<FST, FST>(array[0], array[1]);
        }

        public Tuple<F, S2> Map<S2>(Func<S, S2> f) => new Tuple<F, S2>(first, f(second));

        public Tuple<F2, S> MapFirst<F2>(Func<F, F2> f) => new Tuple<F2, S>(f(first), second);

        public T Reduce<T>(Func<T, S, T> reducer, T initialValue) => reducer(initialValue, second);

        public bool Some(Func<F, bool> pred)
        {
            if (second is F fSecond)
            {
                return pred(first) && pred(fSecond);
            }

            return pred(first);
        }

        public bool Some(Func<S, bool> pred)
        {
            if (first is S sFirst)
            {
                return pred(sFirst) && pred(second);
            }

            return pred(second);
        }

        public Tuple<S, F> Swap() => new Tuple<S, F>(second, first);

        public object[] ToArray() => [first, second];
    }
}
