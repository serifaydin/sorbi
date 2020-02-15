using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
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
        static bool _help = false;
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            var replyText = turnContext.Activity.Text;

            if (replyText.Contains("siparis"))
            {
                await SendSuggestedActionsPlatformAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText == "PL")
            {
                platform = "PL";
                await SendSuggestedActionsAccountAsync(turnContext, cancellationToken);
                return;
            }
            if (replyText == "PX")
            {
                platform = "PX";
                //account = replyText;
                await SendSuggestedActionsAccount2Async(turnContext, cancellationToken);
                return;
            }

            if (replyText == "IKEA")
            {
                account = "IKEA";
                await turnContext.SendActivityAsync(MessageFactory.Text("Lütfen Siparis Nunamasi giriniz."), cancellationToken);
                return;
            }

            if (replyText == "ELCA")
            {
                account = "ELCA";
                await turnContext.SendActivityAsync(MessageFactory.Text("Lütfen Siparis Nunamasi giriniz."), cancellationToken);
                return;
            }

            if (replyText.Contains("ord"))
            {
                ordernumber = replyText;
                await SendSuggestedActionPackingAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText == "6" || replyText == "hayir")
            {
                _help = true;
                await turnContext.SendActivityAsync(MessageFactory.Text(ordernumber + " numarali siparis için lütfen karsilastiginiz sorunu yazarmısın"), cancellationToken);
                return;
            }

            if (replyText == "4")
            {
                await SendSuggestedActionWayybillAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText == "1")
            {
                await SendIntroCardAsync(turnContext, cancellationToken);
                return;
            }

            if (replyText == "evet")
            {
                string text = "SORUNA CEVAP BULDUĞUNA ÇOK SEVİNDİM. SANA YAKŞALIK 1 DAKİKADA CEVAP BULDUM. SORDUĞUN SORUNUN CEVABINI KAYIT ALTINA ALDIM VE ÖNERİLERE EKLEYECEĞİME EMİN OLABİLİRSİN. GÖRÜŞMEK ÜZERE";
                await HelloSendIntroCardAsync(turnContext, cancellationToken, text);
                return;
            }

            if (_help == true)
            {
                Random rnd = new Random();
                int i = rnd.Next(1, 1000);

                _help = false;
                await turnContext.SendActivityAsync(MessageFactory.Text("ASM-" + i.ToString() + " numarali Jira maddesini senin için olusturdum. Bu numaradan takip edebilirsin. - " + platform + " Depo " + account + " Müsterisi için " + ordernumber + " numarali siparisde bir sorun ile karsilastim. Acil yardiminiz Bekleniyor - '" + replyText + "' - DESTEK BİRİMİNE ILETILMISTIR EN KISA SUREDE SIZE DONUS SAGLANACAKTIR"), cancellationToken);
                return;
            }

            await turnContext.SendActivityAsync(MessageFactory.Text("Üzgünüm !! Seni anlayamadim :( Lütfen daha fazla bilgi verebilirmisin."), cancellationToken);
        }

        private static async Task SendIntroCardAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var card = new HeroCard();
            card.Title = "SENİ ANLADIM SANIYORUM";
            card.Text = @"Sorun ile ilgili bir araştırma yaptım ve Aşağıdaki Çözüm önerilerini sunmak istiyorum, Araştırdığım Sistemler yandaki gibidir.(ASM - SDEV - SLOT) - SORUNA CEVAP BULABİLDİNMİ (evet - hayir) ?";
            card.Images = new List<CardImage>() { new CardImage("https://cdn.technologyadvice.com/wp-content/uploads/2018/02/friendly-chatbot-700x408.jpg") };
            card.Buttons = new List<CardAction>()
    {
        new CardAction(ActionTypes.OpenUrl, "Kargo Atama Sorunu", null, "Kargo Atama Sorunu", "Kargo Atama Sorunu", "https://docs.microsoft.com/en-us/azure/bot-service/?view=azure-bot-service-4.0"),
        new CardAction(ActionTypes.OpenUrl, "Adres Sorunu", null, "Adres Sorunu", "Adres Sorunu", "https://stackoverflow.com/questions/tagged/botframework"),
        new CardAction(ActionTypes.OpenUrl, "Paketleme sorunu", null, "Paketleme sorunu", "Paketleme sorunu", "https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-deploy-azure?view=azure-bot-service-4.0"),
    };
            var response = MessageFactory.Attachment(card.ToAttachment());
            await turnContext.SendActivityAsync(response, cancellationToken);
        }


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            string text = "Senin sorunlarına cevap bulmak için buradayım. İstersen sorunu sorarak başlayabiliriz.";
            await HelloSendIntroCardAsync(turnContext, cancellationToken, text);
        }

        private static async Task HelloSendIntroCardAsync(ITurnContext turnContext, CancellationToken cancellationToken, string message)
        {
            var card = new HeroCard();
            card.Title = "MERHABA BEN SORBİ";
            card.Text = message;
            card.Images = new List<CardImage>() { new CardImage("https://cdn.technologyadvice.com/wp-content/uploads/2018/02/friendly-chatbot-700x408.jpg") };
            var response = MessageFactory.Attachment(card.ToAttachment());
            await turnContext.SendActivityAsync(response, cancellationToken);
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
            var reply = MessageFactory.Text("Lütfen Müsteri seçermisin ?");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetAccountsc()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionsAccount2Async(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Lütfen Müsteri seçermisin ?");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetAccountsc2()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionPackingAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text(ordernumber + " numarali siparis için, Asagidaki hangi nedeni söyleyebilirsin ?");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetPackingErrors()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendSuggestedActionWayybillAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text(ordernumber + " numarali siparis için, Irsaliye ye asagidaki Linke tiklayarak ulasabilirsin. SENİ ANLAMIŞMIYIM (evet - hayir)");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = CardActionManager.GetWayybil()
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
    }
}