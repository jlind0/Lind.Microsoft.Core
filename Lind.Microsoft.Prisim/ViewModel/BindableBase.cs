using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Lind.Microsoft.Core;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Lind.Microsoft.Prism.ViewModel
{
    public class BindableBase : INotifyPropertyChanged
    {
        public NonNullReference<IDispatcher> Dispatcher { get; set; }
        public BindableBase(IDispatcher dispatcher) //comonly used, in theory could change with new arch (moving VM's between applications while retaining state)
        {
            this.Dispatcher += dispatcher;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam>
        /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }
        
    }
}
