using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Template.Infrastructure;
using TgBotFramework.WrapperExtensions;

public class StartCommand : CommandBase<BotContext>
{
    public override async Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, string[] args, CancellationToken cancellationToken)
    {
        await context.Client.SendTextMessageAsync(context.Update.GetSenderId(), "You've used /startCommand", cancellationToken: cancellationToken);
    }
}