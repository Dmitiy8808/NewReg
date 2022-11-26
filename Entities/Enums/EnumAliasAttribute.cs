using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumAliasAttribute : Attribute
    {
        public readonly string Description;

        public EnumAliasAttribute(string description) => this.Description = description;
    }
}