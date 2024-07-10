namespace LambdaSharp.Tuple
{
    internal class Tuple<F, S> // does not implement Iterator and ArrayLike interface
    {
        private F first;
        private S second;

        public Tuple(F f, S s) => (first, second) = (f, s);

        public F fst() => first;

        public S snd() => second;

        public Tuple<F, S2> Ap<T, S2>(Tuple<T, Func<S, S2>> f) => new Tuple<F, S2>(first, f.second(second));

        public Tuple<F2, S2> Bimap<F2, S2>(Func<F,F2> f, Func<S, S2> g) => new Tuple<F2, S2>(f(first), g(second));

        public bool Equals(Tuple<F,S> tuple)
        {
            return first.Equals(tuple.first) && second.Equals(tuple.second);
        }

        public bool Some(Func<F, bool> pred)
        {
            if (second is F fSecond)
            {
                return pred(first) && pred(fSecond);
            }

            return false;
        }

        public static Tuple<FST, SND> Fanout<FST, SND, T>(Func<T, FST> f, Func<T, SND> g, T value) => new Tuple<FST, SND>(f(value), g(value));  

        public static Tuple<FST, FST> FromArray<FST>(FST[] array)
        {
            if (array.Length != 2) throw new ArgumentException();

            return new Tuple<FST, FST>(array[0], array[1]);
        }

        public Tuple<F, S2> Map<S2>(Func<S, S2> f) => new Tuple<F, S2>(first, f(second));

        public Tuple<F2, S> MapFirst<F2>(Func<F, F2> f) => new Tuple<F2, S>(f(first),second);

        public T Reduce<T>(Func<T, S, T> reducer, T initialValue) => reducer(initialValue, second);

        public bool Some(Func<F, bool> pred)
        {
            if (second is F fSecond)
            {
                return pred(first) && pred(fSecond);
            }

            return pred(first);
        }

        public bool Some(Func<S, bool> pred) //how to guarantee the above function will always match first
        {
            if (first is S sFirst)
            {
                return pred(sFirst) && pred(second);
            }

            return pred(second);
        }

        public Tuple<S, F> Swap() => new Tuple<S, F>(second, first);

        public IEnumerable<object> ToArray() => new List<object> { first, second };

    }
}
