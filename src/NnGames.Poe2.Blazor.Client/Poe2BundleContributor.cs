﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Bundling;

namespace NnGames.Poe2.Blazor.Client;

/* Add your global styles/scripts here.
 * See https://docs.abp.io/en/abp/latest/UI/Blazor/Global-Scripts-Styles to learn how to use it
 */
public class Poe2BundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        context.Add("bootstrap-patch.js");
    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}