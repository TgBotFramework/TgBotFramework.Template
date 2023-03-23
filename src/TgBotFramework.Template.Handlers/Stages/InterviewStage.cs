using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotFramework;
using TgBotFramework.Template.Infrastructure;
using TgBotFramework.WrapperExtensions;

namespace TgBotFramework.Template.Handlers.Stages;

public class InterviewStage : IUpdateHandler<BotContext>
{
    public async Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next,
        CancellationToken cancellationToken)
    {
        if (context.ChatStage.Stage != Stage.Interview)
            throw new Exception("Incorrect stage");

        if (context.Update.GetChat().Type != ChatType.Private)
            return;

        switch (context.ChatStage.Step)
        {
            case 0:
                if (context.Update.Type != UpdateType.Message)
                    return;
                await context.Client.SendTextMessageAsync(context.Update.GetSenderId(),
                    "Lets begin our interview. What's your name?",
                    allowSendingWithoutReply: false, cancellationToken: cancellationToken);
                context.ChatStage.Step++;
                break;
            case 1:
                // filtering by contents of update
                if (context.Update.Type != UpdateType.Message && context.Update.Message.Text == null)
                    return;

                //getting answer
                var name = context.Update.Message.Text;
                // lets say we'll save this answers somewhere
                await context.Client.SendTextMessageAsync(context.Update.GetSenderId(), "Your email?",
                    allowSendingWithoutReply: false, cancellationToken: cancellationToken);
                context.ChatStage.Step++;
                break;
            case 2:
                if (context.Update.Type != UpdateType.Message && context.Update.Message.Text == null)
                    return;
                var email = context.Update.Message.Text;
                // lets say we'll save this answers somewhere
                await context.Client.SendTextMessageAsync(context.Update.GetSenderId(),
                    "Preferred language? Click on 1 of the buttons",
                    replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("English", "en-us"), // ISO 639-1 standard language codes
                        InlineKeyboardButton.WithCallbackData("Ukrainian", "uk")
                    }),
                    cancellationToken: cancellationToken);
                context.ChatStage.Step++;
                break;
            case 3:
                if (context.Update.Type != UpdateType.CallbackQuery)
                    return;

                // lets say we'll save this answers somewhere
                var language = context.Update.CallbackQuery.Data;
                // example of getting button text by callback data
                var langName = context.Update.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.Select(x =>
                    x.FirstOrDefault(t => t.CallbackData == context.Update.CallbackQuery.Data).Text).FirstOrDefault();
                await context.Client.SendTextMessageAsync(context.Update.GetSenderId(),
                    $"Thanks, your desired language is: {langName} \nnow you can use other bot functions!",
                    cancellationToken: cancellationToken);
                context.ChatStage.Stage = Stage.User;
                context.ChatStage.Step = 0;
                break;
        }
    }
}