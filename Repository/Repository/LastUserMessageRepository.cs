using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Models;

namespace Repository.Repository;

public class LastUserMessageRepository : GenericRepository<LastUserMessage>
{
    public async Task<LastUserMessage?> GetLastUserMessageBySenderAndReceiverAsync(string senderEmail, string receiverEmail)
    {
        return await _context.LastUserMessages
            .FirstOrDefaultAsync(x => (x.SenderEmail == senderEmail && x.ReceiverEmail == receiverEmail) ||
                                      (x.SenderEmail == receiverEmail && x.ReceiverEmail == senderEmail)
            );
    }
}