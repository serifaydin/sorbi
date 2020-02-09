using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using ServiceStack.Support.Markdown;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sorbi.Bots
{
    public class EchoBot : ActivityHandler
    {
        public static string ordernumber = "";
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = turnContext.Activity.Text;

            if (replyText.Contains("sipari�"))
            {
                await SendSuggestedActionsPlatformAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText == "PX")
            {
                await SendSuggestedActionsAccountAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText == "IKEA")
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("L�tfen Sipari� Nunamas� giriniz."), cancellationToken);
                return;
            }

            if (replyText == "123456789")
            {
                await SendSuggestedActionPackingAsync(turnContext, cancellationToken);
                ordernumber = replyText;
                return;
            }

            if (replyText == "6")
            {
                await turnContext.SendActivityAsync(MessageFactory.Text(ordernumber + " numaral� sipari� i�in l�tfen kar��la�t���n�z sorunu yaz�n�z"), cancellationToken);
                return;
            }

            //await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Merhaba ben Sorbi, Size nas�l yard�mc� olabilirim.";
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
            var reply = MessageFactory.Text("Size daha iyi yard�mc� olabilmem i�in, L�tfen Depo se�iniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPlatforms()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionsAccountAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("L�tfen M��teri se�iniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetAccountsc()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionPackingAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text(ordernumber + " numaral� sipari� i�in, A�a��daki hangi nedeni s�yleyebilirsiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPackingErrors()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
    }
}