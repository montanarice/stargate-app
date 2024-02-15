using System.ComponentModel;
using System.Reflection;

namespace StargateAPI.Business.Enums;

public static class EnumExtensions
{
    public static string PrintPretty(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        DescriptionAttribute? attribute = null;

        if (field is not null)
        {
            attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) 
                as DescriptionAttribute;
        }

        return attribute == null ? value.ToString() : attribute.Description;
    }
}