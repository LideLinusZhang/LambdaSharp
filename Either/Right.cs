using LambdaSharp.Maybe;
using System.Reflection;

namespace LambdaSharp.Either
{
    internal class Right<L, R> : IEither<L, R>
    {
        private R Value {  get; set; }

        internal Right(R value)
        {
            Value = value;
        }

        public IEither<L, R> Alt(IEither<L, R> other) => new Right<L, R>(Value);

        public IEither<L, R> AltLazy(Lazy<IEither<L, R>> other) => new Right<L, R>(Value);

        public IEither<L, R2> Ap<R2>(IEither<L, Func<R, R2>> other)
        {
            object value = other.Extract();

            return other.IsLeft()
                ? new Left<L, R2>((L)value)
                : new Right<L, R2>(((Func<R, R2>)value)(Value));
        }


        public IEither<L2, R2> BiMap<L2, R2>(Func<L, L2> f, Func<R, R2> g) => new Right<L2, R2>(g(Value));

        public T CaseOf<T>(Func<L, T> left, Func<R, T> right) => right(Value);

        public T CaseOf<T>(Func<T> wildcard) => wildcard();

        public IEither<L, R2> Chain<R2>(Func<R, IEither<L, R2>> f) => f(Value);

        public IEither<L2, R> ChainLeft<L2>(Func<L, IEither<L2, R>> f) => new Right<L2,R>(Value);

        public bool Equals(IEither<L, R> other)
        {
            return other.IsRight() && other.Extract().Equals(Value);
        }

        public IEither<L, R2> Extend<R2>(Func<IEither<L, R>, R2> f) => new Right<L, R2>(f(this));

        public object Extract() => Value;

        public bool IsLeft() => false;

        public bool IsRight() => true;

        public IEither<L, R> IfLeft(Action<L> effect) => this;

        public IEither<L, R> IfRight(Action<R> effect)
        {
            effect(Value);
            return this;
        }

        public IEither<L, R> Join()
        {
            if (Value is IEither<L, R> either) return either;

            return this;
        }


        public IMaybe<L> LeftToMaybe() => Maybe.Maybe.Nothing<L>();

        public L LeftOrDefaultLazy(Lazy<Func<L>> defaultValue) => defaultValue.Value();

        public L LeftOrDefault(L defaultValue) => defaultValue;

        public IEither<L, R2> Map<R2>(Func<R, R2> f) => new Right<L, R2>(f(Value));

        public IEither<L2, R> MapLeft<L2>(Func<L, L2> f) => new Right<L2, R>(Value);

        public R OrDefault(R defaultValue) => Value;

        public R OrDefaultLazy(Lazy<Func<R>> defaultValue) => Value;

        public T Reduce<T>(Func<T, R, T> reducer, T initialValue) => reducer(initialValue, Value);

        public IEither<R, L> Swap() => new Left<R, L>(Value);

        public IMaybe<R> ToMaybe() => Maybe.Maybe.Just<R>(Value);

        public R UnsafeCoerce() => Value;
    }
}