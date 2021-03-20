using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LRU
{
    public class LRUCache<TKey, TValue>
    {
        private readonly int _cap;
        private readonly ConcurrentDictionary<TKey, LinkedListNode<(TKey Key, TValue Value)>> _lookup;
        private readonly LinkedList<(TKey Key, TValue Value)> _list;

        public LRUCache(int capacity)
        {
            _cap = capacity;
            _lookup = new ConcurrentDictionary<TKey, LinkedListNode<(TKey, TValue)>>();
            _list = new LinkedList<(TKey, TValue)>();
        }

        public ICollection<TKey> Keys => _lookup.Keys;
        public ICollection<TValue> Values => _lookup.Values.Select(v => v.Value.Value).ToList();
        public int Count => _lookup.Count;
        public bool ContainsKey(TKey key) => _lookup.ContainsKey(key);

        public void Add(TKey key, TValue value)
        {
            lock(this)
            {
                Remove(key);
                while (_lookup.Count >= _cap)
                {
                    ClearOldests();
                }
                var node = _list.AddFirst((key, value));
                _lookup[key] = node;
            }
        }

        public bool Remove(TKey key)
        {
            lock(this)
            {
                if (_lookup.TryRemove(key, out var node))
                {
                    _list.Remove(node);
                    return true;
                }
                return false;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_lookup.TryGetValue(key, out var node))
            {
                value = node.Value.Value;
                return true;
            }

            value = default;
            return false;
        }

        public void Clear()
        {
            lock(this)
            {
                _lookup.Clear();
                _list.Clear();
            }
        }

        private void ClearOldests()
        {
            _lookup.TryRemove(_list.Last.Value.Key, out var _);
            _list.RemoveLast();
        }
    }
}
