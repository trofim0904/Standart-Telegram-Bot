using System;
using System.Collections.Generic;
using System.Text;

namespace Corebot.DataBase
{
    public class Sticker
    {
        public int Id { get; set; }
        public string CommandName { get; set; }
        public string Url { get; set; }

        public int? BotId { get; set; }
    }
}
