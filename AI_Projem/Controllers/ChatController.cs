using AI_Projem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AI_Projem.Controllers
{
    public class ChatController : Controller
    {
        private readonly string ApiUrl = "https://api.openai.com/v1/chat/completions";
        private readonly string ApiKey;

      
        public ChatController(IConfiguration configuration)
        {
            ApiKey = configuration["OpenAI:ApiKey"]; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                ViewBag.Response = "Lütfen bir sorun sorun. Örneğin, bana bir çorba tarifi ver diyebilirsiniz...   "; 
                return View();
            }

        
            if (!RecipeFilter.IsRecipeQuery(userInput))
            {
                ViewBag.Response = "Bu bir yemek tarifiyle ilgili görünmüyor. Lütfen yemek tarifleriyle ilgili soru sorun. Örneğin, bana bir çorba tarifi ver diyebilirsiniz... ";
                if (userInput.Any(c => char.IsLetter(c) && char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.UppercaseLetter))
                {
                    ViewBag.Response = "This doesn't seem related to recipes. Please ask about cooking or ingredients. For example, you could say, 'Can you give me a recipe for any type of soup?"; 
                }

                return View();
            }

         
            string response = await GetChatResponse(userInput);
            ViewBag.Response = response;
            return View();
        }


        private async Task<string> GetChatResponse(string prompt)
        {
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

            
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] { new { role = "user", content = prompt } },
                    max_tokens = 1000
                };

               
                var jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

           
                var response = await httpClient.PostAsync(ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return jsonResponse?.choices[0]?.message?.content ?? "No response";
                }
                else
                {
                    //return "ChatGPT API'ye bağlanırken hata oluştu";
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode} - {errorMessage}";
                }
            }
        }
    }
}

