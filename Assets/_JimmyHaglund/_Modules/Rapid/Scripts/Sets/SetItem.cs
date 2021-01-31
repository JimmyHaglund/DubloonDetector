using System.Collections;
using UnityEngine;
using JimmyHaglund;

namespace JimmyHaglund.Rapid {
    public class SetItem<KeyType, ItemType> {
        private IKeyValueSet<KeyType, ItemType> _set;
        private ItemType _item;
        private KeyType _key;
        private bool _assigned = false;

        public SetItem(ItemType item, IKeyValueSet<KeyType, ItemType> set) {
            _item = item;
            _set = set;
        }

        public void RemoveFromSet() {
            if (_set == null) return;
            _set.Remove(_key);
        }

        public void AssignToSet(KeyType key, bool _removeIfAdded = true) {
            if (_removeIfAdded && _assigned) RemoveFromSet();
            if (_set == null) return;
            if (_set.ContainsKey(key)) {
                Debug.LogWarning("Couldn't add item " + _item.ToString() + " to set " + 
                    _set.ToString() + " at key " + key.ToString() + ": key already exists in set!");
                return;
            }
            _key = key;
            _set.Add(key, _item);
        }
    }
}