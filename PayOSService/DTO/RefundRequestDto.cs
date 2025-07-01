using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayOSService.DTO
{
    public class RefundRequestDto
    {
        public long OrderCode { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string PaymentId { get; set; }
    }
}
