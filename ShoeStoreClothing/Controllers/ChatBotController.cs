using Microsoft.AspNetCore.Mvc;

namespace ShoeStoreClothing.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly string OpenAIApiKey = "YOUR_OPENAI_API_KEY";
        [HttpPost]
        public async Task<IActionResult> GetResponse(string message)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {OpenAIApiKey}");
                var data = new
                {
                    model = "text-davinci-003",
                    prompt = message,
                    max_tokens = 150
                };
                var response = await client.PostAsJsonAsync("https://api.openai.com/v1/completions", data);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Json(new { reply = result });
                }
                return Json(new { reply = "Sorry, I couldn't process your request. Please try again." });
            }
            catch (Exception ex) {
                return Json(new { reply = "Sorry, I couldn't process your request. Please try again." });
            }
        }
    }
}
