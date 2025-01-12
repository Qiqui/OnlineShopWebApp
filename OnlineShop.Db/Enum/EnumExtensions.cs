using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OnlineShop.Db.Enum
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(System.Enum enumValue)
        {
            return enumValue.GetType()
                ?.GetMember(enumValue.ToString())
                ?.First()
                ?.GetCustomAttribute<DisplayAttribute>()
                ?.GetName() ?? "Null";
        }
    }
}
