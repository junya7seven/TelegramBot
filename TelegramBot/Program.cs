using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Service;
using TelegramBotData.DbOperations;
using TelegramBotData;

var botClient = new TelegramBotClient("6948054071:AAHswM8cDEpKQ4y18Byj7iwKM_DbV5a0C-M");

using CancellationTokenSource cts = new();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();


Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;
    var chatId = message.Chat.Id;
    var firstName = message.Chat.FirstName;
    var lastName = message.Chat.LastName;
    var userName = message.Chat.Username;
    var bio = message.Chat.Bio;
    var phone = message.Location;

    ServiceLogick service = new ServiceLogick();
    
    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: $"{chatId}-{bio}--{phone}",
        cancellationToken: cancellationToken
        );
    DbOperations db = new DbOperations();
    await db.AddValuesAsync(chatId,userName,firstName,lastName,bio,messageText);
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}