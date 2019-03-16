using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvvmExtensions.EventAggregator
{
    /// <summary>
    /// Thread-safe event aggregator implementation
    /// </summary>
    [Obsolete("Use Messenger class from MVVM Light package as it provides better implementation")]
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<TokenSubscriber>> subscribers = new Dictionary<Type, List<TokenSubscriber>>();
        private readonly object subscribersLock = new object();

        /// <summary>
        /// Publish event to all subscribers of this <see cref="EventAggregator"/> with specified <see cref="token"/>
        /// </summary>
        /// <param name="token">Only subscribers with matching token are notified</param>
        public Task PublishEventAsync<TEventType>(TEventType eventObject, int? token = null)
        {
            return Task.Run(() =>
            {
                lock (subscribersLock)
                {
                    var eventType = eventObject.GetType();
                    var eventSubscribers = GetSubscribers(eventType);

                    List<TokenSubscriber> aliveSubs = new List<TokenSubscriber>();
                    List<TokenSubscriber> disposedSubs = new List<TokenSubscriber>();

                    // Sort all subscribers into collections of alive and disposed subscribers
                    foreach (var sub in eventSubscribers)
                    {
                        if (sub.Subscriber.IsAlive)
                            aliveSubs.Add(sub);
                        else
                            disposedSubs.Add(sub);
                    }

                    // Notify all alive subscribers about event
                    foreach (var sub in aliveSubs)
                    {
                        if (sub.Token == token)
                            (sub.Subscriber.Target as IEventSubscriber<TEventType>)?.OnEventReceived(eventObject);
                    }

                    // Remove all disposed subscribers from subscribers collection
                    if (disposedSubs.Any())
                    {
                        lock (subscribersLock)
                        {
                            foreach (var sub in disposedSubs)
                            {
                                subscribers[eventType].Remove(sub);
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Subscribe to events published through this <see cref="EventAggregator"/>
        /// </summary>
        /// <param name="subscriber">Object that subscribes to aggregator events</param>
        /// <param name="token">Only subscribers with matching token are notified</param>
        public void SubsribeToEvents(object subscriber, int? token = null)
        {
            lock (subscribersLock)
            {
                var subscriberType = subscriber.GetType();
                var interfaces = subscriberType.GetInterfaces();

                foreach (var i in interfaces)
                {
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventSubscriber<>))
                    {
                        var eventType = i.GetGenericArguments()[0];
                        var reference = new WeakReference(subscriber);
                        var scopedSubscriber = new TokenSubscriber(reference, token);
                        GetSubscribers(eventType).Add(scopedSubscriber);
                    }
                }
            }
        }

        /// <summary>
        /// Get list of subscribers for particular event type
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <returns>Returns list of event subscribers</returns>
        private List<TokenSubscriber> GetSubscribers(Type eventType)
        {
            lock (subscribersLock)
            {
                // Check if a collection of subscribers has been created for this event type
                if (!subscribers.ContainsKey(eventType))
                    subscribers.Add(eventType, new List<TokenSubscriber>());

                return subscribers[eventType];
            }
        }
    }
}
