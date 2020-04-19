using Corebot.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Corebot
{
    class Program
    {
        private static ITelegramBotClient _client;

        private static List<Command> _commands;
        static void Main(string[] args)
        {
           

            _client = new TelegramBotClient(args[0]);
            

            var me = _client.GetMeAsync().Result;
            Console.WriteLine("Bot name: " + me.FirstName);
            Console.WriteLine("Token: " + args[0]);
            _commands = new List<Command>
            {
                new StartCommand(),
                new TestCommand(),
                new KillCommand(),
                new ZerotwoCommand()
                
            };
            _client.OnMessage += _client_OnMessage;
            _client.StartReceiving();

            Thread.Sleep(Timeout.Infinite);
  


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
