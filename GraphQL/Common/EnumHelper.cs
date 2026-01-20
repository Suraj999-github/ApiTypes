using System.ComponentModel;

namespace GraphQL.Common
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo != null)
            {
                var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
            return value.ToString();
        }
    }
}
