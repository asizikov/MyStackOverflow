using System.Collections.Generic;

namespace MyStackOverflow.Data
{
    public static class EnumerableEx
    {
        public static IEnumerable<T> Return<T>(T value)
        {
            yield return value;
        }
    }
}