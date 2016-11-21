﻿using System.Collections;
using System.Globalization;
using System.Resources;

using Composite;

namespace CompositeC1Contrib.Localization
{
    public class C1ResourceWriter : IResourceWriter
    {
        private string _resourceSet;
        private CultureInfo _culture;

        private IDictionary resourceList = new Hashtable();

        public C1ResourceWriter(CultureInfo culture) : this(null, culture) { }

        public C1ResourceWriter(string resourceSet, CultureInfo culture)
        {
            _resourceSet = resourceSet;
            _culture = culture;
        }

        public void AddResource(string name, string value)
        {
            AddResource(name, (object)value);
        }

        public void AddResource(string name, byte[] value)
        {
            AddResource(name, (object)value);
        }

        public void AddResource(string name, object value)
        {
            Verify.ArgumentNotNull(name, "name");

            resourceList[name] = value;
        }

        public void Generate()
        {
            var dataManager = C1ResourceDataManager.Instance;

            dataManager.GenerateResources(resourceList, _resourceSet, _culture);
        }

        public void Close()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Generate();
            }
        }
    }
}
