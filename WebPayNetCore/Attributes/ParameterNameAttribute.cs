using System;

namespace WebPayNetCore.Attributes
{
    public class ParameterNameAttribute : Attribute
    {
        public static readonly ParameterNameAttribute Default = new ParameterNameAttribute();

        public ParameterNameAttribute()
            : this(string.Empty)
        {
        }

        public ParameterNameAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (obj is ParameterNameAttribute displayNameAttribute)
                return displayNameAttribute.ParameterName == ParameterName;
            return false;
        }

        public override int GetHashCode()
        {
            return ParameterName.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return Equals(Default);
        }
    }
}