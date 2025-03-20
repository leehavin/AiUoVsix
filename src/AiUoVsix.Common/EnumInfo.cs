using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AiUoVsix.Common
{

    public class EnumInfo
    {
        private Dictionary<int, EnumItem> _itemsInt;

        private Dictionary<string, EnumItem> _itemsStr;

        private Dictionary<string, int> _itemsMapDic;

        public Type EnumType { get; private set; }

        public string Name => EnumType.Name;

        public string Description { get; set; }

        public EnumItem this[int enumValue] => _itemsInt[enumValue];

        public EnumItem this[string enumName] => _itemsStr[enumName];

        public bool Exist(int value)
        {
            return _itemsInt.ContainsKey(value);
        }

        public bool Exist(string name)
        {
            return _itemsStr.ContainsKey(name);
        }

        public EnumInfo(Type enumType)
        {
            EnumType = enumType;
            Description = (Attribute.GetCustomAttribute(EnumType, typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description;
            FieldInfo[] fields = EnumType.GetFields(BindingFlags.Static | BindingFlags.Public);
            _itemsInt = new Dictionary<int, EnumItem>();
            _itemsStr = new Dictionary<string, EnumItem>();
            _itemsMapDic = new Dictionary<string, int>();
            FieldInfo[] array = fields;
            foreach (FieldInfo fieldInfo in array)
            {
                DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
                string text = (Attribute.GetCustomAttribute(fieldInfo, typeof(EnumMapAttribute)) as EnumMapAttribute)?.MapName;
                EnumItem enumItem = new EnumItem
                {
                    Name = fieldInfo.Name,
                    MapName = text,
                    Value = (int)Enum.Parse(EnumType, fieldInfo.Name),
                    Description = descriptionAttribute?.Description,
                    FieldInfo = fieldInfo
                };
                _itemsInt.Add(enumItem.Value, enumItem);
                _itemsStr.Add(enumItem.Name, enumItem);
                if (!string.IsNullOrEmpty(text))
                {
                    _itemsMapDic.Add(text, enumItem.Value);
                }
            }
        }

        public List<EnumItem> GetList()
        {
            return _itemsInt.Values.ToList();
        }

        public bool TryGetItem(int enumValue, out EnumItem value)
        {
            return _itemsInt.TryGetValue(enumValue, out value);
        }

        public EnumItem GetItem(int enumValue)
        {
            EnumItem value;
            return TryGetItem(enumValue, out value) ? value : null;
        }

        public bool TryGetItem(string enumName, out EnumItem value)
        {
            return _itemsStr.TryGetValue(enumName, out value);
        }

        public EnumItem GetItem(string enumName)
        {
            EnumItem value;
            return TryGetItem(enumName, out value) ? value : null;
        }

        public EnumItem GetItem(object value)
        {
            int enumValue = ((value is int || value.GetType() == EnumType) ? ((int)value) : ((int)Enum.Parse(EnumType, Convert.ToString(value), ignoreCase: true)));
            return GetItem(enumValue);
        }

        public bool TryGetItemByMap(string mapName, out int value)
        {
            return _itemsMapDic.TryGetValue(mapName, out value);
        }
    }
}
