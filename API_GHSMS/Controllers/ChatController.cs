using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using System.Net.Http.Headers;

namespace API_GHSMS.Controllers
{
    [ApiController]
    [Route("api/v1/chat")]
    public class ChatController(IConfiguration _configuration) : Controller
    {
        [HttpPost("chat")]
        public async Task<IActionResult> GeniniChatBotMarkdown([FromBody] PromtAI request)
        {
            try
            {
                string apiKey = _configuration["Gemini:ApiKey"]!;
                string apiUrl = $"{_configuration["Gemini:ApiUrl"]}?key={apiKey}";

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new[]
                            {
                                new
                                {
                                    text = "Bạn hãy đóng vai trò là AI Thế Phi, của một bệnh viện đa khoa chuyên xét nghiệm, hãy trả lời vấn đề sau: " + request.Text
                                }
                            }
                        }
                    }
                };
                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                var resultJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(resultJson);

                    if (doc.RootElement.TryGetProperty("candidates", out var candidates) &&
                        candidates.GetArrayLength() > 0 &&
                        candidates[0].TryGetProperty("content", out var contentElement) &&
                        contentElement.TryGetProperty("parts", out var partsElement) &&
                        partsElement.GetArrayLength() > 0 &&
                        partsElement[0].TryGetProperty("text", out var textElement))
                    {
                        var text = textElement.GetString();

                        return Ok(new { data = text });
                    }

                    return Ok(new { data = new { text = "⚠ Không tìm thấy nội dung từ AI." } });
                }
                else
                {
                    return Content($"{{\"text\": \"❌ Error {response.StatusCode}: {resultJson}\"}}", "application/json", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                return Content($"{{\"text\": \"❌ Internal Server Error: {ex.Message}\"}}", "application/json", Encoding.UTF8);
            }
        }
    }
}
