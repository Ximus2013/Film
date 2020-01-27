using System;
using System.ComponentModel;
using System.Linq;

namespace film.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum GenericEnum)
        {
            var enumType = GenericEnum.GetType();
            var memberInfo = enumType.GetMember(GenericEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Count() > 0)
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
}