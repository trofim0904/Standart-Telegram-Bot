using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Corebot.Commands
{
    class StartCommand : Command
    {
        public override string Name => "start";
        //https://s.tcdn.co/3b6/1c4/3b61c4e8-695a-30c1-9fc9-78530479a8fb/12.png
        public async override void Action(ITelegramBotClient client, Chat chat, int messageId)
        {
         

            await client.SendStickerAsync(chatId: chat, sticker: "https://trofim0904.github.io/webp/start.webp");
            await client.SendTextMessageAsync(chat, "Ohayo");

            //await client.SendPhotoAsync(chat, photo: "https://s.tcdn.co/3b6/1c4/3b61c4e8-695a-30c1-9fc9-78530479a8fb/12.png",
            //    caption: "<b>Ohayo</b>", parseMode: ParseMode.Html);
        }
    }
}
