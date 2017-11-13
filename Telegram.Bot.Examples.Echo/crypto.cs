using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.Enums;
using System.Globalization;

namespace Telegram.Bot.Examples.Echo
{
    public class Crypto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string rank { get; set; }
        public string price_usd { get; set; }
        public string price_btc { get; set; }
        [JsonProperty(PropertyName = "24h_volume_usd")]
        public string _24h_volume_usd { get; set; }
        public string market_cap_usd { get; set; }
        public string available_supply { get; set; }
        public string total_supply { get; set; }
        public string percent_change_1h { get; set; }
        public string percent_change_24h { get; set; }
        public string percent_change_7d { get; set; }
        public string last_updated { get; set; }
        public string price_eur { get; set; }
        public string __invalid_name__24h_volume_eur { get; set; }
        public string market_cap_eur { get; set; }

        internal InlineQueryResultArticle toInline()
        {
            string msg = "<b>" + name + "</b> <i>" + symbol + "</i>" + Environment.NewLine;
            double eur = double.Parse(market_cap_eur, CultureInfo.InvariantCulture);
            double usd = double.Parse(market_cap_usd, CultureInfo.InvariantCulture);

            msg += "$" + price_usd + Environment.NewLine;
            msg += "€" + price_eur + Environment.NewLine;
            msg += Environment.NewLine;
            if (percent_change_1h != null) msg += (percent_change_1h.Contains("-") ? "🔻" : "🔺") + "<i>1h " + percent_change_1h + "%</i>"+ Environment.NewLine;
            if (percent_change_24h != null) msg += (percent_change_24h.Contains("-") ? "🔻" : "🔺") + "<i>24h " + percent_change_24h + "%</i>" + Environment.NewLine;
            if(percent_change_7d != null)  msg += (percent_change_7d.Contains("-") ? "🔻" : "🔺") + "<i>7d " + percent_change_7d + "%</i>" + Environment.NewLine;
            msg += Environment.NewLine;
            msg += "<b>Market Cap</b>" + Environment.NewLine;
            msg += "$" + usd.ToString("#,##0") + Environment.NewLine;
            msg += "€" + eur.ToString("#,##0") + Environment.NewLine;
            return new InlineQueryResultArticle
            {
                Id =  rank,
                Title = name,
                Description = symbol + " $" + price_usd + " " + (percent_change_24h.Contains("-") ? "🔻" : "🔺") + percent_change_24h,
                InputMessageContent = new InputTextMessageContent() {
                    MessageText = msg,
                    DisableWebPagePreview = false,
                    ParseMode = ParseMode.Html
                },

            };
        }
    }
}
