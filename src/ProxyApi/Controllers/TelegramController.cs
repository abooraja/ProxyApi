using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace ProxyApi.Controllers
{
    [Route("api/telegram")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        [HttpPost("send")]
        public async Task<string> NewMessage(NewTelegramMessage newTelegramMessage)
        {
            var botClient = new Telegram.Bot.TelegramBotClient(newTelegramMessage.BotToken);
            var me = await botClient.GetMeAsync();

            if (newTelegramMessage.File != null)
            {
                var memoryStream = new MemoryStream();
                await newTelegramMessage.File.CopyToAsync(memoryStream);
                var resultPhotoMessage = await botClient.SendPhotoAsync(newTelegramMessage.ChatId, memoryStream, newTelegramMessage.Text);
                return resultPhotoMessage.MessageId.ToString();
            }
            else
            {
                var resultTextMessage = await botClient.SendTextMessageAsync(newTelegramMessage.ChatId, newTelegramMessage.Text);
                return resultTextMessage.MessageId.ToString();
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
