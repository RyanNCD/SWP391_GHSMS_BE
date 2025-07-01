using Repository.DTO;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BlogService(BlogRepository _blogRepository) : IBlogService
    {
        public async Task<bool> CreateBlog(Guid userId, CreateBlogDTO request)
        {
            return await _blogRepository.CreateBlog(userId, request);
        }

        public async Task<bool> DeleteBlog(Guid blogId)
        {
            return await _blogRepository.DeleteBlog(blogId);
        }

        public async Task<bool> EditBlog(Guid blogId, CreateBlogDTO request)
        {
            return await _blogRepository.EditBlogs(blogId, request);
        }

        public async Task<List<GetBlogsDTO>> GetBlogs()
        {
            return await _blogRepository.GetBlogs();
        }

        public async Task<GetBlogDTO> GetBlog(Guid blogId)
        {
            return await _blogRepository.GetBlog(blogId);
        }
    }
}
