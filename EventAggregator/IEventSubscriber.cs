using System;

namespace MvvmExtensions.EventAggregator
{
    [Obsolete("Use Messenger class from MVVM Light package as it provides better implementation")]
    [System.Reflection.ObfuscationAttribute(ApplyToMembers = true)]
    public interface IEventSubscriber<TEventType>
    {
        void OnEventReceived(TEventType eventObject);
    }
}
