namespace LambdaSharp.Maybe
{
    internal class Just<T> : IMaybe<T>
    {
        private T Value { get; set; }

        internal Just(T value) 
        {
            Value = value;
        }

        public IMaybe<T> Alt(IMaybe<T> other) => this;

        public IMaybe<T> AltLazy(Lazy<IMaybe<T>> getOther) => this;

        public T CaseOf(Func<T, T> justPattern, Func<T> nothingPattern) => justPattern(Value);

        public IMaybe<TResult> Chain<TResult>(Func<T, IMaybe<TResult>> f) => f(Value);

        public bool Equals(IMaybe<T> other)
        {
            return other.IsJust() && (other as Just<T>)!.Value!.Equals(Value);
        }

        public IMaybe<TResult> Extend<TResult>(Func<IMaybe<T>, TResult> f) => new Just<TResult>(f(this));

        public T? Extract() => Value;

        public IMaybe<T> Filter(Predicate<T> predicate)
        {
            return predicate(Value) ? this : Maybe.Nothing<T>();
        }

        public IMaybe<T> IfJust(Action<T> effect)
        {
            effect(Value);
            return this;
        }

        public IMaybe<T> IfNothing(Action effect) => this;

        public bool IsJust() => true;

        public bool IsNothing() => false;

        public IMaybe<T> Join()
        {
            if (Value is IMaybe<T> inner) 
            { 
                return inner; 
            }
            else
            {
                return this;
            }
        }

        public IMaybe<TResult> Map<TResult>(Func<T, TResult> f) => new Just<TResult>(f(Value));

        public TResult MapOrDefault<TResult>(Func<T, TResult> f, TResult defaultValue) => f(Value);

        public T OrDefault(T defaultValue) => Value;

        public T OrDefaultLazy(Lazy<T> getDefaultValue) => Value;

        public TResult Reduce<TResult>(Func<TResult, T, TResult> reducer, TResult initialValue)
        {
            return reducer(initialValue, Value);
        }

        public T UnsafeCoerce() => Value;
    }
}
