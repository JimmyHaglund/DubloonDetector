using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace JimmyHaglund {
    public abstract class DictionarySet<TKey, TValue> : ScriptableObject, IKeyValueSet<TKey, TValue> {
        protected Dictionary<TKey, TValue> _items = new Dictionary<TKey, TValue>();

        public TValue this[TKey key] { get => _items[key]; set => _items[key] = value; }

        public Dictionary<TKey, TValue> Items => _items;

        public virtual void Add(TKey key, TValue value) {
            if (!_items.ContainsKey(key)) {
                _items.Add(key, value);
            }
        }

        public bool ContainsKey(TKey key) => _items.ContainsKey(key);

        public bool ContainsValue(TValue value) => _items.ContainsValue(value);


        public virtual bool Remove(TKey key) => _items.Remove(key);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public bool TryGetValue(TKey key, out TValue value) {
            return _items.TryGetValue(key, out value);
        }
    }
}