﻿namespace SchemaExplorer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    [Serializable]
    public class SchemaObjectBaseCollection : ISchemaObjectBaseList, IList, IList<SchemaObjectBase>, ICloneable
    {
        private SchemaObjectBase[] _array;
        private int _count;
        private const int _defaultCapacity = 0x10;
        [NonSerialized]
        private int _version;

        public SchemaObjectBaseCollection()
        {
            this._array = new SchemaObjectBase[0x10];
        }

        public SchemaObjectBaseCollection(SchemaObjectBaseCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this._array = new SchemaObjectBase[collection.Count];
            this.AddRange(collection);
        }

        private SchemaObjectBaseCollection(Tag tag)
        {
        }

        public SchemaObjectBaseCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");
            }
            this._array = new SchemaObjectBase[capacity];
        }

        public SchemaObjectBaseCollection(SchemaObjectBase[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            this._array = new SchemaObjectBase[array.Length];
            this.AddRange(array);
        }

        public virtual int Add(SchemaObjectBase value)
        {
            int num2 = 2;
        Label_000D:
            switch (num2)
            {
                case 0:
                    this.EnsureCapacity(this._count + 1);
                    num2 = 1;
                    goto Label_000D;

                case 1:
                    goto Label_005D;

                case 2:
                    break;

                default:
                    break;
                   
            }
            if (this._count == this._array.Length)
            {
                num2 = 0;
                goto Label_000D;
            }
        Label_005D:
            this._version++;
            this._array[this._count] = value;
            return this._count++;
        }

        public virtual void AddRange(SchemaObjectBaseCollection collection)
        {
            int num = 5;
        Label_000D:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    this.EnsureCapacity(this._count + collection.Count);
                    num = 2;
                    goto Label_000D;

                case 2:
                    goto Label_00C9;

                case 3:
                    return;

                case 4:
                    if ((this._count + collection.Count) <= this._array.Length)
                    {
                        goto Label_00C9;
                    }
                    num = 1;
                    goto Label_000D;

                case 6:
                    if (collection.Count != 0)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;

                default:
                    if (collection != null)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentNullException("collection");
        Label_00C9:
            this._version++;
            Array.Copy(collection.InnerArray, 0, this._array, this._count, collection.Count);
            this._count += collection.Count;
        }

        public virtual void AddRange(SchemaObjectBase[] array)
        {
            // This item is obfuscated and can not be translated.
            int num = 1;
        Label_0018:
            switch (num)
            {
                case 0:
                    return;

                case 1:
                     
                    break;

                case 2:
                    if (array.Length != 0)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_0018;

                case 3:
                    this.EnsureCapacity(this._count + array.Length);
                    num = 6;
                    goto Label_0018;

                case 4:
                    throw new ArgumentNullException("array");

                case 5:
                    if ((this._count + array.Length) <= this._array.Length)
                    {
                        goto Label_00C8;
                    }
                    num = 3;
                    goto Label_0018;

                case 6:
                    goto Label_00C8;
            }
            if (array != null)
            {
                num = 2;
            }
            else
            {
                num = 4;
            }
            goto Label_0018;
        Label_00C8:
            this._version++;
            Array.Copy(array, 0, this._array, this._count, array.Length);
            this._count += array.Length;
        }

        public virtual int BinarySearch(SchemaObjectBase value)
        {
            return Array.BinarySearch<SchemaObjectBase>(this._array, 0, this._count, value);
        }

        private void CheckEnumIndex(int index)
        {
            // This item is obfuscated and can not be translated.
            int num = 3;
        Label_000D:
            switch (num)
            {
                case 0:
                    num = 1;
                    goto Label_000D;

                case 1:
                    if (index < this._count)
                    {
                        return;
                    }
                    num = 2;
                    goto Label_000D;

                case 2:
                   
                    break;

                default:
                    if (index >= 0)
                    {
                        num = 0;
                        goto Label_000D;
                    }
                    break;
            }
            throw new InvalidOperationException("Enumerator is not on a collection element.");
        }

        private void CheckEnumVersion(int version)
        {
            if (version != this._version)
            {
                throw new InvalidOperationException("Enumerator invalidated by modification to collection.");
            }
        }

        private void CheckTargetArray(Array array, int arrayIndex)
        {
            int num = 9;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentException("Argument section must be large enough for collection.", "array");

                case 1:
                    throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");

                case 2:
                    if (arrayIndex < array.Length)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 8;
                    }
                    goto Label_000D;

                case 3:
                    if (array.Rank <= 1)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 7;
                    }
                    goto Label_000D;

                case 4:
                    if (this._count <= (array.Length - arrayIndex))
                    {
                        return;
                    }
                    num = 0;
                    goto Label_000D;

                case 5:
                    if (arrayIndex >= 0)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;

                case 6:
                    break;

                case 7:
                    throw new ArgumentException("Argument cannot be multidimensional.", "array");

                case 8:
                    throw new ArgumentException("Argument must be less than array length.", "arrayIndex");

                default:
                    if (array == null)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentNullException("array");
        }

        public virtual void Clear()
        {
            goto Label_0003;
           
        Label_0003:
            if (this._count == 0)
            {
                return;
            }
            this._version++;
            Array.Clear(this._array, 0, this._count);
            this._count = 0;
        }

        public virtual object Clone()
        {
            SchemaObjectBaseCollection bases;
            goto Label_0003;
           
        Label_0003:
            bases = new SchemaObjectBaseCollection(this._count);
            Array.Copy(this._array, 0, bases._array, 0, this._count);
            bases._count = this._count;
            bases._version = this._version;
            return bases;
        }

        public virtual bool Contains(SchemaObjectBase value)
        {
            return (this.IndexOf(value) >= 0);
        }

        public bool Contains(string name)
        {
            return (this.IndexOf(name) >= 0);
        }

        public virtual void CopyTo(SchemaObjectBase[] array)
        {
            // This item is obfuscated and can not be translated.
             
            this.CheckTargetArray(array, 0);
            Array.Copy(this._array, array, this._count);
        }

        public virtual void CopyTo(SchemaObjectBase[] array, int arrayIndex)
        {
            // This item is obfuscated and can not be translated.
            
            this.CheckTargetArray(array, arrayIndex);
            Array.Copy(this._array, 0, array, arrayIndex, this._count);
        }

        private void EnsureCapacity(int minimum)
        {
            // This item is obfuscated and can not be translated.
        }

        public virtual ISchemaObjectBaseEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public virtual int IndexOf(SchemaObjectBase value)
        {
            return Array.IndexOf<SchemaObjectBase>(this._array, value, 0, this._count);
        }

        public int IndexOf(string name)
        {
            int num;
            goto Label_0026;
           
        Label_0026:
            num = 0;
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (num < this.Count)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 1:
                case 4:
                    num2 = 0;
                    goto Label_0002;

                case 2:
                    return num;

                case 3:
                    if (string.Compare(this[num].Name, name, true) != 0)
                    {
                        num++;
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 5:
                    return -1;
            }
            goto Label_0026;
        }

        public virtual void Insert(int index, SchemaObjectBase value)
        {
            // This item is obfuscated and can not be translated.
            int num = 9;
        Label_000D:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    Array.Copy(this._array, index, this._array, index + 1, this._count - index);
                    num = 3;
                    goto Label_000D;

                case 2:
                    if (index >= this._count)
                    {
                        goto Label_0161;
                    }
                    num = 1;
                    goto Label_000D;

                case 3:
                    goto Label_0161;

                case 4:
                    this.EnsureCapacity(this._count + 1);
                    num = 0;
                    goto Label_000D;

                case 5:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 6:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot exceed Count.");

                case 7:
                    if (this._count != this._array.Length)
                    {
                        break;
                    }
                    num = 4;
                    goto Label_000D;

                case 8:
                    if (index <= this._count)
                    {
                        num = 7;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 8;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;
            }
            
            this._version++;
            num = 2;
            goto Label_000D;
        Label_0161:
            this._array[index] = value;
            this._count++;
        }

        public static SchemaObjectBaseCollection ReadOnly(SchemaObjectBaseCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new ReadOnlyList(collection);
        }

        public virtual void Remove(SchemaObjectBase value)
        {
            int num;
            goto Label_001A;
           
        Label_001A:
            num = this.IndexOf(value);
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (num < 0)
                    {
                        break;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    this.RemoveAt(num);
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_001A;
            }
        }

        public virtual void RemoveAt(int index)
        {
            // This item is obfuscated and can not be translated.
        Label_0017:
            this.ValidateIndex(index);
            this._version++;
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (index >= --this._count)
                    {
                        break;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    Array.Copy(this._array, index + 1, this._array, index, this._count - index);
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    
                    break;

                default:
                    goto Label_0017;
            }
            this._array[this._count] = null;
        }

        public virtual void RemoveRange(int index, int count)
        {
            int num = 1;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (count != 0)
                    {
                        break;
                    }
                    num = 4;
                    goto Label_000D;

                case 2:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 3:
                    goto Label_0173;

                case 4:
                    return;

                case 5:
                    if (count >= 0)
                    {
                        num = 8;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 6:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 7:
                    Array.Copy(this._array, index + count, this._array, index, this._count - index);
                    num = 3;
                    goto Label_000D;

                case 8:
                    if ((index + count) <= this._count)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_000D;

                case 9:
                    if (index >= this._count)
                    {
                        goto Label_0173;
                    }
                    num = 7;
                    goto Label_000D;

                case 10:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                default:
                    if (index < 0)
                    {
                        num = 10;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;
                   
                    
            }
            this._version++;
            this._count -= count;
            num = 9;
            goto Label_000D;
        Label_0173:
            Array.Clear(this._array, this._count, count);
        }

        public virtual void Reverse()
        {
            // This item is obfuscated and can not be translated.
            if (this._count > 1)
            {
                
                this._version++;
                Array.Reverse(this._array, 0, this._count);
            }
        }

        public virtual void Reverse(int index, int count)
        {
            int num = 7;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 1:
                    if (this._count > 1)
                    {
                        break;
                    }
                    num = 6;
                    goto Label_000D;

                case 2:
                    if (count >= 0)
                    {
                        num = 9;
                    }
                    else
                    {
                        num = 8;
                    }
                    goto Label_000D;

                case 3:
                    num = 1;
                    goto Label_000D;

                case 4:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 3;
                    goto Label_000D;

                case 5:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 6:
                    return;

                case 8:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 9:
                    if ((index + count) <= this._count)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;
                   
            }
            this._version++;
            Array.Reverse(this._array, index, count);
        }

        public virtual void Sort()
        {
            goto Label_0003;
           
        Label_0003:
            if (this._count <= 1)
            {
                return;
            }
            this._version++;
            Array.Sort<SchemaObjectBase>(this._array, 0, this._count);
        }

        public virtual void Sort(IComparer comparer)
        {
            // This item is obfuscated and can not be translated.
            if (this._count <= 1)
            {
                if (0 <= (0 + 2))
                {
                    return;
                }
               
            }
            this._version++;
            Array.Sort(this._array, 0, this._count, comparer);
        }

        public virtual void Sort(int index, int count, IComparer comparer)
        {
            int num = 6;
        Label_000D:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    if (count >= 0)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;

                case 2:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 3:
                    if ((index + count) <= this._count)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 4:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 7;
                    goto Label_000D;

                case 5:
                    return;

                case 7:
                    num = 8;
                    goto Label_000D;

                case 8:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Sort(this._array, index, count, comparer);
                        return;
                    }
                    num = 5;
                    goto Label_000D;

                case 9:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                default:
                    if (index >= 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 9;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");
        }

        public static SchemaObjectBaseCollection Synchronized(SchemaObjectBaseCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new SyncList(collection);
        }

        void ICollection<SchemaObjectBase>.Add(SchemaObjectBase item)
        {
            this.Add(item);
        }

        bool ICollection<SchemaObjectBase>.Remove(SchemaObjectBase item)
        {
            if (this.Contains(item.Name))
            {
                this.Remove(item);
                return true;
               
            }
            return false;
        }

        IEnumerator<SchemaObjectBase> IEnumerable<SchemaObjectBase>.GetEnumerator()
        {
            return new List<SchemaObjectBase>(this.ToArray()).GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CopyTo((SchemaObjectBase[]) array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((SchemaObjectBase) value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((SchemaObjectBase) value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((SchemaObjectBase) value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (SchemaObjectBase) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((SchemaObjectBase) value);
        }

        public virtual SchemaObjectBase[] ToArray()
        {
            // This item is obfuscated and can not be translated.
            
            SchemaObjectBase[] destinationArray = new SchemaObjectBase[this._count];
            Array.Copy(this._array, destinationArray, this._count);
            return destinationArray;
        }

        public override string ToString()
        {
            // This item is obfuscated and can not be translated.
            StringBuilder builder;
            int num;
            int num2;
            goto Label_0032;
        Label_0002:
           
            switch (num2)
            {
                case 0:
                    builder.Append(", ");
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    goto Label_0047;

                case 2:
                    if (num < this.Count)
                    {
                        builder.AppendFormat("\"{0}\"", this[num].ToString().Replace("\"", "\"\""));
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    return builder.ToString();

                case 4:
                    if (num >= (this.Count - 1))
                    {
                        goto Label_0047;
                    }
                    num2 = 0;
                    goto Label_0002;

                case 5:
                case 6:
                    num2 = 2;
                    goto Label_0002;
            }
        Label_0032:
            builder = new StringBuilder();
            num = 0;
            num2 = 5;
            goto Label_0002;
        Label_0047:
            num++;
            num2 = 6;
            goto Label_0002;
        }

        public virtual void TrimToSize()
        {
            this.Capacity = this._count;
        }

        private void ValidateIndex(int index)
        {
            // This item is obfuscated and can not be translated.
            int num = 3;
        Label_0018:
            switch (num)
            {
                case 0:
                    if (index < this._count)
                    {
                        return;
                    }
                    num = 2;
                    goto Label_0018;

                case 1:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 2:
                    throw new ArgumentOutOfRangeException("index", index, "Argument must be less than Count.");

                case 3:
                    
                    break;
            }
            if (index < 0)
            {
                num = 1;
            }
            else
            {
                num = 0;
            }
            goto Label_0018;
        }

        public virtual int Capacity
        {
            get
            {
                return this._array.Length;
            }
            set
            {
                int num = 0;
            Label_000D:
                switch (num)
                {
                    case 1:
                        if (value >= this._count)
                        {
                            num = 4;
                        }
                        else
                        {
                            num = 2;
                        }
                        goto Label_000D;

                    case 2:
                        throw new ArgumentOutOfRangeException("Capacity", value, "Value cannot be less than Count.");

                    case 3:
                        this._array = new SchemaObjectBase[0x10];
                        return;

                    case 4:
                        if (value != 0)
                        {
                            SchemaObjectBase[] destinationArray = new SchemaObjectBase[value];
                            Array.Copy(this._array, destinationArray, this._count);
                            this._array = destinationArray;
                            return;
                        }
                        num = 3;
                        goto Label_000D;

                    case 5:
                        return;

                    default:
                        if (value != this._array.Length)
                        {
                            num = 1;
                            goto Label_000D;
                        }
                        break;
                       
                }
                num = 5;
                goto Label_000D;
            }
        }

        public virtual int Count
        {
            get
            {
                return this._count;
            }
        }

        protected virtual SchemaObjectBase[] InnerArray
        {
            get
            {
                return this._array;
            }
        }

        public virtual bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public SchemaObjectBase this[string name]
        {
            get
            {
                int index = this.IndexOf(name);
                if (index >= 0)
                {
                    return this[index];
                   
                }
                return null;
            }
        }

        public virtual SchemaObjectBase this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this._array[index];
            }
            set
            {
                // This item is obfuscated and can not be translated.
               
                this.ValidateIndex(index);
                this._version++;
                this._array[index] = value;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return this;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (SchemaObjectBase) value;
            }
        }

        [Serializable]
        private sealed class Enumerator : ISchemaObjectBaseEnumerator, IEnumerator
        {
            private readonly SchemaObjectBaseCollection _collection;
            private int _index;
            private readonly int _version;

            internal Enumerator(SchemaObjectBaseCollection collection)
            {
                this._collection = collection;
                this._version = collection._version;
                this._index = -1;
            }

            public bool MoveNext()
            {
                // This item is obfuscated and can not be translated.
                
                this._collection.CheckEnumVersion(this._version);
                return (++this._index < this._collection.Count);
            }

            public void Reset()
            {
                this._collection.CheckEnumVersion(this._version);
                this._index = -1;
            }

            public SchemaObjectBase Current
            {
                get
                {
                    goto Label_0003;
                   
                Label_0003:
                    this._collection.CheckEnumIndex(this._index);
                    this._collection.CheckEnumVersion(this._version);
                    return this._collection[this._index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }

        [Serializable]
        private sealed class ReadOnlyList : SchemaObjectBaseCollection
        {
            private SchemaObjectBaseCollection _collection;

            internal ReadOnlyList(SchemaObjectBaseCollection collection) : base(SchemaObjectBaseCollection.Tag.Default)
            {
                this._collection = collection;
            }

            public override int Add(SchemaObjectBase value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(SchemaObjectBaseCollection collection)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(SchemaObjectBase[] array)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int BinarySearch(SchemaObjectBase value)
            {
                return this._collection.BinarySearch(value);
            }

            public override void Clear()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override object Clone()
            {
                return new SchemaObjectBaseCollection.ReadOnlyList((SchemaObjectBaseCollection) this._collection.Clone());
            }

            public override bool Contains(SchemaObjectBase value)
            {
                return this._collection.Contains(value);
            }

            public override void CopyTo(SchemaObjectBase[] array)
            {
                this._collection.CopyTo(array);
            }

            public override void CopyTo(SchemaObjectBase[] array, int arrayIndex)
            {
                this._collection.CopyTo(array, arrayIndex);
            }

            public override ISchemaObjectBaseEnumerator GetEnumerator()
            {
                return this._collection.GetEnumerator();
            }

            public override int IndexOf(SchemaObjectBase value)
            {
                return this._collection.IndexOf(value);
            }

            public override void Insert(int index, SchemaObjectBase value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Remove(SchemaObjectBase value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void RemoveAt(int index)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void RemoveRange(int index, int count)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Reverse()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Reverse(int index, int count)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort(IComparer comparer)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort(int index, int count, IComparer comparer)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override SchemaObjectBase[] ToArray()
            {
                return this._collection.ToArray();
            }

            public override void TrimToSize()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int Capacity
            {
                get
                {
                    return this._collection.Capacity;
                }
                set
                {
                    throw new NotSupportedException("Read-only collections cannot be modified.");
                }
            }

            public override int Count
            {
                get
                {
                    return this._collection.Count;
                }
            }

            protected override SchemaObjectBase[] InnerArray
            {
                get
                {
                    return this._collection.InnerArray;
                }
            }

            public override bool IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                    return this._collection.IsSynchronized;
                }
            }

            public override SchemaObjectBase this[int index]
            {
                get
                {
                    return this._collection[index];
                }
                set
                {
                    throw new NotSupportedException("Read-only collections cannot be modified.");
                }
            }

            public override object SyncRoot
            {
                get
                {
                    return this._collection.SyncRoot;
                }
            }
        }

        [Serializable]
        private sealed class SyncList : SchemaObjectBaseCollection
        {
            private SchemaObjectBaseCollection _collection;
            private object _root;

            internal SyncList(SchemaObjectBaseCollection collection) : base(SchemaObjectBaseCollection.Tag.Default)
            {
                this._root = collection.SyncRoot;
                this._collection = collection;
            }

            public override int Add(SchemaObjectBase value)
            {
                // This item is obfuscated and can not be translated.
                int num;
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    num = this._collection.Add(value);
                }
                finally
                {
                    
                    Monitor.Exit(obj2);
                }
                return num;
            }

            public override void AddRange(SchemaObjectBaseCollection collection)
            {
                lock (this._root)
                {
                    this._collection.AddRange(collection);
                }
                return;
               
            }

            public override void AddRange(SchemaObjectBase[] array)
            {
                // This item is obfuscated and can not be translated.
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.AddRange(array);
                }
                finally
                {
                    
                    Monitor.Exit(obj2);
                }
            }

            public override int BinarySearch(SchemaObjectBase value)
            {
                // This item is obfuscated and can not be translated.
                int num;
                lock (this._root)
                {
                    num = this._collection.BinarySearch(value);
                }
               
                return num;
            }

            public override void Clear()
            {
                // This item is obfuscated and can not be translated.
                lock (this._root)
                {
                    
                    this._collection.Clear();
                }
            }

            public override object Clone()
            {
                //object obj2;
                lock (this._root)
                {
                    return new SchemaObjectBaseCollection.SyncList((SchemaObjectBaseCollection) this._collection.Clone());
                }
               
                //return obj2;
            }

            public override bool Contains(SchemaObjectBase value)
            {
                // This item is obfuscated and can not be translated.
                 
                lock (this._root)
                {
                    return this._collection.Contains(value);
                }
            }

            public override void CopyTo(SchemaObjectBase[] array)
            {
                // This item is obfuscated and can not be translated.
                
                lock (this._root)
                {
                    this._collection.CopyTo(array);
                }
            }

            public override void CopyTo(SchemaObjectBase[] array, int arrayIndex)
            {
                lock (this._root)
                {
                    goto Label_0012;
                   
                Label_0012:
                    this._collection.CopyTo(array, arrayIndex);
                }
            }

            public override ISchemaObjectBaseEnumerator GetEnumerator()
            {
                ISchemaObjectBaseEnumerator enumerator;
                object obj2;
                goto Label_0003;
               
            Label_0003:
                Monitor.Enter(obj2 = this._root);
                try
                {
                    enumerator = this._collection.GetEnumerator();
                }
                finally
                {
                    Monitor.Exit(obj2);
                }
                return enumerator;
            }

            public override int IndexOf(SchemaObjectBase value)
            {
                int index;
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    index = this._collection.IndexOf(value);
                }
                finally
                {
                    goto Label_0021;
                   
                Label_0021:
                    Monitor.Exit(obj2);
                }
                return index;
            }

            public override void Insert(int index, SchemaObjectBase value)
            {
                lock (this._root)
                {
                    this._collection.Insert(index, value);
                }
                return;
               
            }

            public override void Remove(SchemaObjectBase value)
            {
                lock (this._root)
                {
                    this._collection.Remove(value);
                }
                return;
               
            }

            public override void RemoveAt(int index)
            {
                 
                lock (this._root)
                {
                    this._collection.RemoveAt(index);
                }
            }

            public override void RemoveRange(int index, int count)
            {
                object obj2;
                goto Label_0003;
               
            Label_0003:
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.RemoveRange(index, count);
                }
                finally
                {
                    Monitor.Exit(obj2);
                }
            }

            public override void Reverse()
            {
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.Reverse();
                }
                finally
                {
                    goto Label_001F;
                   
                Label_001F:
                    Monitor.Exit(obj2);
                }
            }

            public override void Reverse(int index, int count)
            {
                object obj2;
                goto Label_0003;
               
            Label_0003:
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.Reverse(index, count);
                }
                finally
                {
                    Monitor.Exit(obj2);
                }
            }

            public override void Sort()
            {
                // This item is obfuscated and can not be translated.
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.Sort();
                }
                finally
                {
                    
                    Monitor.Exit(obj2);
                }
            }

            public override void Sort(IComparer comparer)
            {
                lock (this._root)
                {
                    goto Label_0012;
                   
                Label_0012:
                    this._collection.Sort(comparer);
                }
            }

            public override void Sort(int index, int count, IComparer comparer)
            {
                // This item is obfuscated and can not be translated.
                lock (this._root)
                {
                    this._collection.Sort(index, count, comparer);
                }
                if (7 <= (7 + 5))
                {
                    return;
                }   
            }

            public override SchemaObjectBase[] ToArray()
            {
                // This item is obfuscated and can not be translated.
                lock (this._root)
                {
                    
                    return this._collection.ToArray();
                }
            }

            public override void TrimToSize()
            {
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.TrimToSize();
                }
                finally
                {
                    goto Label_001F;
                   
                Label_001F:
                    Monitor.Exit(obj2);
                }
            }

            public override int Capacity
            {
                get
                {
                    int num;
                    lock (this._root)
                    {
                        goto Label_0012;
                       
                    Label_0012:
                        num = this._collection.Capacity;
                    }
                    return num;
                }
                set
                {
                    lock (this._root)
                    {
                        goto Label_0012;
                       
                    Label_0012:
                        this._collection.Capacity = value;
                    }
                }
            }

            public override int Count
            {
                get
                {
                    // This item is obfuscated and can not be translated.
                    lock (this._root)
                    {
                       
                        return this._collection.Count;
                    }
                }
            }

            protected override SchemaObjectBase[] InnerArray
            {
                get
                {
                    // This item is obfuscated and can not be translated.
                    lock (this._root)
                    {
                        
                        return this._collection.InnerArray;
                    }
                }
            }

            public override bool IsFixedSize
            {
                get
                {
                    return this._collection.IsFixedSize;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return this._collection.IsReadOnly;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                    return true;
                }
            }

            public override SchemaObjectBase this[int index]
            {
                get
                {
                    // This item is obfuscated and can not be translated.
                    
                    lock (this._root)
                    {
                        return this._collection[index];
                    }
                }
                set
                {
                    object obj2;
                    goto Label_0003;
                   
                Label_0003:
                    Monitor.Enter(obj2 = this._root);
                    try
                    {
                        this._collection[index] = value;
                    }
                    finally
                    {
                        Monitor.Exit(obj2);
                    }
                }
            }

            public override object SyncRoot
            {
                get
                {
                    return this._root;
                }
            }
        }

        private enum Tag
        {
            Default
        }
    }
}

