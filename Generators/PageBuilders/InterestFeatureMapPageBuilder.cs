using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JNCC.Microsite.SAC.Helpers.Generators;
using JNCC.Microsite.SAC.Models.Data;
using JNCC.Microsite.SAC.Models.Website;
using Microsoft.Extensions.DependencyInjection;
using JNCC.Microsite.SAC.Helpers.Website;

namespace JNCC.Microsite.SAC.Generators.PageBuilders
{
    public static class InterestFeatureMapPageBuilder
    {
        
        public static Task<string> RenderPage(IServiceScopeFactory scopeFactory, GeneratorConfig config, InterestFeature feature)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {   
                var helper = RenderHelper.GetRendererHelper(serviceScope);
                var isHabitat = InterestFeatureHelpers.IsHabitatCode(feature.Code);

                var breadcrumbs = new List<(string href, string text, bool current)> { ("/", "Home", true) };
                
                if (isHabitat)
                {
                    breadcrumbs.Add(("/habitat", "Habitats", true));
                    breadcrumbs.Add((string.Format("/habitat/{0}", feature.Code), feature.Name, true));
                    breadcrumbs.Add((string.Format("/habitat/{0}/map", feature.Code), "Map", true));
                }
                else
                {
                    breadcrumbs.Add(("/species", "Species", true));
                    breadcrumbs.Add((string.Format("/species/{0}", feature.Code), feature.Name, true));
                    breadcrumbs.Add((string.Format("/species/{0}/map", feature.Code), "Map", true));
                }

                var model = new InterestFeaturePage
                {
                    GeneratorConfig = config,
                    Breadcrumbs = breadcrumbs,
                    CurrentSection = isHabitat ? "Habitat" : "Species",
                    InterestFeature = feature,
                    Title = StringHelpers.RemoveHTMLTags(String.Format("{0} ({1}) SAC/SCI/cSAC distribution map - {2}", feature.LayTitle, feature.Name, Page.DefaultTitle))
                };

                return helper.RenderViewToStringAsync("Views/InterestFeatureMap.cshtml", model);
            }
        }
    }
}
