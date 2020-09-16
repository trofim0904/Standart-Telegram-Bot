using Corebot.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;

namespace Corebot
{
    class Program
    {
        private static ITelegramBotClient _client;

        private static List<Command> _commands;
        static void Main(string[] args)
        {

            List<BotSticker> botStickers = new List<BotSticker>
            {
                new BotSticker() { CommandName = "hello", Url = "https://trofim0904.github.io/webp/hello.webp" },
                new BotSticker() { CommandName = "start", Url = "https://trofim0904.github.io/webp/start.webp" },
                new BotSticker() { CommandName = "kill", Url = "https://trofim0904.github.io/webp/kill.webp" },
                new BotSticker() { CommandName = "02", Url = "https://trofim0904.github.io/webp/zerotwo.webp" }
            };

            #region Bot start

            _client = new TelegramBotClient(args[0]);

            var me = _client.GetMeAsync().Result;
            Console.WriteLine("Bot name: " + me.FirstName);
            _commands = new List<Command>();

            foreach(BotSticker botSticker in botStickers)
            {
                _commands.Add(new StickerCommand(botSticker.CommandName, botSticker.Url));
            }

           
            _client.OnMessage += _client_OnMessage;
            _client.StartReceiving();


            #endregion

            Thread.Sleep(Timeout.Infinite);
            //Console.ReadKey();
  


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
