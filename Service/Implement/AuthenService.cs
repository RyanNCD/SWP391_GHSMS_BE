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

        public async Task<AuthenticationModel> LoginWithEmailPasswordAsync(LoginRequest request)
        {
            var normalizedEmail = NormalizeEmail(request.Email);

            var user = await _unitOfWork.GetRepository<User>()
                .Entities
                .Include(u => u.ConsultantProfile)
                .FirstOrDefaultAsync(u => u.Email == normalizedEmail);

            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "Invalid email or password");
            }

            if (user.DeletedTime.HasValue)
            {
                throw new ErrorException(StatusCodes.Status403Forbidden, ResponseCodeConstants.FORBIDDEN, "User account is deleted");
            }

            return await _tokenGenerator.CreateToken(user, _jwtSettings);
        }

        public async Task<UserResponseModel> RegisterAsync(UserRegistrationRequest request)
        {
            var nomalizedEmail = NormalizeEmail(request.Email);

            var userRepo = _unitOfWork.GetRepository<User>();
            var exists = await userRepo.Entities.AsNoTracking().AnyAsync(u => u.Email == nomalizedEmail);

            if (exists)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ResponseCodeConstants.DUPLICATE, "Email is already registered");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = nomalizedEmail,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender.ToString(),
                Role = request.Role == Role.Consultant ? Role.Customer.ToString() : Role.Customer.ToString(),
                ConsultantStatus = request.Role == Role.Consultant ? ConsultantStatus.Pending.ToString() : null,
            };

            await userRepo.InsertAsync(user);
            await _unitOfWork.SaveAsync();

            return user.ToUserDto();
        }

      

    }
}
