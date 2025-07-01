using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBlogService
    {
        public Task<bool> CreateBlog(Guid userId, CreateBlogDTO request);
        public Task<List<GetBlogsDTO>> GetBlogs();
        public Task<bool> EditBlog(Guid blogId, CreateBlogDTO request);
        public Task<bool> DeleteBlog(Guid blogId);
        public Task<GetBlogDTO> GetBlog(Guid blogId);

    }
}
