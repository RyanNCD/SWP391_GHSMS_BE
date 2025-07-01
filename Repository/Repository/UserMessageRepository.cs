using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Models;

namespace Repository.Repository;

public class UserMessageRepository : GenericRepository<UserMessage>
{
    public async Task<List<UserMessage>?> GetUserMessageBySenderAndReceiverAsync(string senderEmail, string receiverEmail)
    {
        return await _context.UserMessages
            .OrderBy(x => x.SentAt)
            .Where(x => (x.SenderEmail == senderEmail && x.ReceiverEmail == receiverEmail) ||
                                      (x.SenderEmail == receiverEmail && x.ReceiverEmail == senderEmail)).ToListAsync();
    }
}