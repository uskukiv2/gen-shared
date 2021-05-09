using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace alms.cherry.data.objects.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityIdAttribute : Attribute
    {
    }
}
