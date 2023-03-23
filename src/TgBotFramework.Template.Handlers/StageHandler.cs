using TgBotFramework;
using TgBotFramework.Template.Infrastructure;
using TgBotFramework.WrapperExtensions;

namespace TgBotFramework.Template.Handlers;

public class StageHandler : IUpdateHandler<BotContext>
{
    private readonly AppDbContext _context;

    public StageHandler(AppDbContext context)
    {
        this._context = context;
    }

    public async Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        long chatId = context.Update.GetSenderId();

        // get or update chat stage info
        var chatStage = _context.Stages.FirstOrDefault(x => x.ChatId == chatId);
        if (chatStage == null)
        {
            chatStage = new ChatStage { ChatId = chatId, Stage = Stage.Empty, Step = 0, LanguageCode = "En-Us" };

            await _context.Stages.AddAsync(chatStage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        context.ChatStage = chatStage;
        
        // call next handlers
        await next(context, cancellationToken);

        // after all handlers - update and save stage
        _context.Update(chatStage);
        await _context.SaveChangesAsync(cancellationToken);
    }
}