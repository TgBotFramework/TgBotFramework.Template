﻿using TgBotFramework;
using TgBotFramework.Template.Infrastructure;

namespace TgBotFramework.Template.Handlers.CommonHandlers;

public class OnEditedMessageHandler : IUpdateHandler<BotContext>
{
    public Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}