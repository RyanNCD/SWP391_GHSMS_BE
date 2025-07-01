using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Models;

namespace Repository.Repository;

public class ConnectionRepository : GenericRepository<Connection>
{
    public async Task<Connection> GetConnectionByIdAsync(string connectionId)
    {
        return await _context.Connections
            .FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
    }
}