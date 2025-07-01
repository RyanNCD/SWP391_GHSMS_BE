using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.CloudinaryService;
using Repository.DTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ConsultantsRepository(SWP391GHSMContext _wp391ghsmContext, ICloudinaryService _cloudinaryService) : GenericRepository<Consultant>
    {
        public async Task<bool> CreateConsultants(CreateConsultantsDTO request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // B1. Validate đầu vào
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "Request không được null");

                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new ArgumentException("Email không được bỏ trống");

                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new ArgumentException("Password không được bỏ trống");

                if (request.Avatar == null)
                    throw new ArgumentException("Avatar không được null");

                // B2. Kiểm tra email trùng
                var checkEmailAlready = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower());

                if (checkEmailAlready != null)
                {
                    return false;
                }

                // B3. Upload ảnh
                var avatarUrl = await _cloudinaryService.Upload(request.Avatar);

                // B4. Dùng RoleId đã xác nhận tồn tại
                var roleName = "Consutants";
                var getRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName.Equals(roleName));

                // B5. Tạo User
                var newUserId = Guid.NewGuid();
                var newUser = new User
                {
                    UserId = newUserId,
                    Email = request.Email,
                    FullName = request.FullName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    RoleId = getRole.RoleId,
                    Avatar = avatarUrl,
                    Gender = request.Gender,
                    CreateAt = DateTime.UtcNow,
                    IsActive = false
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync(); // ⬅ Bắt buộc lưu trước để có UserId thật

                // B6. Tạo Consultant gắn với User
                var consultant = new Consultant
                {
                    ConsultantId = Guid.NewGuid(),
                    UserId = newUserId,
                    Degree = request.Degree,
                    ExperienceYears = request.experienceYears,
                    Bio = request.Bio
                };

                await _context.Consultants.AddAsync(consultant);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"❌ Lỗi khi tạo consultant: {ex.Message}", ex);
            }
        }

        public async Task<List<GetConsultants>> GetConsultants()
        {
            var getConsultants = await _context.Consultants
                                               .Include(x => x.User)
                                               .Where(x => x.User.IsActive == true)
                                               .ToListAsync();

            var mapItem = getConsultants.Select(c => new GetConsultants
            {
                ConsultantId = c.ConsultantId,
                Degree = c.Degree,
                ExperienceYears = c.ExperienceYears,
                Bio = c.Bio
            }).ToList();

            return mapItem;

        }

        public async Task<GetConsultantDetail> GetConsultantDetail(Guid consultanId)
        {
            var getDetail = await _context.Consultants
                                          .Include(x => x.User)
                                          .FirstOrDefaultAsync(x => x.ConsultantId == consultanId);
            if (getDetail == null)
            {
                return null;
            }

            var mapItem = new GetConsultantDetail
            {
                consultantId = consultanId,
                Degree = getDetail.Degree,
                Bio = getDetail.Bio,
                ExperienceYears = getDetail.ExperienceYears,
                User = new UserDTO
                {
                    UserId = getDetail.UserId,
                    Email = getDetail.User.Email,
                    FullName = getDetail.User.FullName,
                    Address = getDetail.User.Address,
                    CreateAt = getDetail.User.CreateAt,
                    Gender = getDetail.User.Gender,
                    Avatar = getDetail.User.Avatar,
                    PhoneNumber = getDetail.User.PhoneNumber
                }
            };

            return mapItem;
        }


        public async Task<bool> DeleteConsultantDetail(Guid consultanId)
        {
            var checkDelete = await _context.Consultants
                                            .Include(x => x.User)
                                            .FirstOrDefaultAsync(x => x.ConsultantId == consultanId);
            if (checkDelete == null)
            {
                return false;
            }

            checkDelete.User.IsActive = false;

            _context.Consultants.Update(checkDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditConsultantDetail(Guid consultantId, CreateConsultantsDTO request)
        {
            var checkUpdate = await _context.Consultants.FirstOrDefaultAsync(x => x.ConsultantId == consultantId);
            if (checkUpdate == null)
            {
                return false;
            }

            checkUpdate.Degree = request.Degree;
            checkUpdate.ExperienceYears = request.experienceYears;
            checkUpdate.Bio = request.Bio;


            _context.Consultants.Update(checkUpdate);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
