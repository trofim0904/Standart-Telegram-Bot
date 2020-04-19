using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Corebot.Commands
{
    public class KillCommand : Command
    {
        public override string Name => "kill";

        public async override void Action(ITelegramBotClient client, Chat chat, int messageId)
        {
            await client.SendStickerAsync(chatId: chat, sticker: "https://trofim0904.github.io/webp/kill.webp");
      
            
            
        }
    }
}
