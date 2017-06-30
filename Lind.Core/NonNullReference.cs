using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Lind.Core
{
    public interface INonNullReference : INotifyPropertyChanged, INotifyPropertyChanging { }
    public interface IONonNullReference<out T> : INonNullReference
        where T: class
    {
        T Value { get; }
    }
    public interface IINonNullReference<in T> : INonNullReference
        where T: class
    {
        T Value { set; }
    }
    public interface INonNullReference<T> : IONonNullReference<T>, IINonNullReference<T>
        where T: class
    { }
    public struct NonNullReference<T> : INonNullReference<T>
        where T : class
    {
        public NonNullReference(T initial)
        {
            value = initial ?? throw new NullReferenceException();
            PropertyChanged = null;
            PropertyChanging = null;
        }
        private T value;

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public T Value
        {
            get { return value; }
            set
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Value"));
                this.value = value ?? throw new NullReferenceException();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }
        public override bool Equals(object obj)
        {
            return value.Equals(obj);
        }
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(NonNullReference<T> reference, T value)
        {
            return reference.Equals(value);
        }
        public static bool operator !=(NonNullReference<T> reference, T value)
        {
            return !reference.Equals(value);
        }
        public static bool operator ==( T value, NonNullReference<T> reference)
        {
            return reference.Equals(value);
        }
        public static bool operator !=(T value, NonNullReference<T> reference)
        {
            return !reference.Equals(value);
        }
        public static NonNullReference<T> operator +(NonNullReference<T> reference, T value)
        {
            reference.Value = value;
            return reference;
        }
    }
}
