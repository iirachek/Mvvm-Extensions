using System;

namespace MvvmExtensions.EventAggregator
{
    [Obsolete("Use Messenger class from MVVM Light package as it provides better implementation")]
    internal class TokenSubscriber
    {
        public WeakReference Subscriber { get; set; }

        public int? Token { get; set; }

        public TokenSubscriber(WeakReference subscriberReference, int? token = null)
        {
            Subscriber = subscriberReference;
            Token = token;
        }
    }
}
