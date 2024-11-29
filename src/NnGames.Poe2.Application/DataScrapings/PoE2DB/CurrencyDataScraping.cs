using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using NnGames.Poe2.Domains.DmCurrency;
using System.Collections.Generic;

namespace NnGames.Poe2.DataScrapings.PoE2DB
{
    public class CurrencyDataScraping
    {
        public List<Currency> Execute()
        {
            var web = new HtmlWeb();
            var document = web.Load("https://poe2db.tw/us/Currency");
            var htmlElements = document.DocumentNode.QuerySelectorAll("div.itemBoxContent");

            var l = new List<Currency>();
            foreach (var productHTMLElement in htmlElements)
            {
                var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.lc").InnerText?.Trim());
                var stackSize = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.colourDefault").InnerText?.Trim().Replace("1/", string.Empty));
                var description = HtmlEntity.DeEntitize((productHTMLElement.QuerySelector("div.explicitMod")?.InnerText ?? productHTMLElement.QuerySelector("div.implicitMod")?.InnerText)?.Trim());
                var note = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.fst-italic")?.InnerText?.Trim());

                l.Add(new Currency(name, short.Parse(stackSize), description, note));
            }

            return l;
        }
    }
}
