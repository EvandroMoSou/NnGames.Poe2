using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using NnGames.Poe2.Domains.DmGem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NnGames.Poe2.DataScrapings.PoE2DB
{
    public class GemDataScraping : IGemDataScraping
    {
        protected IGemRepository _repository;

        public GemDataScraping(IGemRepository repository)
        {
            _repository = repository;
        }

        public async Task InsertOrUpdateAsync()
        {
            var l = SkillGemDataScraping();
            l.AddRange(SpiritGemDataScraping());
            l.AddRange(SupportGemDataScraping());

            await _repository.InsertOrUpdateByNameAsync(l);
        }

        protected List<Gem> SkillGemDataScraping()
        {
            return Execute("https://poe2db.tw/us/Skill_Gems");
        }

        protected List<Gem> SpiritGemDataScraping()
        {
            return Execute("https://poe2db.tw/us/Spirit_Gems");
        }

        protected List<Gem> SupportGemDataScraping()
        {
            return Execute("https://poe2db.tw/us/Support_Gems");
        }

        protected List<Gem> Execute(string url)
        {
            var web = new HtmlWeb();
            var document = web.Load(url);
            var htmlElements = document.DocumentNode.QuerySelectorAll("div.itemBoxContent");

            var l = new List<Gem>();
            foreach (var productHTMLElement in htmlElements)
            {
                var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.itemName span.lc").InnerText?.Trim());
                var type = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.typeLine span.lc").InnerText?.Trim());

                //var type = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.colourDefault").InnerText?.Trim().Replace("1/", string.Empty));
                //var description = HtmlEntity.DeEntitize((productHTMLElement.QuerySelector("div.explicitMod")?.InnerText ?? productHTMLElement.QuerySelector("div.implicitMod")?.InnerText)?.Trim());
                //var note = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.fst-italic")?.InnerText?.Trim());

                l.Add(new Gem { Name = name, Type = type });
            }

            return l;
        }

    }
}
