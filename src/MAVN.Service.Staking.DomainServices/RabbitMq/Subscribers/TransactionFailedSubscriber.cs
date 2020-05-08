﻿using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.PrivateBlockchainFacade.Contract.Events;
using MAVN.Service.Staking.Domain.RabbitMq.Handlers;

namespace MAVN.Service.Staking.DomainServices.RabbitMq.Subscribers
{
    public class TransactionFailedSubscriber : JsonRabbitSubscriber<TransactionFailedEvent>
    {
        private readonly ITransactionFailedEventHandler _handler;
        private readonly ILog _log;

        public TransactionFailedSubscriber(
            ITransactionFailedEventHandler handler,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _handler = handler;
            _log = logFactory.CreateLog(this);
        }


        protected override async Task ProcessMessageAsync(TransactionFailedEvent message)
        {
            await _handler.HandleAsync(message.OperationId);

            _log.Info("Handled transaction failed event", message);
        }
    }
}
