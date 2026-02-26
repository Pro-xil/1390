using System;
using System.Collections.Generic;

namespace OfflineVoxelMining.Events
{
    public interface IGameEvent { }

    public static class GameEventBus
    {
        private static readonly Dictionary<Type, Delegate> Subscribers = new();

        public static void Subscribe<T>(Action<T> callback) where T : IGameEvent
        {
            var key = typeof(T);
            if (Subscribers.TryGetValue(key, out var existing))
            {
                Subscribers[key] = Delegate.Combine(existing, callback);
                return;
            }

            Subscribers[key] = callback;
        }

        public static void Unsubscribe<T>(Action<T> callback) where T : IGameEvent
        {
            var key = typeof(T);
            if (!Subscribers.TryGetValue(key, out var existing))
            {
                return;
            }

            var updated = Delegate.Remove(existing, callback);
            if (updated == null)
            {
                Subscribers.Remove(key);
                return;
            }

            Subscribers[key] = updated;
        }

        public static void Publish<T>(T gameEvent) where T : IGameEvent
        {
            var key = typeof(T);
            if (!Subscribers.TryGetValue(key, out var handler))
            {
                return;
            }

            (handler as Action<T>)?.Invoke(gameEvent);
        }

        public static void Reset() => Subscribers.Clear();
    }
}
