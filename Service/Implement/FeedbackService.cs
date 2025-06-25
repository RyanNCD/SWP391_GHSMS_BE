using Repository.DTO;
using Repository.Models;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackRepository _repository;

        public FeedbackService(FeedbackRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(FeedbackDTO dto)
        {
            var feedback = new Feedback
            {
                UserId = dto.UserId,
                TestBookingId = dto.TestBookingId,
                ConsultationBookingId = dto.ConsultationBookingId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreateAt = dto.CreateAt ?? DateTime.Now
            };

            return await _repository.CreateAsync(feedback);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var feedback = await _repository.GetByIdAsync(id);
            if (feedback == null)
                return 0;

            await _repository.RemoveAsync(feedback);
            await _repository.SaveAsync();
            return 1;
        }

        public async Task<List<FeedbackDTO>> GetAllAsync()
        {
            var feedbacks = await _repository.GetAllAsync();
            return feedbacks.Select(f => new FeedbackDTO
            {
                FeedbackId = f.FeedbackId,
                UserId = f.UserId,
                TestBookingId = f.TestBookingId,
                ConsultationBookingId = f.ConsultationBookingId,
                Rating = f.Rating,
                Comment = f.Comment,
                CreateAt = f.CreateAt
            }).ToList();
        }

        public async Task<FeedbackDTO> GetByIdAsync(int id)
        {
            var f = await _repository.GetByIdAsync(id);
            if (f == null) return null;

            return new FeedbackDTO
            {
                FeedbackId = f.FeedbackId,
                UserId = f.UserId,
                TestBookingId = f.TestBookingId,
                ConsultationBookingId = f.ConsultationBookingId,
                Rating = f.Rating,
                Comment = f.Comment,
                CreateAt = f.CreateAt
            };
        }

        public async Task<int> UpdateAsync(FeedbackDTO dto)
        {
            var existing = await _repository.GetByIdAsync(dto.FeedbackId);
            if (existing == null) return 0;

            existing.UserId = dto.UserId;
            existing.TestBookingId = dto.TestBookingId;
            existing.ConsultationBookingId = dto.ConsultationBookingId;
            existing.Rating = dto.Rating;
            existing.Comment = dto.Comment;
            existing.CreateAt = dto.CreateAt ?? existing.CreateAt;

            return await _repository.UpdateAsync(existing);
        }
    }
}
