using TgBotFramework.Template.Infrastructure;
using TgBotFramework;

namespace TgBotFramework.Template.Handlers.CommonHandlers;

public class OnChannelPostHandler : IUpdateHandler<BotContext>
{
    public Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}