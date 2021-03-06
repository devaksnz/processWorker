using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace MessageBusServiceProvider
{
    public class QueueMessageSender : IQueueMessageSender
    {
        private readonly IQueueClient _queueClient;
        private readonly ILogger _logger;

        public QueueMessageSender(IQueueClient queueClient, ILogger logger)
        {
            this._queueClient = queueClient;
            _logger = logger;
        }

        public async Task SendMessagesAsync(string messageSource)
        {
            this._logger.LogInformation($"Sending message : {messageSource}");
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(messageSource));
                await this._queueClient.SendAsync(message);
                this._logger.LogInformation($"Sent!");
            }
            catch (Exception exception)
            {
                this._logger.LogError($"{exception.Message}");
            }
        }
    }
}