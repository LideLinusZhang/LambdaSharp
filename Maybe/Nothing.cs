namespace LambdaSharp.Maybe
{
    internal class Nothing : IMaybe<object>
    {
        public IMaybe<object> Alt(IMaybe<object> other) => other.IsJust() ? other : this;

        public IMaybe<object> AltLazy(Lazy<IMaybe<object>> getOther) => getOther.Value.IsJust() ? getOther.Value : this;

        public object CaseOf(Func<object, object> justPattern, Func<object> nothingPattern) => nothingPattern();

        public IMaybe<TResult> Chain<TResult>(Func<object, IMaybe<TResult>> f) => (IMaybe<TResult>) this;

        public bool Equals(IMaybe<object> other) => other.IsNothing();

        public IMaybe<TResult> Extend<TResult>(Func<IMaybe<object>, TResult> f) => (IMaybe<TResult>) this;

        public object? Extract() => null;

        public IMaybe<object> Filter(Predicate<object> predicate) => this;

        public IMaybe<object> IfJust(Action<object> effect) => this;

        public IMaybe<object> IfNothing(Action effect)
        {
            effect();
            return this;
        }

        public bool IsJust() => false;

        public bool IsNothing() => true;

        public IMaybe<object> Join() => this;

        public IMaybe<TResult> Map<TResult>(Func<object, TResult> f) => (IMaybe<TResult>) this;

        public TResult MapOrDefault<TResult>(Func<object, TResult> f, TResult defaultValue) => defaultValue;

        public object OrDefault(object defaultValue) => defaultValue;

        public object OrDefaultLazy(Lazy<object> getDefaultValue) => getDefaultValue.Value;

        public TResult Reduce<TResult>(Func<TResult, object, TResult> reducer, TResult initialValue) => initialValue;

        public object UnsafeCoerce()
        {
            throw new NullReferenceException();
        }
    }
}
