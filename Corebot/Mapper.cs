using Corebot.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corebot
{
    public class Mapper
    {
        public static BotSticker StickerToBotSticker(Sticker sticker)
        {
            return new BotSticker()
            {
                CommandName = sticker.CommandName,
                Url = sticker.Url
            };
        }
    }
}
