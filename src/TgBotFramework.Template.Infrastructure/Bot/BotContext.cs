using TgBotFramework;

namespace TgBotFramework.Template.Infrastructure;

public class BotContext : UpdateContext
{
    public ChatStage ChatStage { get; set; }
}