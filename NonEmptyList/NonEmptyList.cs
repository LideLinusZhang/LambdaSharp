using LambdaSharp.Maybe;
using LambdaSharp.Tuple;
using System.Collections;

namespace LambdaSharp.NotEmptyList
{
    internal class NonEmptyList<T> : IList<T>
    {
        private readonly List<T> list;

        public int Count => ((ICollection<T>)list).Count;

        public bool IsReadOnly => ((ICollection<T>)list).IsReadOnly;

        public T this[int index] { get => ((IList<T>)list)[index]; set => ((IList<T>)list)[index] = value; }

        public NonEmptyList(IList<T> list)
        {
            if (list is null || list.Count == 0) throw new ArgumentException();

            this.list = list.ToList();
        }


        public static IMaybe<NonEmptyList<T>> FromArray(IList<T> array)
        {
            if (array == null || array.Count == 0) return Maybe.Maybe.Nothing<NonEmptyList<T>>();

            return new Just<NonEmptyList<T>>(new NonEmptyList<T>(array));
        }

        public static NonEmptyList<object> FromTuple(Tuple.Tuple<object, object> tuple) => new NonEmptyList<object>(tuple.ToArray());

        public static bool IsNonEmpty() => true;

        public static NonEmptyList<T> UnsafeCoerce(IList<T> array)
        {
            if (array == null || array.Count == 0) throw new ArgumentException("NonEmptyList#unsafeCoerce was ran on an empty array");

            return new NonEmptyList<T>(array);
        }

        public static T Head(NonEmptyList<T> a) => a[0];

        public static T Last(NonEmptyList<T> a) => a.Last();

        public static IList<T> Tail(NonEmptyList<T> a) => a.list[1..];

        public int IndexOf(T item)
        {
            return ((IList<T>)list).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)list).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)list).RemoveAt(index);
        }

        public void Add(T item)
        {
            ((ICollection<T>)list).Add(item);
        }

        public void Clear()
        {
            ((ICollection<T>)list).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)list).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)list).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)list).Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }
    }
}
