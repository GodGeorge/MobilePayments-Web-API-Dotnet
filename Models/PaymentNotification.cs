using System.ComponentModel.DataAnnotations;

namespace PaymentNotifications.MobileBux.WebApi.Controllers
{
    public class PaymentNotification
    {

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

    }
}
