﻿using System;
using System.Reflection;

namespace CompositeC1Contrib.FormBuilder.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FormValidationAttribute : Attribute
    {
        public string Message { get; private set; }

        public FormValidationAttribute(string message)
        {
            Message = message;
        }

        public abstract FormValidationRule CreateRule(PropertyInfo prop, BaseForm form);
    }
}