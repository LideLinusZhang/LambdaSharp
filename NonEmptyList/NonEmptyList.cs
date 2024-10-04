using LambdaSharp.Maybe;
using LambdaSharp.Tuple;

namespace LambdaSharp.NotEmptyList
{
    internal class NonEmptyList<T>
    {
        private T[] Array;

        internal NonEmptyList(T[] array)
        {
            if (array == null || array.Length == 0) throw new ArgumentException();

            Array = array;
        }


        public static IMaybe<NonEmptyList<T>> FromArray(T[] array) 
        {
            if (array == null || array.Length == 0) return Maybe.Maybe.Nothing<NonEmptyList<T>>();

            return new Just<NonEmptyList<T>>(new NonEmptyList<T>(array));
        }

        public static NonEmptyList<object> FromTuple(Tuple.Tuple<object, object> tuple) => new NonEmptyList<object>(tuple.ToArray());

        public static bool IsNonEmpty() => true;

        public static NonEmptyList<T> UnsafeCoerce(T[] array)
        {
            if (array == null || array.Length == 0) throw new ArgumentException("NonEmptyList#unsafeCoerce was ran on an empty array");

            return new NonEmptyList<T>(array);
        }

        public static T Head(NonEmptyList<T> a) => a.Array[0];

        public static T Last(NonEmptyList<T> a) => a.Array.Last();

        public static T[] Tail(NonEmptyList<T> a) => a.Array[1..];
    }
}
