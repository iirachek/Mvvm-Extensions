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
    /// 
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
	}
}
