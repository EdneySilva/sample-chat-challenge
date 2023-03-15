using AppChat.Domain;
using AppChat.Infrastructure.Bot;
using Microsoft.AspNetCore.Mvc;

namespace AppChat.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBotController : ControllerBase
    {
        private readonly ChatBot _chatBot;

        public ChatBotController(ChatBot chatBot)
        {
            _chatBot = chatBot;
        }

        [HttpPost("httpclient")]
        public async Task<IActionResult> PostHttpBotClient([FromBody] HttpClientChatBotCommand bot)
        {
            await _chatBot.AddCommandAsync(new System.Text.RegularExpressions.Regex(bot.Command), bot);
            return Ok();
        }

        [HttpPost("replay")]
        public async Task<IActionResult> BotResponse(
            [FromBody] ThreadMessage message, 
            [FromServices] ThreadMessageStream stream)
        {
            stream.Append(message);
            return Ok();
        }
    }
}