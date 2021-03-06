using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;

namespace PaymentNotifications.MobileBux.WebApi.Controllers
{
    [ApiController]
    [Route("payment_notifications")]
    public class PaymentNotificationsController : ControllerBase
    {
  
        private readonly EventHubProducerClient _eventHubClient;
        private readonly ILogger _logger;

        public PaymentNotificationsController(ILogger<PaymentNotificationsController> logger, EventHubProducerClient eventHubClient)
        {
            _logger = logger;
            _eventHubClient = eventHubClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentNotification paymentNotification)
        {

            try
            {

                // Create Event
                var notificationEvent = new PaymentNotificationReceived(paymentNotification.Amount);
                var jsonString = JsonConvert.SerializeObject(notificationEvent);
                var data = new EventData(Encoding.UTF8.GetBytes(jsonString));
                data.Properties["content - type"] = "application/json";

                // Publish Event to EventHub
                using EventDataBatch eventBatch = await _eventHubClient.CreateBatchAsync();
                eventBatch.TryAdd(data);
                await _eventHubClient.SendAsync(eventBatch);
                _logger.LogInformation($"Payment Notification Received: {jsonString}");

                // Respond to Client
                Console.WriteLine($"Payment Received. Amount: {paymentNotification.Amount}");
                return StatusCode(202);
            }
            catch(Exception e)
            {
                _logger.LogError($"{e.GetType().FullName}: {e.Message}");
                return StatusCode(500);
            }

        }
    }
}
