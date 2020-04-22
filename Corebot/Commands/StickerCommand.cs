using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Corebot.Commands
{
    public class StickerCommand : Command
    {
        private string commandName;
        private string stickerUrl;

        public StickerCommand(string commandName, string stickerUrl)
        {
            this.commandName = commandName;
            this.stickerUrl = stickerUrl;
        }

        public override string Name => commandName;

        public async override void Action(ITelegramBotClient client, Chat chat, int messageId)
        {
            await client.SendStickerAsync(chatId: chat, sticker: stickerUrl);
        }
    }
}
