﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompositeC1Contrib.FormBuilder.Web.UI
{
    public class DropdownInputElement : IInputElementHandler
    {
        public string ElementName
        {
            get { return "selectbox"; }
        }

        public IHtmlString GetHtmlString(FormField field, IDictionary<string, object> htmlAttributes)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("<select name=\"{0}\" id=\"{1}\" {2}>",
                        HttpUtility.HtmlAttributeEncode(field.Name),
                        HttpUtility.HtmlAttributeEncode(field.Id),
                        FormRenderer.WriteClass(htmlAttributes));

            if (field.DataSource != null && field.DataSource.Any())
            {
                var value = field.Value;
                var selectLabel = field.OwningForm.Options.HideLabels ? field.Label.Label : Localization.Widgets_Dropdown_SelectLabel;

                sb.AppendFormat("<option value=\"\" selected=\"selected\" disabled=\"disabled\">{0}</option>", HttpUtility.HtmlEncode(selectLabel));

                foreach (var item in field.DataSource)
                {
                    sb.AppendFormat("<option value=\"{0}\" {1}>{2}</option>",
                        HttpUtility.HtmlAttributeEncode(item.Key),
                        FormRenderer.WriteChecked(item.Key == (value ?? String.Empty).ToString(), "selected"),
                        HttpUtility.HtmlEncode(item.Value));
                }
            }

            sb.Append("</select>");

            return new HtmlString(sb.ToString());
        }
    }
}
