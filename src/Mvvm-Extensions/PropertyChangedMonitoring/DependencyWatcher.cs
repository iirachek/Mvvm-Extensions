using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MvvmExtensions.PropertyChangedMonitoring
{
    // Todo: Find a way to make this work without requiring object to do anything besides calling Register()
    /// <summary>
    /// Class that allows to register classes that implement INotifyPropertyChanged for 
    /// automatic dependent properties update.
    /// Properties marked with DependsOn attribute are automatically notified
    /// when specificied properties invoke PropertyChanged event 
    /// 
    /// Must be used in conjunction with <see cref="DependsOnAttribute"/>
    /// </summary>
    public class DependencyWatcher : IDisposable
    {
        private readonly INotifyPropertyChanged target;
        private readonly Dictionary<string, List<string>> dependencies;
        private readonly Action<string> notifyPropertyChangedDelegate;

        public static DependencyWatcher Register(INotifyPropertyChanged target, Action<string> notifyPropertyChangedDelegate)
        {
            return new DependencyWatcher(target, notifyPropertyChangedDelegate);
        }

        private DependencyWatcher(INotifyPropertyChanged target, Action<string> notifyPropertyChangedDelegate)
        {
            this.target = target;
            dependencies = DependencyHelper.GetDependenciesDictionary(target);
            this.notifyPropertyChangedDelegate = notifyPropertyChangedDelegate;

            target.PropertyChanged += Target_OnPropertyChanged;
        }

        private void Target_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (dependencies.ContainsKey(e.PropertyName))
            {
                foreach (var dependentProperty in dependencies[e.PropertyName])
                    notifyPropertyChangedDelegate?.Invoke(dependentProperty);
            }
        }

        // Public implementation of Dispose pattern callable by consumers.
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                target.PropertyChanged -= Target_OnPropertyChanged;
            }

            disposed = true;
        }
    }
}
