using UnityEngine;
using System.Collections.Generic;

namespace JimmyHaglund {
    public abstract class ListSet<T> : ScriptableObject {
        protected List<T> _items = new List<T>();

        public List<T> Items => _items;

        public void Add(T item) {
            if (!_items.Contains(item)) {
                _items.Add(item);
            }
        }

        public void Remove(T item) {
            _items.Remove(item);
        }
    }
}