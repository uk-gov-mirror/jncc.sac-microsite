using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JNCC.Microsite.SAC.Helpers.Generators;
using JNCC.Microsite.SAC.Models.Data;
using JNCC.Microsite.SAC.Models.Website;
using JNCC.Microsite.SAC.Helpers.Website;
using Microsoft.Extensions.DependencyInjection;

namespace JNCC.Microsite.SAC.Generators.PageBuilders
{
    public static class InterestFeatureListPageBuilder
    {
        public static Task<string> RenderPage(IServiceScopeFactory scopeFactory, bool habitat, List<InterestFeature> features)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var helper = RenderHelper.GetRendererHelper(serviceScope);
                
                var breadcrumbs = new List<(string href, string text, bool current)> { ("/", "Home", true), habitat ? ("/habitat", "Habitats", true) : ("/species", "Species", true)};

                return helper.RenderViewToStringAsync("Views/InterestFeatureList.cshtml", new InterestFeatureListPage
                {
                    Breadcrumbs = breadcrumbs,
                    CurrentSection = habitat ? "Habitats" : "Species",
                    Type = habitat ? "Habitats" : "Species",
                    InterestFeatureSections = features.GroupBy(s => s.SectionTitle).Select(s => new InterestFeatureSection {
                        SectionTitle = s.Key,
                        InterestFeatures = s.ToList()
                    }).ToList(),
                    Title = StringHelpers.RemoveHTMLTags(String.Format("{0} - {1}",  habitat ? "Habitat Interest Features" : "Species Interest Features", Page.DefaultTitle))
                });
            }

        }
    }
}