using MvvmExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace MvvmExtensions.PropertyChangedMonitoring
{
    internal class DependencyHelper
    {
        /// <summary>
        /// Get dictionary that displays relations between properties and their dependency properties
        /// </summary>
        /// <param name="target">Object to analyze for dependencies</param>
        /// <returns>Return a dictionary where Key is a property name and Value is a list of property names that are dependent upon it/></returns>
        public static Dictionary<string, List<string>> GetDependenciesDictionary(object target)
        {
            if (target == null)
                throw new ArgumentNullException();

            var dependencies = new Dictionary<string, List<string>>();

            var properties = target.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(DependsOnAttribute), false);
                foreach (DependsOnAttribute attribute in attributes)
                {
                    foreach (var dependencyName in attribute.Dependencies)
                    {
                        if (!dependencies.ContainsKey(dependencyName))
                            dependencies.Add(dependencyName, new List<string>());

                        dependencies[dependencyName].Add(property.Name);
                    }
                }
            }

            return dependencies;
        }

        /// <summary>
        /// Invokes PropertyChanged event for all properties that specified current property as their dependency
        /// </summary>
        /// <param name="propertyName">Name of the property that changed</param>
        /// <param name="dependencies">Dictionary of properties dependencies</param>
        /// <param name="notifyPropertyChangedDelegate">Delegate for PropertyChanged event invocation</param>
        public static void NotifyDependencies(
            string propertyName, 
            Dictionary<string, List<string>> dependencies, 
            Action<string> notifyPropertyChangedDelegate)
        {
            if (dependencies.ContainsKey(propertyName))
            {
                foreach (var dependentProperty in dependencies[propertyName])
                    notifyPropertyChangedDelegate?.Invoke(dependentProperty);
            }
        }
    }
}
