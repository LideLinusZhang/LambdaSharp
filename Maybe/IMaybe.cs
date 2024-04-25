
namespace LambdaSharp.Maybe
{
    public interface IMaybe<T>
    {
        IMaybe<T> Alt(IMaybe<T> other);
        IMaybe<T> AltLazy(Lazy<IMaybe<T>> getOther);
        T CaseOf(Func<T, T> justPattern, Func<T> nothingPattern);
        IMaybe<TResult> Chain<TResult>(Func<T, IMaybe<TResult>> f);
        bool Equals(IMaybe<T> other);
        IMaybe<TResult> Extend<TResult>(Func<IMaybe<T>, TResult> f);
        T? Extract();
        IMaybe<T> Filter(Predicate<T> predicate);
        IMaybe<T> IfJust(Action<T> effect);
        IMaybe<T> IfNothing(Action effect);
        bool IsJust();
        bool IsNothing();
        IMaybe<T> Join();
        IMaybe<TResult> Map<TResult>(Func<T, TResult> f);
        TResult MapOrDefault<TResult>(Func<T, TResult> f, TResult defaultValue);
        T OrDefault(T defaultValue);
        T OrDefaultLazy(Lazy<T> getDefaultValue);
        TResult Reduce<TResult>(Func<TResult, T, TResult> reducer, TResult initialValue);
        T UnsafeCoerce();
    }
}