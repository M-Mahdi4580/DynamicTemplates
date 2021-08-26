using System;
using System.Collections;
using System.Collections.Generic;


namespace CoroutineFacilities
{
    /// <summary>
    /// Allows an ordered recursive execution of coroutines and also, provides facilities for a coroutine-based programming.
    /// </summary>
    public static class CoroutineManager
    {
        /// <summary>
        /// Starts execution of the coroutine.
        /// </summary>
        /// <remarks>
        /// Note:
        /// Yielding another coroutine or an array of coroutines, starts a new recursive execution. This allows for further control over the execution order in of the coroutines.
        /// </remarks>
        /// <param name="coroutine">The coroutine object to fully execute</param>
        public static void Start(IEnumerator coroutine)
        {
            while (coroutine.MoveNext())
            {
                switch (coroutine.Current)
                {
                    case IEnumerator innerCoroutine:
                        Start(innerCoroutine);
                        break;

                    case IEnumerator[] innerCoroutines:
                        Start(innerCoroutines);
                        break;
                }
            }
        }

        /// <summary>
        /// Starts an ordered execution of coroutines and continues until all of them have completely yielded.
        /// </summary>
        /// <param name="coroutines">An ordered array of coroutines to execute</param>
        public static void Start(params IEnumerator[] coroutines)
        {
            int index = 0;
            int length = coroutines.Length;

            while (length > 0)
            {
                var coroutine = coroutines[index];
                if (coroutine == null)
                {
                    index--;
                    length--;
                    continue;
                }

                bool hasNext;

                while (hasNext = coroutine.MoveNext())
                {
                    object current = coroutine.Current;
                    if (current is IEnumerator[] innerCoroutines)
                    {
                        Start(innerCoroutines);
                    }
                    else if (current is IEnumerator innerCoroutine)
                    {
                        Start(innerCoroutine);
                    }
                    else
                    {
                        break;
                    }
                }

                if (!hasNext)
                {
                    coroutines[index] = null;
                    if (index == length - 1) length--;
                }

                index += Math.Sign(length - 1 - index);
            }
        }


        /// <summary>
        /// Yields the coroutine array.
        /// </summary>
        public static IEnumerator Yield(params IEnumerator[] coroutines)
        {
            yield return coroutines;
        }

        /// <summary>
        /// Yields the coroutine.
        /// </summary>
        public static IEnumerator Yield(IEnumerator coroutine)
        {
            yield return coroutine;
        }

        /// <summary>
        /// Iteratively calls the function and yields the generated coroutines.
        /// </summary>
        /// <param name="count">Number of times to call the coroutine</param>
        /// <param name="func">A coroutine generator function with a zero-based for-loop counter as its argument</param>
        /// <returns>A sequence of coroutines</returns>
        public static IEnumerator For(int count, Func<int, IEnumerator> func)
        {
            for (int i = 0; i < count; i++)
            {
                yield return func(i);
            }
        }

        /// <summary>
        /// Calls the function for each element in the collection and yields the generated coroutines one by one.
        /// </summary>
        /// <typeparam name="T">Type of the elements of the collection</typeparam>
        /// <param name="collection">The collection to iterate over</param>
        /// <param name="func">A coroutine generator function with the current collection element as its argument</param>
        /// <returns>A sequence of coroutines</returns>
        public static IEnumerator ForEach<T>(IEnumerable<T> collection, Func<T, IEnumerator> func)
        {
            foreach (var item in collection)
            {
                yield return func(item);
            }
        }
    }
}