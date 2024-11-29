using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using NnGames.Poe2.Domains.DmGem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace NnGames.Poe2.DataScrapings.PoE2DB
{
    public class GemDataScraping : IGemDataScraping, ITransientDependency
    {
        protected IGemRepository _repository;

        public GemDataScraping(IGemRepository repository)
        {
            _repository = repository;
        }

        public async Task InsertOrUpdateAsync()
        {
            var l = AllGemDataScraping();

            //types
            var lightningBolt = l.Where(x => x.Type == "Lightning Bolt").FirstOrDefault();
            if(lightningBolt != null)
            {
                lightningBolt.Name = "Lightning Bolt";
                lightningBolt.Type = "Spell";
            }

            var rampage = l.Where(x => x.Type == "Rampage").FirstOrDefault();
            if (rampage != null)
            {
                rampage.Name = "Rampage";
                rampage.Type = "Attack";
            }

            var tornado = l.Where(x => x.Type == "Tornado").FirstOrDefault();
            if (tornado != null)
            {
                tornado.Name = "Tornado";
                tornado.Type = "Attack";
            }

            var volcano = l.Where(x => x.Type == "Volcano").FirstOrDefault();
            if (volcano != null)
            {
                volcano.Name = "Volcano";
                volcano.Type = "Attack";
            }

            //Weapons - Once

            var types = GetTypes(l);
            var tags = GetTags(l);
            var properties = GetProperties(l);
            var weapons = GetWeapons(l);
            var a = 1;

            //await _repository.InsertOrUpdateByNameAsync(l);
        }

        protected string GetTypes(List<GemDataScrapingModel> l)
        {
            var types = l.Select(x => x.Type).Distinct();
            return string.Join($",{Environment.NewLine}", types.OrderBy(x => x));
        }

        protected string GetTags(List<GemDataScrapingModel> l)
        {
            var tags = l.SelectMany(x => x.TagList).Distinct();
            return string.Join($",{Environment.NewLine}", tags.OrderBy(x => x));
        }

        protected string GetProperties(List<GemDataScrapingModel> l)
        {
            var properties = l.SelectMany(x => x.PropertyDict.Select(x => x.Key)).Distinct();
            return string.Join($",{Environment.NewLine}", properties.OrderBy(x => x));
        }

        protected string GetWeapons(List<GemDataScrapingModel> l)
        {
            var weaponsBase = l.SelectMany(x => x.RequirementList.Where(x => !x.StartsWith("Level")));

            List<string> weapons = new List<string>();
            foreach (var weaponBase in weaponsBase)
            {
                if(!weaponBase.Contains(","))
                {
                    weapons.Add(weaponBase);
                    continue;
                }

                weapons.AddRange(weaponBase.Split(',').Select(x => x.Trim()));
            }
            weapons = weapons.Distinct().ToList();

            return string.Join($",{Environment.NewLine}", weapons.OrderBy(x => x));
        }

        protected List<GemDataScrapingModel> AllGemDataScraping()
        {
            var l = SkillGemDataScraping();
            l.AddRange(SpiritGemDataScraping());
            l.AddRange(SupportGemDataScraping());

            return l;
        }

        protected List<GemDataScrapingModel> SkillGemDataScraping()
        {
            return Execute("https://poe2db.tw/us/Skill_Gems");
        }

        protected List<GemDataScrapingModel> SpiritGemDataScraping()
        {
            return Execute("https://poe2db.tw/us/Spirit_Gems");
        }

        protected List<GemDataScrapingModel> SupportGemDataScraping()
        {
            return Execute("https://poe2db.tw/us/Support_Gems");
        }

        protected List<GemDataScrapingModel> Execute(string url)
        {
            var web = new HtmlWeb();
            var document = web.Load(url);
            var htmlElements = document.DocumentNode.QuerySelectorAll("div.itemBoxContent");

            var l = new List<GemDataScrapingModel>();
            foreach (var productHTMLElement in htmlElements)
            {
                var m = new GemDataScrapingModel();

                m.Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.itemName span.lc").InnerText?.Trim());
                m.Type = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.typeLine span.lc").InnerText?.Trim());

                var lProperty = productHTMLElement.QuerySelectorAll("div.property");
                foreach (var iProperty in lProperty)
                {
                    var propertyInnerText = HtmlEntity.DeEntitize(iProperty.InnerText?.Trim());
                    if (!propertyInnerText.Contains(":"))
                    {
                        m.TagList = propertyInnerText.Split(',').ToList();
                        m.TagList = m.TagList.Where(x => !string.IsNullOrWhiteSpace(x.Trim())).Select(x => x.Trim()).ToList();
                        continue;
                    }

                    var propertySplit = propertyInnerText.Split(':');
                    m.PropertyDict.Add(propertySplit[0].Trim().TrimEnd(':'), propertySplit[1].Replace("<span class=\"colourDefault\">", string.Empty).Replace("</span>", string.Empty).Trim());
                }

                var lRequirement = productHTMLElement.QuerySelectorAll("div.requirements");
                foreach (var iRequirement in lRequirement)
                {
                    var lRequirementText = iRequirement.QuerySelectorAll("span.colourDefault");
                    m.RequirementList.Add(string.Join(", ", lRequirementText.Select(x => HtmlEntity.DeEntitize(x.InnerText?.Trim()))));
                }

                m.Description = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.secDescrText")?.InnerText?.Trim());

                var lExplicitMod = productHTMLElement.QuerySelectorAll("div.explicitMod");
                foreach (var iExplicitMod in lExplicitMod)
                    m.ExplicitModList.Add(HtmlEntity.DeEntitize(iExplicitMod.InnerText?.Trim()));

                m.Observation = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.fst-italic")?.InnerText?.Trim());

                l.Add(m);
            }

            return l;
        }

    }

    public class GemDataScrapingModel
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> TagList { get; set; } = new List<string>();
        public Dictionary<string, string> PropertyDict { get; set; } = new Dictionary<string, string>();
        public List<string> RequirementList { get; set; } = new List<string>();
        public string Description { get; set; } = string.Empty;
        public List<string> ExplicitModList { get; set; } = new List<string>();
        public string Observation { get; set; } = string.Empty;

    }
}
