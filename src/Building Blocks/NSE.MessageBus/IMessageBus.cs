using EasyNetQ;
using EasyNetQ.Internals;
using NSE.Core.Messages.Integration;

namespace NSE.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        IAdvancedBus AdvancedBus { get; }

        void Publish<T>(T message) where T : IntegrationEvent;

        Task PublishASync<T>(T message) where T : IntegrationEvent;

        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

        void SubscribeASync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;

        TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> respond)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> respond)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;
        bool IsConnected { get; }

    }
}