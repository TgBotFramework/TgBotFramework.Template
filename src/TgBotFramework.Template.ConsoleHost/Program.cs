using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TgBotFramework;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.Enums;
using TgBotFramework.Template.Handlers;
using TgBotFramework.Template.Handlers.CommonHandlers;
using TgBotFramework.Template.Handlers.Stages;
using TgBotFramework.Template.Infrastructure;
using TgBotFramework.WrapperExtensions;

await Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase("in-mem-prod-database");
    });
    services.AddScoped<StageHandler>();
    
    services.AddScoped<EmptyStage>();
    services.AddScoped<InterviewStage>();
    services.AddScoped<OnMessageHandler>();
    services.AddScoped<OnEditedMessageHandler>();
    services.AddScoped<OnPollHandler>();
    services.AddScoped<OnPollAnswerHandler>();
    services.AddScoped<OnChannelPostHandler>();
    services.AddScoped<OnEditedChannelPostHandler>();
    services.AddScoped<OnPreCheckoutQueryHandler>();
    services.AddScoped<OnShippingQueryHandler>();
    services.AddScoped<OnChatMemberHandler>();
    services.AddScoped<OnChatJoinRequestHandler>();
    services.AddScoped<OnMyChatMemberHandler>();
    services.AddScoped<OnCallbackQueryHandler>();
    services.AddScoped<OnInlineQueryHandler>();
    services.AddScoped<OnChosenInlineResultHandler>();
    services.AddScoped<StartCommand>();
    
    services.AddBotService<BotContext>("<bot-token>", builder => builder
        .UseLongPolling(ParallelMode.SingleThreaded, new LongPollingOptions() { DebugOutput = true })
        .UseMiddleware<StageHandler>()
        .SetPipeline(pipeBuilder => pipeBuilder
                //small dialog example
            .MapWhen(x=>x.ChatStage.Stage != Stage.User && x.Update.GetChat()?.Type == ChatType.Private,
                (pipelineBuilder =>pipelineBuilder 
                    .MapWhen<EmptyStage>(x=>x.ChatStage.Stage == Stage.Empty)
                    .MapWhen<InterviewStage>(x=> x.ChatStage.Stage == Stage.Interview)
                ))
            //message related 
            .UseWhen(In.PrivateChat, pipelineBuilder => pipelineBuilder
                .UseCommand<StartCommand>("start")
            )
            .MapWhen<OnMessageHandler>(On.Message)
            .MapWhen<OnEditedMessageHandler>(On.EditedMessage)
            // poll related
            .MapWhen<OnPollHandler>(On.Poll)
            .MapWhen<OnPollAnswerHandler>(On.PollAnswer)
            // channel related
            .MapWhen<OnChannelPostHandler>(On.ChannelPost)
            .MapWhen<OnEditedChannelPostHandler>(On.EditedChannelPost)
            // payments
            .MapWhen<OnPreCheckoutQueryHandler>(On.PreCheckoutQuery)
            .MapWhen<OnShippingQueryHandler>(On.ShippingQuery)
            // chats
            .MapWhen<OnChatMemberHandler>(On.ChatMember)
            .MapWhen<OnChatJoinRequestHandler>(On.ChatJoinRequest)
            .MapWhen<OnMyChatMemberHandler>(On.MyChatMember)
            // common 
            .MapWhen<OnCallbackQueryHandler>(On.CallbackQuery)
            .MapWhen<OnInlineQueryHandler>(On.InlineQuery)
            .MapWhen<OnChosenInlineResultHandler>(On.ChosenInlineResult)
        )
    );
}).RunConsoleAsync();