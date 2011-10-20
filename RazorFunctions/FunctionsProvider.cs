﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.WebPages;

using Composite.Core;
using Composite.Functions;
using Composite.Functions.Plugins.FunctionProvider;

namespace CompositeC1Contrib.RazorFunctions
{
    public class FunctionsProvider : IFunctionProvider
    {
        public FunctionNotifier FunctionNotifier
        {
            set { }
        }

        public IEnumerable<IFunction> Functions
        {
            get
            {
                var virtualPath = "~/App_Data/Razor";
                var absolutePath = HostingEnvironment.MapPath(virtualPath);

                var files = new DirectoryInfo(absolutePath).EnumerateFiles("*.cshtml", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var parts = file.FullName.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

                    string ns = "";
                    string name = Path.GetFileNameWithoutExtension(parts[parts.Length - 1]);

                    for (int i = parts.Length - 2; i > 0; i--)
                    {
                        if (parts[i].Equals("Razor", StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }

                        ns = parts[i] + "." + ns;
                    }

                    ns = ns.Substring(0, ns.Length - 1);

                    IList<FunctionParameterHolder> parameters = new List<FunctionParameterHolder>();
                    var relativeFilePath = Path.Combine(virtualPath, ns.Replace(".", Path.DirectorySeparatorChar.ToString()), name + ".cshtml");


                    WebPageBase webPage = null;

                    try
                    {
                        webPage = WebPage.CreateInstanceFromVirtualPath(relativeFilePath);
                    }
                    catch (Exception exc)
                    {
                        Log.LogError("Error in instantiating razor function", exc);

                        continue;
                    }

                    var fields = webPage.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (var field in fields)
                    {
                        var att = field.GetCustomAttributes(typeof(FunctionParameterAttribute), false).Cast<FunctionParameterAttribute>().FirstOrDefault();
                        if (att != null)
                        {
                            var myField = field;
                            var holder = new FunctionParameterHolder(field.Name, field.FieldType, att, (p, o) => myField.SetValue(p, o));

                            parameters.Add(holder);
                        }
                    }

                    var properties = webPage.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (var prop in properties)
                    {
                        var att = prop.GetCustomAttributes(typeof(FunctionParameterAttribute), false).Cast<FunctionParameterAttribute>().FirstOrDefault();
                        if (att != null)
                        {
                            var myProp = prop;
                            var holder = new FunctionParameterHolder(prop.Name, prop.PropertyType, att, (p, o) => myProp.SetValue(p, o, null));

                            parameters.Add(holder);
                        }
                    }

                    yield return new RazorFunction(ns, name, parameters, relativeFilePath);
                }
            }
        }
    }
}
