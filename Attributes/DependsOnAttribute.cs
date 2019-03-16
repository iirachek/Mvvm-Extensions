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
        /// <param name="dp">Names of properties that are dependencies for current property</param>
        public DependsOnAttribute(params string[] dp)
        {
            Properties = dp;
        }

        /// <summary>
        /// Names of properties that are dependencies for current property
        /// </summary>
        public string[] Properties { get; }
    }
}
