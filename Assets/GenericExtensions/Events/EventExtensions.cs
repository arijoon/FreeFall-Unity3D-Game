using System;

namespace GenericExtensions.Events
{
    public static class EventExtensions
    {
        public static void SafeCall(this EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        public static void SafeCall<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}
