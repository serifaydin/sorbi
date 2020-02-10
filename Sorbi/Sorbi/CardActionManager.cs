using Microsoft.Bot.Schema;
using System.Collections.Generic;

namespace Sorbi
{
    public static class CardActionManager
    {
        public static List<CardAction> GetPlatforms()
        {
            return new List<CardAction>
            {
                new CardAction() { Title = "Platform X", Type = ActionTypes.ImBack, Value = "PX" },
                new CardAction() { Title = "Platform S", Type = ActionTypes.ImBack, Value = "PS" },
                new CardAction() { Title = "Platform S2", Type = ActionTypes.ImBack, Value = "PS2" },
                new CardAction() { Title = "Platform M", Type = ActionTypes.ImBack, Value = "PM" },
                new CardAction() { Title = "Platform M2", Type = ActionTypes.ImBack, Value = "PM2" },
                new CardAction() { Title = "Platform L", Type = ActionTypes.ImBack, Value = "PL" }
            };
        }

        public static List<CardAction> GetAccountsc()
        {
            return new List<CardAction>
            {
                new CardAction() { Title = "IKEA", Type = ActionTypes.ImBack, Value = "IKEA" },
                new CardAction() { Title = "AYPIMA", Type = ActionTypes.ImBack, Value = "AYPIMA" }
            };
        }

        public static List<CardAction> GetPackingErrors()
        {
            return new List<CardAction>
            {
                new CardAction() { Title = "Ortak Etiket Sorunu", Type = ActionTypes.ImBack, Value = "1" },
                new CardAction() { Title = "Arvato Etiketi Sorunu", Type = ActionTypes.ImBack, Value = "2" },
                new CardAction() { Title = "Görev Bulunamadı sorunu", Type = ActionTypes.ImBack, Value = "3" },
                new CardAction() { Title = "İrsaliye sorunu", Type = ActionTypes.ImBack, Value = "4" },
                new CardAction() { Title = "İptal Sipariş Transfer sorunu", Type = ActionTypes.ImBack, Value = "5" },
                new CardAction() { Title = "Diğer", Type = ActionTypes.ImBack, Value = "6" },
            };
        }


        public static List<CardAction> GetWayybil()
        {
            return new List<CardAction>
            {
                new CardAction() { Title = "İkea İrsaliye", Type = ActionTypes.OpenUrl, Value = "http://www.leighsocialclub.com/wp-content/uploads/waybill-format-air-waybill-sample-form-9-esca-pro.jpg" },
            };
        }
    }
}