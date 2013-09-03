﻿using System;
using System.Linq;

namespace CompositeC1Contrib.FormBuilder.Web.UI
{
    public class POCOFormTypeHelper
    {
        public static FormModel FromBaseForm<T>(T instance, FormOptions options) where T : IPOCOForm
        {
            var formType = typeof(T);

            var model = new FormModel
            {
                Name = formType.FullName,
                Options = options,
                OnSubmitHandler = instance.Submit
            };

            if (instance is IValidationHandler)
            {
                model.OnValidateHandler = ((IValidationHandler)instance).OnValidate;
            }

            foreach (var itm in formType.GetCustomAttributes(true).Cast<Attribute>())
            {
                model.Attributes.Add(itm);
            }

            foreach (var prop in formType.GetProperties())
            {
                var attributes = prop.GetCustomAttributes(true).Cast<Attribute>().ToList();
                var field = new FormField(model, prop.Name, prop.PropertyType, attributes);

                model.Fields.Add(field);
            }

            return model;
        }
    }
}
