using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiUoVsix.Common
{
    public static class EnumUtil
    {
        private static readonly ConcurrentDictionary<string, EnumInfo> _descsCache = new ConcurrentDictionary<string, EnumInfo>();

        public static EnumInfo GetInfo(Type enumType)
        {
            EnumInfo ret = null;
            if (!_descsCache.TryGetValue(enumType.FullName, out ret))
            {
                ret = new EnumInfo(enumType);
                _descsCache.AddOrUpdate(enumType.FullName, ret, (string key, EnumInfo value) => ret);
            }

            return ret;
        }

        public static EnumInfo GetInfo<T>()
        {
            return GetInfo(typeof(T));
        }

        public static EnumItem GetItemInfo(Type enumType, object value)
        {
            return GetInfo(enumType).GetItem(value);
        }

        public static EnumItem GetItemInfo<T>(object value)
        {
            return GetItemInfo(typeof(T), value);
        }

        public static EnumItem GetItemInfo(this Enum value)
        {
            return GetItemInfo(value.GetType(), value);
        }

        public static string GetDescription(Type enumType)
        {
            return GetInfo(enumType).Description;
        }

        public static string GetDescription<T>()
        {
            return GetInfo<T>().Description;
        }

        public static string GetItemDescription(Type enumType, object value)
        {
            return GetItemInfo(enumType, value).Description;
        }

        public static string GetItemDescription<T>(object value)
        {
            return GetItemInfo<T>(value).Description;
        }

        public static string GetItemDescription(this Enum value)
        {
            return GetItemInfo(value.GetType(), value).Description;
        }

        public static bool HasFlag(int value, int flag)
        {
            return (value & flag) != 0;
        }

        public static bool HasFlag(Enum variable, Enum flag)
        {
            ulong num = Convert.ToUInt64(flag);
            return (Convert.ToUInt64(variable) & num) == num;
        }

        public static T ToEnum<T>(this int value) where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                throw new Exception($"int数值{value}在枚举{typeof(T).FullName}中没有定义");
            }

            return (T)(object)value;
        }

        public static T? ToEnumN<T>(this int value) where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                return null;
            }

            return (T)(object)value;
        }

        public static T ToEnum<T>(this int value, T defaultValue) where T : Enum
        {
            return Enum.IsDefined(typeof(T), value) ? ((T)(object)value) : defaultValue;
        }

        public static T ToEnum<T>(this string value) where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
            }
            catch
            {
                throw new Exception("string数值" + value + "在枚举" + typeof(T).FullName + "中没有定义");
            }
        }

        public static T? ToEnumN<T>(this string value) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
            }
            catch
            {
                return null;
            }
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T ToEnumByMap<T>(this string value) where T : Enum
        {
            try
            {
                EnumInfo info = GetInfo<T>();
                if (!info.TryGetItemByMap(value, out var value2))
                {
                    throw new Exception("Enum类型没有定义EnumMapAttribute进行映射。type: " + typeof(T).FullName + " string:" + value);
                }

                return value2.ToEnum<T>();
            }
            catch
            {
                throw new Exception("string数值" + value + "在枚举" + typeof(T).FullName + "中没有定义");
            }
        }
    }
}
