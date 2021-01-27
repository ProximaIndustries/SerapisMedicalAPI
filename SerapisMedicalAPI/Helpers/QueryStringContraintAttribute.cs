﻿using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Helpers
{
    public class QueryStringContraintAttribute : ActionMethodSelectorAttribute
    {

        public QueryStringContraintAttribute(string valueName, bool valuePresent)
        {
            this.ValueName = valueName;
            this.ValuePresent = valuePresent;
        }

        public string ValueName { get; private set; }
        public bool ValuePresent { get; private set; }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var value = routeContext.HttpContext.Request.Query[this.ValueName];
            
            if(this.ValuePresent)
            {
                return !StringValues.IsNullOrEmpty(value);
            }

            return StringValues.IsNullOrEmpty(value);
        }
    }
}
