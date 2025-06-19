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
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackRepository _repository;
        public FeedbackService(FeedbackRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> CreateAsync(Feedback feedback)
        {
            return await _repository.CreateAsync(feedback);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var feedback = await _repository.GetByIdAsync(id);
            if (feedback == null)
            {
                return 0;
            }
             await _repository.RemoveAsync(feedback);
            await _repository.SaveAsync();
            return 1;
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Feedback feedback)
        {
            return await (_repository.UpdateAsync(feedback));
        }
    }
}
