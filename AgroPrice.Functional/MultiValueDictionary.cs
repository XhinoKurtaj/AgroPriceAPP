using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace AgroPrice.Functional
{
    /// <summary>
    /// Represents a dictionary where a given key can have multiple values.
    /// </summary>
    /// <typeparam name="TKey">Type of the key parameter.</typeparam>
    /// <typeparam name="TValue">Type of the value parameter.</typeparam>
    public class MultiValueDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, MultiValueDictionary<TKey, TValue>.ValueSet>>
    {
        private readonly Dictionary<TKey, ValueSet> _dictionary;

        public MultiValueDictionary()
        {
            _dictionary = new Dictionary<TKey, ValueSet>();
        }

        public MultiValueDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, ValueSet>(comparer);
        }

        public MultiValueDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, ValueSet>(capacity, comparer);
        }

        public int Count => _dictionary.Count;

        public bool IsEmpty => _dictionary.Count == 0;

        public Dictionary<TKey, ValueSet>.KeyCollection Keys => _dictionary.Keys;

        public Dictionary<TKey, ValueSet>.ValueCollection Values => _dictionary.Values;

        // Returns an empty set if there is no such key in the dictionary.
        public ValueSet this[TKey k] => _dictionary.TryGetValue(k, out var set) ? set : default;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, ValueSet>> IEnumerable<KeyValuePair<TKey, ValueSet>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Add(TKey k, TValue v)
        {
            ValueSet updated;

            if (_dictionary.TryGetValue(k, out var set))
            {
                updated = set.Add(v);
                if (updated.Equals(set)) return false;
            }
            else
            {
                updated = new ValueSet(v);
            }

            _dictionary[k] = updated;
            return true;
        }

        public Dictionary<TKey, ValueSet>.Enumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool ContainsKey(TKey k)
        {
            return _dictionary.ContainsKey(k);
        }

        internal void Clear()
        {
            _dictionary.Clear();
        }

        public void Remove(TKey key)
        {
            _dictionary.Remove(key);
        }

        public struct ValueSet : IEnumerable<TValue>
        {
            public struct Enumerator : IEnumerator<TValue>
            {
                private readonly TValue _value;
                private ImmutableHashSet<TValue>.Enumerator _values;
                private int _count;

                public Enumerator(ValueSet v)
                {
                    if (v._value == null)
                    {
                        _value = default;
                        _values = default;
                        _count = 0;
                    }
                    else
                    {
                        var set = v._value as ImmutableHashSet<TValue>;
                        if (set == null)
                        {
                            _value = (TValue)v._value;
                            _values = default;
                            _count = 1;
                        }
                        else
                        {
                            _value = default;
                            _values = set.GetEnumerator();
                            _count = set.Count;
                            Debug.Assert(_count > 1);
                        }

                        Debug.Assert(_count == v.Count);
                    }
                }

                public void Dispose()
                {
                }

                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator.Current => Current;

                // Note that this property is not guaranteed to throw either before MoveNext()
                // has been called or after the end of the set has been reached.
                public TValue Current => _count > 1 ? _values.Current : _value;

                public bool MoveNext()
                {
                    switch (_count)
                    {
                        case 0:
                            return false;

                        case 1:
                            _count = 0;
                            return true;

                        default:
                            if (_values.MoveNext()) return true;

                            _count = 0;
                            return false;
                    }
                }
            }

            // Stores either a single V or an ImmutableHashSet<V>
            private readonly object _value;

            public int Count
            {
                get
                {
                    if (_value == null) return 0;

                    // The following code used to be written like so:
                    //    
                    //    return (_value as ImmutableHashSet<V>)?.Count ?? 1;
                    // 
                    // This code pattern triggered a code-gen bug on Mac:
                    // https://github.com/dotnet/coreclr/issues/4801

                    var set = _value as ImmutableHashSet<TValue>;
                    if (set == null) return 1;

                    return set.Count;
                }
            }

            public ValueSet(object value)
            {
                _value = value;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            public ValueSet Add(TValue v)
            {
                Debug.Assert(_value != null);

                var set = _value as ImmutableHashSet<TValue>;
                if (set == null)
                {
                    if (ImmutableHashSet<TValue>.Empty.KeyComparer.Equals((TValue)_value, v)) return this;

                    set = ImmutableHashSet.Create((TValue)_value);
                }

                return new ValueSet(set.Add(v));
            }

            public bool Contains(TValue v)
            {
                var set = _value as ImmutableHashSet<TValue>;
                if (set == null) return ImmutableHashSet<TValue>.Empty.KeyComparer.Equals((TValue)_value, v);

                return set.Contains(v);
            }

            public bool Contains(TValue v, IEqualityComparer<TValue> comparer)
            {
                foreach (var other in this)
                    if (comparer.Equals(other, v))
                        return true;

                return false;
            }

            public TValue Single()
            {
                Debug.Assert(_value is TValue); // Implies value != null
                return (TValue)_value;
            }

            public bool Equals(ValueSet other)
            {
                return _value == other._value;
            }
        }
    }
}
