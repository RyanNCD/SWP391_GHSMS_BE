using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Models;

namespace Repository.Repository;

public class GroupRepository : GenericRepository<Group>
{
    public async Task<Group> GetGroupByNameAsync(string groupName)
    {
        return await _context.Groups
            .Include(g => g.Connections)
            .FirstOrDefaultAsync(g => g.Name == groupName);
    }
    public async Task<Group> GetGroupByConnectionIdAsync(string connectionId)
    {
        return await _context.Groups
            .Include(g => g.Connections)
            .FirstOrDefaultAsync(g => g.Connections.Any(c => c.ConnectionId == connectionId));
    }
}