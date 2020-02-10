using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sorbi.Bots
{
    public class EchoBot : ActivityHandler
    {
        public static string ordernumber = "";
        public static string account = "";
        static string platform = "";
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = turnContext.Activity.Text;

            if (replyText.Contains("siparis"))
            {
                await SendSuggestedActionsPlatformAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText == "PM")
            {
                await SendSuggestedActionsAccountAsync(turnContext, cancellationToken);
                platform = "PM";
                return;
            }
            if (replyText == "PX")
            {
                await SendSuggestedActionsAccount2Async(turnContext, cancellationToken);
                account = replyText;
                return;
            }

            if (replyText == "IKEA")
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Lütfen Siparis Nunamasi giriniz."), cancellationToken);
                account = "IKEA";
                return;
            }

            if (replyText == "ELCA")
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Lütfen Siparis Nunamasi giriniz."), cancellationToken);
                account = "ELCA";
                return;
            }

            if (replyText.Contains("ord"))
            {
                await SendSuggestedActionPackingAsync(turnContext, cancellationToken);
                ordernumber = replyText;
                return;
            }

            if (replyText == "6")
            {
                await turnContext.SendActivityAsync(MessageFactory.Text(ordernumber + " numarali siparis için lütfen karsilastiginiz sorunu yaziniz"), cancellationToken);
                return;
            }

            if (replyText == "4")
            {
                await SendSuggestedActionWayybillAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText.Contains("yardim"))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("ASM-434 numarali Jira maddesini senin için olusturdum. Bu numaradan takip edebilirsin. - " + platform + " Depo " + account + " Müsterisi için " + ordernumber + " numarali siparisde bir sorun ile karsilastim. Acil yardiminiz Bekleniyor - '" + replyText + "' - DESTEK BİRİMİNE ILETILMISTIR EN KISA SUREDE SIZE DONUS SAGLANACAKTIR"), cancellationToken);
                return;
            }

            await turnContext.SendActivityAsync(MessageFactory.Text("Üzgünüm !! Seni anlayamadim :(, Lütfen daha fazla bilgi verebilirmisin."), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Merhaba ben Sorbi, Size nasil yardimci olabilirim.";
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
            var reply = MessageFactory.Text("Size daha iyi yardimci olabilmem için, Lütfen Depo seçiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPlatforms()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionsAccountAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Lütfen Müsteri seçiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetAccountsc()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionsAccount2Async(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Lütfen Müsteri seçiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetAccountsc2()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionPackingAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text(ordernumber + " numarali siparis için, Asagidaki hangi nedeni söyleyebilirsiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPackingErrors()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionWayybillAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text(ordernumber + " numarali siparis için, Irsaliye yi asagidaki Linke tiklayarak ulasabilirsiniz.");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetWayybil()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
    }
}