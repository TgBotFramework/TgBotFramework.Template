using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Template.Infrastructure;
using TgBotFramework.WrapperExtensions;

namespace TgBotFramework.Template.Handlers.Stages;

public class EmptyStage : IUpdateHandler<BotContext>
{
    private readonly InterviewStage _interviewStage;

    public EmptyStage(InterviewStage interviewStage)
    {
        _interviewStage = interviewStage;
    }
    
    public async Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        context.ChatStage.Stage = Stage.Interview;
        await _interviewStage.HandleAsync(context, next, cancellationToken);
    }
}