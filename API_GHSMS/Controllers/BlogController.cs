using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Repository.Util;
using Service.Interface;

namespace API_GHSMS.Controllers
{
    public class BlogController(IBlogService _blogService) : Controller
    {

        [HttpPost("create-blog")]
        [Authorize]
        public async Task<IActionResult> CreateBlog([FromForm] CreateBlogDTO request)
        {
            try
            {
                Guid userId = UserUtil.GetUserId(User);

                bool response = await _blogService.CreateBlog(userId, request);
                if (response == true)
                {
                    return StatusCode(200, new { Message = "Create blog susscess" });
                }
                else
                {
                    return StatusCode(500, new { Message = "Create Blog Fail" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("get-blogs")]
        public async Task<IActionResult> GetBlogs()
        {
            var getBlogs = await _blogService.GetBlogs();
            return Ok(new { Message = "Get blogs success", Data = getBlogs });
        }

        [HttpPatch("edit-blog/{blogId}")]
        public async Task<IActionResult> EditBlog(Guid blogId, [FromForm] CreateBlogDTO request)
        {
            try
            {
                var response = await _blogService.EditBlog(blogId, request);
                if (response == false)
                {
                    return StatusCode(404, new { Message = "Blog not found" });
                }

                else
                {
                    return Ok(new { Message = "Edit Blog Success" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.ToString() });
            }
        }

        [HttpDelete("delete-blog/{blogId}")]
        public async Task<IActionResult> DeleteBlog(Guid blogId)
        {
            var response = await _blogService.DeleteBlog(blogId);
            if (response == false)
            {
                return StatusCode(404, new { Message = "Blog not found" });
            }

            return Ok(new { Message = "Delete success" });
        }

        [HttpGet("get-blog")]
        public async Task<IActionResult> GetBlog([FromQuery] Guid blogId)
        {
            var response = await _blogService.GetBlog(blogId);
            if (response == null)
            {
                return NotFound(new { Message = "Blog not found" });
            }

            return Ok(new { Message = "Get blog success", Data = response });
        }
    }
}
