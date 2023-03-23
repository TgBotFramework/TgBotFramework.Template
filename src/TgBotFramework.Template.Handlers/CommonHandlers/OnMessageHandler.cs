using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Template.Infrastructure;
using TgBotFramework.WrapperExtensions;

namespace TgBotFramework.Template.Handlers.CommonHandlers;

public class OnMessageHandler : IUpdateHandler<BotContext>
{
    public async Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        await context.Client.SendTextMessageAsync(context.Update.GetSenderId(), "Message", cancellationToken: cancellationToken);
    }
}