using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.CloudinaryService;
using Repository.DTO;
using Repository.Models;
using Repository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BlogRepository : GenericRepository<Blog>
    {
        private readonly SWP391GHSMContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        public BlogRepository(SWP391GHSMContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }


        public async Task<bool> CreateBlog(Guid userId, CreateBlogDTO request)
        {
            try
            {
                var createBlog = new Blog
                {
                    BlogId = Guid.NewGuid(),
                    Title = request.Title,
                    Content = HTMLUtil.Sanitize(request.Content),
                    CreateAt = DateTime.Now,
                    Image = await _cloudinaryService.Upload(request.Image),
                    AuthorId = userId
                };

                await _context.Blogs.AddAsync(createBlog);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<GetBlogsDTO>> GetBlogs()
        {
            var list = await _context.Blogs.ToListAsync();

            var mapItem = list.Select(b => new GetBlogsDTO
            {
                Id = b.BlogId,
                Title = b.Title,
                Image = b.Image
            }).ToList();

            return mapItem;
        }

        public async Task<bool> EditBlogs(Guid blogId, CreateBlogDTO request)
        {
            try
            {
                var checkUpdate = await _context.Blogs.FirstOrDefaultAsync(x => x.BlogId == blogId);
                if (checkUpdate == null)
                {
                    return false;
                }

                checkUpdate.Title = request.Title ?? checkUpdate.Title;
                checkUpdate.Content = HTMLUtil.Sanitize(request.Content) ?? checkUpdate.Content;
                checkUpdate.CreateAt = checkUpdate.CreateAt;
                if (request.Image != null)
                {
                    checkUpdate.Image = await _cloudinaryService.Upload(request.Image);
                }
                else
                {
                    checkUpdate.Image = checkUpdate.Image;
                }

                _context.Blogs.Update(checkUpdate);
                await _context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        public async Task<bool> DeleteBlog(Guid blogId)
        {
            var checkDelete = await _context.Blogs.FirstOrDefaultAsync(x => x.BlogId == blogId);
            if (checkDelete == null)
            {
                return false;
            }

            _context.Blogs.Remove(checkDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetBlogDTO> GetBlog(Guid blogId)
        {
            var getBlog = await _context.Blogs
                                        .Include(a => a.Author)
                                        .FirstOrDefaultAsync(x => x.BlogId == blogId);
            if (getBlog == null)
            {
                return null;
            }

            var mapItem = new GetBlogDTO
            {
                Id = getBlog.BlogId,
                Title = getBlog.Title,
                Image = getBlog.Image,
                Content = getBlog.Content,
                CreateAt = getBlog.CreateAt,
                Author = new Author
                {
                    AuthorId = getBlog.AuthorId,
                    FullName = getBlog.Author.FullName,
                    Address = getBlog.Author.Address!,
                    Email = getBlog.Author.Email,
                    Phone = getBlog.Author.PhoneNumber!,
                    Avatar = getBlog.Author.Avatar!
                }
            };

            return mapItem;
        }
    }
}
