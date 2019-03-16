using MvvmExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmExtensions
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
		public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Relationship between properties and those who depend upon them
        /// </summary>
		private Dictionary<string, List<string>> propertyDependencies;

		public PropertyChangedImplementation()
		{
            RegisterDependencies();
		}

        protected void RegisterDependencies()
        {
            propertyDependencies = new Dictionary<string, List<string>>();
            var properties = GetType().GetProperties();

            foreach (var classProp in properties)
            {
                // Check if property is marked as being dependent on other properties
                var attributes = classProp.GetCustomAttributes(typeof(DependsOnAttribute), false);
                foreach (DependsOnAttribute attribute in attributes)
                {
                    if (attribute.Properties != null) // Attribute contains list of properties that current property is dependent upon
                    {
                        foreach (var propDependency in attribute.Properties)
                        {
                            if (!propertyDependencies.ContainsKey(propDependency))
                                propertyDependencies.Add(propDependency, new List<string>());

                            propertyDependencies[propDependency].Add(classProp.Name);
                        }
                    }
                }
            }
        }

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if ((propertyChanged != null))
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));

				// Call NotifyPropertyChanged for all properties that are dependant on current property
				if (propertyDependencies.ContainsKey(propertyName))
				{
					foreach (var prop in propertyDependencies[propertyName])
						NotifyPropertyChanged(prop);
				}
			}
		}
	}
}
