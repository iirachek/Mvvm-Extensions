using MvvmExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmExtensions.PropertyChangedMonitoring
{
    /// <summary>
    /// This class has a special implementation of INotifyPropertyChanged that should be used together
    /// with the DependsOn attribute. Properties marked with DependsOn attribute are automatically notified
    /// when specificied property invokes NotifyPropertyChanged event 
    /// <br/>
    /// <br/>
    /// Must be used in conjunction with <see cref="DependsOnAttribute"/>
    /// </summary>
	public class PropertyChangedImplementation : INotifyPropertyChanged
	{
		private Dictionary<string, List<string>> propertyDependencies;

        public PropertyChangedImplementation()
		{
            RegisterDependencies();
		}

		public event PropertyChangedEventHandler PropertyChanged;

        protected void RegisterDependencies()
        {
            propertyDependencies = DependencyHelper.GetDependenciesDictionary(this);
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            DependencyHelper.NotifyDependencies(propertyName, propertyDependencies, NotifyPropertyChanged);
		}

        /// <summary>
        /// Assigns a new value to the property. Then, raises the PropertyChanged event if needed. 
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns>True if the PropertyChanged event has been raised, false otherwise. 
        /// The event is not raised if the old value is equal to the new value.</returns>
        protected bool Set<T>(ref T field, T newValue = default(T), [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
