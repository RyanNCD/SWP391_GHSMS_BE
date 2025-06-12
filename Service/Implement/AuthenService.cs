using Repository.Models;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class AuthenService : IAuthenService
    {
        private readonly AuthenRepository _authenRepository;

        public AuthenService(AuthenRepository authenRepository)
        {
            _authenRepository = authenRepository;
        }

        public async Task<string> LoginWithToken(string email, string password)
        {
            return await _authenRepository.LoginWithToken(email, password);
        }

        public async Task<User> RegisterAsync(string fullName, string email, string password, int roleId, string phoneNumber, string gender)
        {
            return await _authenRepository.RegisterAsync(fullName, email, password, roleId, phoneNumber, gender);
        }
    }
}
