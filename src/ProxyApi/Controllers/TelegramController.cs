using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace ProxyApi.Controllers
{
    [Route("api/telegram")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly ILogger<TelegramController> _logger;

        public TelegramController(ILogger<TelegramController> logger)
        {
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> NewMessage([FromForm]NewTelegramMessage newTelegramMessage)
        {
            try
            {
                _logger.LogInformation($"{newTelegramMessage.ChatId} : {newTelegramMessage.Text}");

                var botClient = new Telegram.Bot.TelegramBotClient(newTelegramMessage.BotToken);
                var me = await botClient.GetMeAsync();

                if (newTelegramMessage.File != null)
                {
                    var resultPhotoMessage = await botClient.SendPhotoAsync(newTelegramMessage.ChatId, new InputMedia(newTelegramMessage.File.OpenReadStream(), newTelegramMessage.File.FileName), newTelegramMessage.Text);
                    return Ok(resultPhotoMessage.MessageId);
                }
                else
                {
                    var resultTextMessage = await botClient.SendTextMessageAsync(newTelegramMessage.ChatId, newTelegramMessage.Text);
                    return Ok(resultTextMessage.MessageId);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
    }

    public class NewTelegramMessage
    {
        public string BotToken { get; set; }
        public string ChatId { get; set; }
        public string Text { get; set; }
        public IFormFile File { get; set; }
    }
}
