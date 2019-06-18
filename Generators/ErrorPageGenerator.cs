using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using JNCC.Microsite.SAC.Models.Data;
using JNCC.Microsite.SAC.Models.Website;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using JNCC.Microsite.SAC.Generators.PageBuilders;
using JNCC.Microsite.SAC.Helpers;
using JNCC.Microsite.SAC.Helpers.Generators;

namespace JNCC.Microsite.SAC.Generators
{
    public static class ErrorPageGenerator
    {
        public static void Generate(IServiceScopeFactory serviceScopeFactory, GeneratorConfig config, string basePath, bool generateSearchDocuments, string searchIndex)
        {
            Console.WriteLine("Generate 404 Page");
            var notFoundContent = ErrorPageBuilder.RenderPage(serviceScopeFactory, 404, config).Result;
            FileHelper.WriteToFile(FileHelper.GetActualFilePath(basePath, "output/html/404.html"), notFoundContent);
        }
    }
}