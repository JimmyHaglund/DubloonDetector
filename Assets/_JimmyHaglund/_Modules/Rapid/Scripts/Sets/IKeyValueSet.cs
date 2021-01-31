using System.Collections.Generic;
namespace JimmyHaglund {
    public interface IKeyValueSet<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> {
        void Add(TKey key, TValue value);
        bool TryGetValue(TKey key, out TValue value);
        bool Remove(TKey key);
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);
        TValue this[TKey key] { get; set; }
    }
}