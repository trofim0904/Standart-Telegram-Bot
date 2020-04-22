using Corebot.Commands;
using Corebot.DataBase;
using Corebot.MemchedClasses;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Corebot
{
    class Program
    {
        private static ITelegramBotClient _client;

        private static List<Command> _commands;
        static void Main(string[] args)
        {

            List<BotSticker> botStickers = new List<BotSticker>();
            #region Data base 

            using(ApplicationContext db = new ApplicationContext("localhost", "myuser", "myuser", "stickers"))//args[]
            {
                foreach(Sticker sticker in db.Stickers)
                {
                    
                    if (sticker.BotId == 2/*args[]*/ || sticker.BotId == null)
                    {
                        botStickers.Add(Mapper.StickerToBotSticker(sticker));
                    }
                }

            }

            #endregion

            #region Memched

            var provider = ContainerConfiguration.Configure();

            var cacheRepository = provider.GetService<ICacheRepository>();

            foreach (BotSticker botSticker in botStickers)
            {
                cacheRepository.Set(botSticker.CommandName, botSticker.Url);
            }


            var cacheProvider = provider.GetService<ICacheProvider>();

            foreach (BotSticker botSticker in botStickers)
            {
                Console.WriteLine($"Memched url: {cacheProvider.GetCache<string>(botSticker.CommandName)}");
            }

      

            #endregion

            #region Bot start

            //_client = new TelegramBotClient(args[0]);
            _client = new TelegramBotClient("1277866234:AAGC5RHjVsy7-UNZfVzmwmPi7lrTgz_CBF4");

            var me = _client.GetMeAsync().Result;
            Console.WriteLine("Bot name: " + me.FirstName);
            //Console.WriteLine("Token: " + args[0]);
            _commands = new List<Command>();
            foreach(BotSticker botSticker in botStickers)
            {
                _commands.Add(new StickerCommand(botSticker.CommandName, botSticker.Url));
            }

            //{
            //    new StartCommand(),
            //    new KillCommand(),
            //    new ZerotwoCommand()

            //};
            _client.OnMessage += _client_OnMessage;
            _client.StartReceiving();

            //Thread.Sleep(Timeout.Infinite);
            #endregion


            Console.ReadKey();
  


        }

        private async static void _client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            Console.WriteLine(e.Message.Chat.FirstName + " " + e.Message.Chat.LastName + " " + text);
            if (text == null)
                return;
            var chatId = e.Message.Chat;
            var messageId = e.Message.MessageId;
            foreach(Command command in _commands)
            {
                if (command.CheckCommand(text))
                {
                    command.Action(_client, chatId, messageId);

                    return;
                }
                
            }
            await _client.SendTextMessageAsync(chatId,"I don`t know what do you want to do");

            
        }
    }
}
