using LambdaSharp.Maybe;

namespace LambdaSharp.Either
{
    public interface IEither<L, R>
    {
        IEither<L, R> Alt(IEither<L, R> other);
        IEither<L, R> AltLazy(Lazy<IEither<L, R>> other);
        IEither<L, R2> Ap<R2>(IEither<L, Func<R, R2>> other);
        IEither<L2, R2> BiMap<L2, R2>(Func<L, L2> f, Func<R, R2> g);
        T CaseOf<T>(Func<L, T> left, Func<R, T> right);
        T CaseOf<T>(Func<T> wildcard);
        IEither<L, R2> Chain<R2>(Func<R,IEither<L, R2>> f);
        IEither<L2, R> ChainLeft<L2>(Func<L, IEither<L2, R>> f);
        bool Equals(IEither<L, R> other);
        IEither<L, R2> Extend<R2>(Func<IEither<L, R>, R2> f);
        object Extract();
        IEither<L, R> IfLeft(Action<L> effect);
        IEither<L, R> IfRight(Action<R> effect);
        bool IsLeft();
        bool IsRight();
        IEither<L,R> Join();
        IMaybe<L> LeftToMaybe();
        L LeftOrDefault(L defaultValue);
        L LeftOrDefaultLazy(Lazy<Func<L>> defaultValue);
        IEither<L, R2> Map<R2>(Func<R, R2> f);
        IEither<L2, R> MapLeft<L2>(Func<L, L2> f);
        R OrDefault(R defaultValue);
        R OrDefaultLazy(Lazy<Func<R>> defaultValue);
        T Reduce<T>(Func<T, R, T> reducer, T initialValue);
        //IEither<R, L> Swap();
        IMaybe<R> ToMaybe();

        R UnsafeCoerce();
    }
}