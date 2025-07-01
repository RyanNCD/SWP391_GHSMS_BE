using PayOSService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayOSService.Services
{
    public interface IPayOSService
    {
        Task<string> CreatePaymentAsync(CreatePaymentDTO createPaymentDTO);
    }
}
