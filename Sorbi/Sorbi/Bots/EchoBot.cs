using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using ServiceStack.Support.Markdown;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sorbi.Bots
{
    public class EchoBot : ActivityHandler
    {
        public static string ordernumber = "";
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //var replyText = turnContext.Activity.Text;

            //if (replyText.Contains("sipariþ"))
            //{
            //    await SendSuggestedActionsPlatformAsync(turnContext, cancellationToken);
            //    return;
            //}

            //if (replyText == "PX")
            //{
            //    await SendSuggestedActionsAccountAsync(turnContext, cancellationToken);
            //    return;
            //}

            //if (replyText == "IKEA")
            //{
            //    await turnContext.SendActivityAsync(MessageFactory.Text("Lütfen Sipariþ Nunamasý giriniz."), cancellationToken);
            //    return;
            //}

            //if (replyText == "123456789")
            //{
            //    await SendSuggestedActionPackingAsync(turnContext, cancellationToken);
            //    ordernumber = replyText;
            //    return;
            //}

            //if (replyText == "6")
            //{
            //    await turnContext.SendActivityAsync(MessageFactory.Text(ordernumber + " numaralý sipariþ için lütfen karþýlaþtýðýnýz sorunu yazýnýz"), cancellationToken);
            //    return;
            //}

            //if (replyText == "4")
            //{
            //    await SendSuggestedActionWayybillAsync(turnContext, cancellationToken);
            //    return;
            //}

           // await turnContext.SendActivityAsync(MessageFactory.Text("Üzgünüm !! Seni anlayamadým, Lütfen daha fazla bilgi verebilirmisin :("), cancellationToken);
        }



        //private static Attachment GetLocalAttachment()
        //{
        //    var imagePath = HttpContext.Current.Server.MapPath("~/images/demo.gif");

        //    var imageData = Convert.ToBase64String(File.ReadAllBytes(imagePath));

        //    return new Attachment
        //    {
        //        Name = "demo.gif",
        //        ContentType = "image/gif",
        //        ContentUrl = $"data:image/gif;base64,{imageData}"
        //    };
        //}


        private static Attachment GetInternetAttachment()
        {
            return new Attachment
            {
                Name = "architecture-resize.png",
                ContentType = "image/png",
                ContentUrl = "https://docs.microsoft.com/en-us/bot-framework/media/how-it-works/architecture-resize.png"
            };
        }











        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Merhaba ben Sorbi, Size nasýl yardýmcý olabilirim.";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

        private static async Task SendSuggestedActionsPlatformAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Size daha iyi yardýmcý olabilmem için, Lütfen Depo seçiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPlatforms()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionsAccountAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Lütfen Müþteri seçiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetAccountsc()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionPackingAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text(ordernumber + " numaralý sipariþ için, Aþaðýdaki hangi nedeni söyleyebilirsiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPackingErrors()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionWayybillAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text(ordernumber + " numaralý sipariþ için, Ýrsaliye yi aþaðýdaki Linke týklayarak ulaþabilirsiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetWayybil()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }



        private static async Task hhhhh(ITurnContext turnContext, CancellationToken cancellationToken)
        {

            turnContext.Activity.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://docs.microsoft.com/en-us/bot-framework/media/how-it-works/architecture-resize.png",
                ContentType = "image/png",
                Name = "AIHelpWebsiteLogo_Large.png"
            });




            var reply = MessageFactory.Text("Size daha iyi yardýmcý olabilmem için, Lütfen Depo seçiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPlatforms()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
    }
}