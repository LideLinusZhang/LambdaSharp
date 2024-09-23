using LambdaSharp.Maybe;

namespace LambdaSharp.Either
{
	internal class Left<L, R> : IEither<L, R>
	{
		private L Value { get; set; }

		internal Left(L value)
		{
			Value = value;
		}

        public IEither<L, R> Alt(IEither<L, R> other) => other.Map(x => x);

        public IEither<L, R> AltLazy(Lazy<IEither<L, R>> other) => other.Value.Map(x => x);

        public IEither<L, R2> Ap<R2>(IEither<L, Func<R, R2>> other)
        {
            return other.IsLeft()
                ? new Left<L, R2>((L)other.Extract())
                : new Left<L, R2>(Value);
        }

        public IEither<L2, R2> BiMap<L2, R2>(Func<L, L2> f, Func<R, R2> g) => new Left<L2, R2>(f(Value));

        public T CaseOf<T>(Func<L, T> left, Func<R, T> right) => left(Value);

        public T CaseOf<T>(Func<T> wildcard) => wildcard();

        public IEither<L, R2> Chain<R2>(Func<R, IEither<L, R2>> f) => new Left<L, R2>(Value);

        public IEither<L2, R> ChainLeft<L2>(Func<L, IEither<L2, R>> f) => f(Value);

        public bool Equals(IEither<L, R> other)
        {
            return other.IsLeft() && other.Extract().Equals(Value);
        }

        public IEither<L, R2> Extend<R2>(Func<IEither<L, R>, R2> f) => new Left<L, R2>(Value);

        public object Extract() => Value;

        public bool IsLeft() => true;

		public bool IsRight() => false;

        public IEither<L, R> IfLeft(Action<L> effect)
        {
            effect(Value);
            return this;
        }
        public IEither<L, R> IfRight(Action<R> effect) => this;

        public IEither<L, R> Join() => new Left<L,R>(Value);

        public IMaybe<L> LeftToMaybe() => Maybe.Maybe.Just<L>(Value);

        public L LeftOrDefault(L defaultValue) => Value;
        
        public L LeftOrDefaultLazy(Lazy<Func<L>> defaultValue) => Value;

        public IEither<L, R2> Map<R2>(Func<R, R2> f) =>new Left<L, R2>(Value);

        public IEither<L2, R> MapLeft<L2>(Func<L, L2> f) => new Left<L2, R>(f(Value));

        public R OrDefault(R defaultValue) => defaultValue;

        public R OrDefaultLazy(Lazy<Func<R>> defaultValue) => defaultValue.Value();

        public T Reduce<T>(Func<T, R, T> reducer, T initialValue) => initialValue;

        public IEither<R, L> Swap() => new Right<R, L>(Value);

        public IMaybe<R> ToMaybe() => Maybe.Maybe.Nothing<R>();

        public R UnsafeCoerce()
        {
            if (Value is Exception exception) throw exception;

            throw new Exception("Uncaught Error: Either#unsafeCoerce was ran on a Left");
        }
    }
}