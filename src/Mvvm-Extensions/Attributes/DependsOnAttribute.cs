using System;

namespace MvvmExtensions.Attributes
{
    /// <summary>
    /// Properties marked with DependsOn attribute are automatically updated
    /// when properties specified as parameters invoke NotifyPropertyChanged event 
    /// 
    /// Must be used in conjunction with <see cref="PropertyChangedImplementation"/>
    /// </summary>
    public sealed class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="DependsOnAttribute"/> class
        /// </summary>
        /// <param name="dependencies">Names of properties that are dependencies for current property</param>
        public DependsOnAttribute(params string[] dependencies)
        {
            Dependencies = dependencies;

            // Input parameter might be null, but for consumers it would be 
            // better to return an empty array instead of null
            if (Dependencies == null)
                Dependencies = new string[0];
        }

        /// <summary>
        /// Names of properties that are dependencies for current property
        /// </summary>
        public string[] Dependencies { get; }
    }
}
