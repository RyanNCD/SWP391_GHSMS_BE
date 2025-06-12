using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuthenService
    {
        Task<string> LoginWithToken(string email, string password);
        Task<User> RegisterAsync(string fullName, string email, string password, int roleId, string phoneNumber, string gender);
    }
}
