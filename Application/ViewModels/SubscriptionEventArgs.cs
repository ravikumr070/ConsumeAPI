using System;

namespace Application.ViewModels
{
    public class SubscriptionEventArgs : EventArgs
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CcAddress { get; set; }
    }
}