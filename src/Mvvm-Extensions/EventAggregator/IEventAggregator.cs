using System;
using System.Threading.Tasks;

namespace MvvmExtensions.EventAggregator
{
    [Obsolete("Use Messenger class from MVVM Light package as it provides better implementation")]
    public interface IEventAggregator
    {
        Task PublishEventAsync<TEventType>(TEventType eventObject, int? token = null);

        void SubsribeToEvents(object subscriber, int? token = null);
    }
}
