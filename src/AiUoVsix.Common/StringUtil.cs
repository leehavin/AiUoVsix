using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AiUoVsix.Common
{

    public static class StringUtil
    {
        public static readonly char[] NumberChars = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static readonly char[] LowerLetterChars = new char[26]
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static readonly char[] UpperLetterChars = new char[26]
        {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
        'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
        'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public static readonly char[] LetterChars = new char[52]
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z'
        };

        public static readonly char[] NumeralRadixChars = new char[64]
        {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z', '+', '/'
        };

        public static readonly char[] CommonPunctuation = new char[30]
        {
        '`', '~', '!', '@', '#', '$', '%', '^', '&', '*',
        '(', ')', '-', '_', '=', '+', '[', ']', '{', '}',
        '\\', ';', ':', '\'', '"', '"', ',', '.', '?', '/'
        };

        private static Dictionary<char, int> _numeralRadixCache = null;

        public static readonly char[] NumberLetterChars = new char[62]
        {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z'
        };

        public static readonly char[] UsualChineseChars = "的一是在不了有和人这中大为上个国我以要他时来用们生到作地于出就分对成会可主发年动同工也能下过子说产种面而方后多定行学法所民得经十三之进着等部度家电力里如水化高自二理起小物现实加量都两体制机当使点从业本去把性好应开它合还因由其些然前外天政四日那社义事平形相全表间样与关各重新线内数正心反你明看原又么利比或但质气第向道命此变条只没结解问意建月公无系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见计别她手角期根论运农指几九区强放决西被干做必战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金增争济阶油思术极交受联什认六共权收证改清己美再采转更单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究观越织装影算低持音众书布复容儿须际商非验连断深难近矿千周委素技备半办青省列习响约支般史感劳便团往酸历市克何除消构府称太准精值号率族维划选标写存候毛亲快效斯院查江型眼王按格养易置派层片始却专状育厂京识适属圆包火住调满县局照参红细引听该铁价严".ToCharArray();

        private static readonly object _locker = new object();

        private static Regex CSharpReserved = new Regex("^(ABSTRACT|AS|BASE|BOOL|BREAK|BYTE|CASE|CATCH|CHAR|CHECKED|CLASS|CONST|CONTINUE|DECIMAL|DEFAULT|DELEGATE|DO|DOUBLE|ELSE|ENUM|EVENT|EXPLICIT|EXTERN|FALSE|FINALLY|FIXED|FLOAT|FOR|FOREACH|GET|GOTO|IF|IMPLICIT|IN|INT|INTERFACE|INTERNAL|IS|LOCK|LONG|NAMESPACE|NEW|NULL|OBJECT|OPERATOR|OUT|OVERRIDE|PARAMS|PARTIAL|PRIVATE|PROTECTED|PUBLIC|READONLY|REF|RETURN|SBYTE|SEALED|SET|SHORT|SIZEOF|STACKALLOC|STATIC|STRING|STRUCT|SWITCH|THIS|THROW|TRUE|TRY|TYPEOF|UINT|ULONG|UNCHECKED|UNSAFE|USHORT|USING|VALUE|VIRTUAL|VOID|VOLATILE|WHERE|WHILE|YIELD)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static Dictionary<char, int> NumeralRadixCache
        {
            get
            {
                if (_numeralRadixCache == null)
                {
                    lock (_locker)
                    {
                        if (_numeralRadixCache == null)
                        {
                            _numeralRadixCache = new Dictionary<char, int>(64);
                            for (int i = 0; i < NumeralRadixChars.Length; i++)
                            {
                                _numeralRadixCache.Add(NumeralRadixChars[i], i);
                            }
                        }
                    }
                }

                return _numeralRadixCache;
            }
        }


        public static int GetStringWidth(this string str)
        {
            return str.Sum((char chr) => GetCharWidth(chr));
        }

        public static int GetCharWidth(char chr)
        {
            return (chr < 'ÿ') ? 1 : 2;
        }

        public static string TrimWidth(this string str, int width)
        {
            string text = null;
            if (width >= str.Length * 2)
            {
                return str;
            }

            for (int i = 0; i < str.Length; i++)
            {
                width -= GetCharWidth(str[i]);
                if (width <= 0)
                {
                    switch (width)
                    {
                        case 0:
                            text = str.Substring(0, i + 1);
                            break;
                        case -1:
                            text = str.Substring(0, i);
                            break;
                    }

                    break;
                }
            }

            return text ?? str;
        }

        public static string TrimWidthSuffix(this string str, int width)
        {
            if (width >= str.Length * 2)
            {
                return str;
            }

            string text = null;
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                num += GetCharWidth(str[i]);
                if (num < width)
                {
                    continue;
                }

                if (num == width)
                {
                    if (i < str.Length - 1)
                    {
                        str = ((GetCharWidth(str[i]) == 2) ? (str.Substring(0, i) + "..") : (str.Substring(0, i - 1) + new string('.', GetCharWidth(str[i - 1]) + 1)));
                    }
                }
                else
                {
                    str = ((GetCharWidth(str[i - 1]) == 2) ? (str.Substring(0, i - 1) + "...") : (str.Substring(0, i - 1) + ".."));
                }

                break;
            }

            return text ?? str;
        }

        public static string ReplaceWhiteSpace(this string src, string newValue, bool repeated = false)
        {
            if (string.IsNullOrEmpty(src))
            {
                return src;
            }

            int num = int.MinValue;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < src.Length; i++)
            {
                char c = src[i];
                if (char.IsWhiteSpace(c))
                {
                    if (repeated)
                    {
                        stringBuilder.Append(newValue);
                        continue;
                    }

                    if (num != i - 1)
                    {
                        stringBuilder.Append(newValue);
                    }

                    num = i;
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

        public static string ReplaceWhiteSpace(this string src, char newChar, bool repeated = true)
        {
            return src.ReplaceWhiteSpace(newChar.ToString(), repeated);
        }

        public static string Replace(this string src, char newChar, int beginIdx, int length)
        {
            if (src.Length < beginIdx + length)
            {
                throw new Exception("起始位置加上替换长度大于源字符串长度。");
            }

            if (beginIdx < 0)
            {
                throw new Exception("参数beginIdx必须大于0");
            }

            if (length < 1)
            {
                throw new Exception("参数length必须大于1");
            }

            string text = string.Empty;
            if (beginIdx > 0)
            {
                text = src.Substring(0, beginIdx);
            }

            text += new string(newChar, length);
            if (src.Length - beginIdx - length > 0)
            {
                text += src.Substring(beginIdx + length);
            }

            return text;
        }

        public static string TrimStart(this string src, string start, bool ignoreCase = true)
        {
            string text = src;
            while (text.StartsWith(start, ignoreCase, null))
            {
                text = text.Substring(start.Length);
            }

            return text;
        }

        public static string TrimEnd(this string src, string end, bool ignoreCase = true)
        {
            string text = src;
            while (text.EndsWith(end, ignoreCase, null))
            {
                text = text.Substring(0, text.Length - end.Length);
            }

            return text;
        }

        public static string Trim(this string src, string start, string end, bool ignoreCase = true)
        {
            return src.TrimStart(start, ignoreCase).TrimEnd(end, ignoreCase);
        }

        public static string StrToHex(this string str, Encoding encode = null)
        {
            byte[] bytes = (encode ?? Encoding.UTF8).GetBytes(str);
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static string BytesToHex(byte[] input)
        {
            return BitConverter.ToString(input).Replace("-", "");
        }

        public static byte[] HexToBytes(this string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[1];
            }

            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            byte[] array = new byte[hex.Length / 2];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = byte.Parse(hex.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
            }

            return array;
        }

        public static string HexToStr(this string hex, Encoding encode = null)
        {
            encode = encode ?? Encoding.UTF8;
            return encode.GetString(hex.HexToBytes());
        }

        public static string Base64UrlEncode(this string str, Encoding encode = null)
        {
            byte[] bytes = (encode ?? Encoding.UTF8).GetBytes(str);
            return Base64UrlEncodeBytes(bytes);
        }

        public static string Base64UrlEncodeBytes(byte[] bytes)
        {
            string text = Convert.ToBase64String(bytes);
            text = text.Split('=')[0];
            text = text.Replace('+', '-');
            return text.Replace('/', '_');
        }

        public static string Base64UrlDecode(this string base64Str, Encoding encode = null)
        {
            byte[] bytes = Base64UrlDecodeBytes(base64Str);
            return (encode ?? Encoding.UTF8).GetString(bytes);
        }

        public static byte[] Base64UrlDecodeBytes(string base64Str)
        {
            string text = base64Str;
            text = text.Replace('-', '+');
            text = text.Replace('_', '/');
            switch (text.Length % 4)
            {
                case 2:
                    text += "==";
                    break;
                case 3:
                    text += "=";
                    break;
                default:
                    throw new Exception("无效的 base64url 字符串!");
                case 0:
                    break;
            }

            return Convert.FromBase64String(text);
        }

        public static string RadixToString(ulong num, uint radix)
        {
            if (num == 0)
            {
                return "0";
            }

            string text = string.Empty;
            while (num != 0)
            {
                text = NumeralRadixChars[num % radix] + text;
                num /= radix;
            }

            return text;
        }

        public static string RadixToString(long num, int radix)
        {
            string text = RadixToString((ulong)Math.Abs(num), (uint)radix);
            return (num < 0) ? ("-" + text) : text;
        }

        public static ulong StringToRadix(this string value, uint radix)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0uL;
            }

            ulong num = 0uL;
            for (int i = 0; i < value.Length; i++)
            {
                char key = value[i];
                num += (uint)NumeralRadixCache[key] * (ulong)Math.Pow(radix, value.Length - i - 1);
            }

            return num;
        }

        public static long StringToRadix(this string value, int radix)
        {
            bool flag = value[0] == '-';
            string value2 = (flag ? value.Substring(1) : value);
            ulong num = value2.StringToRadix((uint)radix);
            return flag ? ((long)num * -1L) : ((long)num);
        }

        public static string ConvertNumberBase(string source, int fromBase, int toBase)
        {
            return RadixToString(source.StringToRadix((uint)fromBase), (uint)toBase);
        }

        public static string ToBase64String(string str, Encoding encoding = null)
        {
            return Convert.ToBase64String((encoding ?? Encoding.UTF8).GetBytes(str));
        }

        public static string FromBase64String(string str, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetString(Convert.FromBase64String(str));
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string GetGuidString(bool removeSymbol = true)
        {
            string text = Convert.ToString(Guid.NewGuid());
            return removeSymbol ? text.Replace("-", "") : text;
        }

        public static int LevenshteinDistance(string source, string target, bool ignoreCase = true)
        {
            int length = source.Length;
            int length2 = target.Length;
            int[,] array = new int[length + 1, length2 + 1];
            if (length == 0)
            {
                return length2;
            }

            if (length2 == 0)
            {
                return length;
            }

            if (ignoreCase)
            {
                source = source.ToLower();
                target = target.ToLower();
            }

            int num = 0;
            while (num <= length)
            {
                array[num, 0] = num++;
            }

            int num2 = 0;
            while (num2 <= length2)
            {
                array[0, num2] = num2++;
            }

            for (int i = 1; i <= length; i++)
            {
                for (int j = 1; j <= length2; j++)
                {
                    int num3 = ((target[j - 1] != source[i - 1]) ? 1 : 0);
                    array[i, j] = Math.Min(Math.Min(array[i - 1, j] + 1, array[i, j - 1] + 1), array[i - 1, j - 1] + num3);
                }
            }

            return array[length, length2];
        }

        public static string CamelCase(this string name)
        {
            return char.ToLower(name[0]) + name.Substring(1);
        }

        public static string PascalCase(this string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        public static string[] SplitNewLine(this string src)
        {
            return src.Trim().Split(new string[3] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitSpace(this string src)
        {
            return src.Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool IsCSharpReserved(this string src)
        {
            return CSharpReserved.IsMatch(src);
        }

        public static string Tab(int n)
        {
            return (n == 0) ? string.Empty : new string('\t', n);
        }

        public static string GetFirstLine(string src)
        {
            string[] array = src.SplitNewLine();
            return (array != null && array.Length != 0) ? array[0] : string.Empty;
        }

        public static int IndexOf(string str, char value, int skipNum)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == value)
                {
                    if (num == skipNum)
                    {
                        return i;
                    }

                    num++;
                }
            }

            return -1;
        }

        public static string FormatFixedLength(object src, int fixedLen, bool leftAlign = true, char paddingChar = ' ')
        {
            string text = Convert.ToString(src);
            return leftAlign ? text.PadRight(fixedLen, paddingChar) : text.PadLeft(fixedLen, paddingChar);
        }

        public static List<string> ExtractMatches(this string src, string begin, string end)
        {
            if (string.IsNullOrEmpty(src))
            {
                return null;
            }

            List<string> list = new List<string>();
            Regex regex = new Regex("\\" + begin + "(\\w+)\\" + end);
            MatchCollection matchCollection = regex.Matches(src);
            foreach (Match item in matchCollection)
            {
                list.Add(Convert.ToString(item.Groups[1]));
            }

            return list;
        }

        public static string HideFixedLength(this string src, int toFixedLength, int beginReservedLen, int endReservedLen, char paddingChar = '*')
        {
            if (string.IsNullOrEmpty(src) || (beginReservedLen == 0 && endReservedLen == 0))
            {
                return string.Empty.PadRight(toFixedLength, paddingChar);
            }

            if (beginReservedLen == 0)
            {
                return src.Substring(src.Length - endReservedLen).PadLeft(toFixedLength, paddingChar);
            }

            if (endReservedLen == 0)
            {
                return src.Substring(0, beginReservedLen).PadRight(toFixedLength, paddingChar);
            }

            string text = src.Substring(0, beginReservedLen);
            string text2 = src.Substring(src.Length - endReservedLen);
            return text.PadRight(toFixedLength - endReservedLen, paddingChar) + text2;
        }

        public static bool IsASCII(string str)
        {
            return MatchCharacterSetOfAll(str, IsASCII);
        }

        public static bool IsGB2312(string str)
        {
            return MatchCharacterSetOfAll(str, IsGB2312);
        }

        public static bool IsGBK(string str)
        {
            return MatchCharacterSetOfAll(str, IsGBK);
        }

        private static bool MatchCharacterSetOfAll(string str, Func<char, bool> func)
        {
            bool result = true;
            foreach (char arg in str)
            {
                if (!func(arg))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool IsASCII(this char chr)
        {
            return chr <= '\u007f';
        }

        public static bool IsGB2312(this char chr)
        {
            bool flag = true;
            if (chr.IsASCII())
            {
                return true;
            }

            byte[] bytes = Encoding.GetEncoding(20936).GetBytes(new char[1] { chr });
            if (bytes.Length == 2)
            {
                byte b = bytes[0];
                byte b2 = bytes[1];
                flag = b >= 176 && b <= 247 && b2 >= 160 && b2 <= 254;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        public static bool IsGBK(this char chr)
        {
            bool flag = true;
            if (chr.IsASCII())
            {
                return true;
            }

            byte[] bytes = Encoding.GetEncoding(936).GetBytes(new char[1] { chr });
            if (bytes.Length == 2)
            {
                byte b = bytes[0];
                byte b2 = bytes[1];
                flag = b >= 129 && b <= 254 && b2 >= 64 && b2 <= 254;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        public static bool IsGB18030(this char chr)
        {
            bool flag = true;
            if (chr.IsASCII())
            {
                return true;
            }

            byte[] bytes = Encoding.GetEncoding(54936).GetBytes(new char[1] { chr });
            if (bytes.Length == 2)
            {
                byte b = bytes[0];
                byte b2 = bytes[1];
                flag = (b >= 129 && b <= 254 && b2 >= 64 && b2 <= 126) || (b >= 129 && b <= 254 && b2 >= 128 && b2 <= 254);
            }
            else if (bytes.Length == 4)
            {
                byte b3 = bytes[0];
                byte b4 = bytes[1];
                byte b5 = bytes[2];
                byte b6 = bytes[3];
                flag = b3 >= 129 && b3 <= 254 && b4 >= 48 && b4 <= 57 && b5 >= 129 && b5 <= 254 && b6 >= 48 && b6 <= 57;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        public static string DefaultIfNull(this string src, string defaultValue)
        {
            return src ?? defaultValue;
        }

        public static string DefaultIfNullOrEmpty(this string src, string defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src;
        }


        public static T To<T>(this string src)
        {
            return (T)Convert.ChangeType(src, typeof(T), null);
        }

        public static T To<T>(this string src, T defaultValue)
        {
            T val = default(T);
            try
            {
                return src.To<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T To<T>(this string src, IFormatProvider provider)
        {
            return (T)Convert.ChangeType(src, typeof(T), provider);
        }

        public static T To<T>(this string src, T defaultValue, IFormatProvider provider)
        {
            T val = default(T);
            try
            {
                return src.To<T>(provider);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T To<T>(this string src, Func<string, T> converter)
        {
            return (converter ?? new Func<string, T>(To<T>))(src);
        }

        public static T To<T>(this string src, T defaultValue, Func<string, T> converter)
        {
            T val = default(T);
            try
            {
                return src.To(converter);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T? ToN<T>(this string src) where T : struct
        {
            T? result = null;
            if (!string.IsNullOrEmpty(src))
            {
                result = src.To<T>();
            }

            return result;
        }

        public static T? ToN<T>(this string src, IFormatProvider provider) where T : struct
        {
            return (!string.IsNullOrEmpty(src)) ? new T?(src.To<T>(provider)) : null;
        }

        public static T? ToN<T>(this string src, T defaultValue) where T : struct
        {
            return (!string.IsNullOrEmpty(src)) ? new T?(src.To(defaultValue)) : null;
        }

        public static T? ToN<T>(this string src, T defaultValue, IFormatProvider provider) where T : struct
        {
            return (!string.IsNullOrEmpty(src)) ? new T?(src.To(defaultValue, provider)) : null;
        }

        public static T? ToN<T>(this string src, Func<string, T> converter) where T : struct
        {
            return (!string.IsNullOrEmpty(src)) ? new T?(src.To(converter)) : null;
        }

        public static T? ToN<T>(this string src, T defaultValue, Func<string, T> converter) where T : struct
        {
            return (!string.IsNullOrEmpty(src)) ? new T?(src.To(defaultValue, converter)) : null;
        }

        public static sbyte ToSByte(this string src)
        {
            return src.To((string str) => sbyte.Parse(str, NumberStyles.Any));
        }

        public static sbyte ToSByte(this string src, sbyte defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToSByte);
        }

        public static sbyte? ToSByteN(this string src)
        {
            return src.ToN(ToSByte);
        }

        public static sbyte? ToSByteN(this string src, sbyte defaultValue)
        {
            return src.ToN(defaultValue, ToSByte);
        }

        public static byte ToByte(this string src)
        {
            return src.To((string str) => byte.Parse(str, NumberStyles.Any));
        }

        public static byte ToByte(this string src, byte defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToByte);
        }

        public static byte? ToByteN(this string src)
        {
            return src.ToN(ToByte);
        }

        public static byte? ToByteN(this string src, byte defaultValue)
        {
            return src.ToN(defaultValue, ToByte);
        }

        public static short ToInt16(this string src)
        {
            return src.To((string str) => short.Parse(str, NumberStyles.Any));
        }

        public static short ToInt16(this string src, short defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToInt16);
        }

        public static short? ToInt16N(this string src)
        {
            return src.ToN(ToInt16);
        }

        public static short? ToInt16N(this string src, short defaultValue)
        {
            return src.ToN(defaultValue, ToInt16);
        }

        public static ushort ToUInt16(this string src)
        {
            return src.To((string str) => ushort.Parse(str, NumberStyles.Any));
        }

        public static ushort ToUInt16(this string src, ushort defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToUInt16);
        }

        public static ushort? ToUInt16N(this string src)
        {
            return src.ToN(ToUInt16);
        }

        public static ushort? ToUInt16N(this string src, ushort defaultValue)
        {
            return src.ToN(defaultValue, ToUInt16);
        }

        public static int ToInt32(this string src)
        {
            return src.To((string str) => int.Parse(str, NumberStyles.Any));
        }

        public static int ToInt32(this string src, int defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToInt32);
        }

        public static int? ToInt32N(this string src)
        {
            return src.ToN(ToInt32);
        }

        public static int? ToInt32N(this string src, int defaultValue)
        {
            return src.ToN(defaultValue, ToInt32);
        }

        public static uint ToUInt32(this string src)
        {
            return src.To((string str) => uint.Parse(str, NumberStyles.Any));
        }

        public static uint ToUInt32(this string src, uint defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToUInt32);
        }

        public static uint? ToUInt32N(this string src)
        {
            return src.ToN(ToUInt32);
        }

        public static uint? ToUInt32N(this string src, uint defaultValue)
        {
            return src.ToN(defaultValue, ToUInt32);
        }

        public static long ToInt64(this string src)
        {
            return src.To((string str) => long.Parse(str, NumberStyles.Any));
        }

        public static long ToInt64(this string src, long defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToInt64);
        }

        public static long? ToInt64N(this string src)
        {
            return src.ToN(ToInt64);
        }

        public static long? ToInt64N(this string src, long defaultValue)
        {
            return src.ToN(defaultValue, ToInt64);
        }

        public static ulong ToUInt64(this string src)
        {
            return src.To((string str) => ulong.Parse(str, NumberStyles.Any));
        }

        public static ulong ToUInt64(this string src, ulong defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToUInt64);
        }

        public static ulong? ToUInt64N(this string src)
        {
            return src.ToN(ToUInt64);
        }

        public static ulong? ToUInt64N(this string src, ulong defaultValue)
        {
            return src.ToN(defaultValue, ToUInt64);
        }

        public static float ToSingle(this string src)
        {
            return src.To((string str) => float.Parse(str, NumberStyles.Any));
        }

        public static float ToSingle(this string src, float defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToSingle);
        }

        public static float? ToSingleN(this string src)
        {
            return src.ToN(ToSingle);
        }

        public static float? ToSingleN(this string src, float defaultValue)
        {
            return src.ToN(defaultValue, ToSingle);
        }

        public static double ToDouble(this string src)
        {
            return src.To((string str) => double.Parse(str, NumberStyles.Any));
        }

        public static double ToDouble(this string src, double defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToDouble);
        }

        public static double? ToDoubleN(this string src)
        {
            return src.ToN(ToDouble);
        }

        public static double? ToDoubleN(this string src, double defaultValue)
        {
            return src.ToN(defaultValue, ToDouble);
        }

        public static decimal ToDecimal(this string src)
        {
            return src.To((string str) => decimal.Parse(str, NumberStyles.Any));
        }

        public static decimal ToDecimal(this string src, decimal defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToDecimal);
        }

        public static decimal? ToDecimalN(this string src)
        {
            return src.ToN(ToDecimal);
        }

        public static decimal? ToDecimalN(this string src, decimal defaultValue)
        {
            return src.ToN(defaultValue, ToDecimal);
        }


        public static char ToChar(this string src)
        {
            return src.To(char.Parse);
        }

        public static char ToChar(this string src, char defaultValue)
        {
            return string.IsNullOrEmpty(src) ? defaultValue : src.To(defaultValue, ToChar);
        }

        public static char? ToCharN(this string src)
        {
            return src.ToN(ToChar);
        }

        public static char? ToCharN(this string src, char defaultValue)
        {
            return src.ToN(defaultValue, ToChar);
        }

     
        public static bool IsChinese(char chr)
        {
            return chr >= '一' && chr <= '龥';
        }

        public static bool IsChinese(string text)
        {
            foreach (char chr in text)
            {
                if (!IsChinese(chr))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }

        public static bool IsTelephone(string telephone)
        {
            return Regex.IsMatch(telephone, "^(\\\\d{3,4}-)\\\\d{7,8}$");
        }

        public static bool IsMobile(string mobile)
        {
            return Regex.IsMatch(mobile, "^[1][3,4,5,6,7,8][0-9]{9}$");
        }

        public static bool IsIpAddress(string ip)
        {
            return Regex.IsMatch(ip, "^(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])$");
        }

        public static bool IsPostcode(string postcode)
        {
            return Regex.IsMatch(postcode, "[1-9]\\d{5}(?!\\d)");
        }

        public static bool IsUrl(string url)
        {
            return Regex.IsMatch(url, "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");
        }

        public static bool IsNotNegativeNumber(string input)
        {
            return Regex.IsMatch(input, "^\\d+[.]?\\d*$");
        }

        public static bool IsNumber(string input)
        {
            return Regex.IsMatch(input, "^[-]?\\d+[.]?\\d*$");
        }

        public static bool IsNotNegativeInteger(string input)
        {
            return Regex.IsMatch(input, "^\\d+$");
        }

        public static bool IsInteger(string input)
        {
            return Regex.IsMatch(input, "^-?\\d+$");
        }

        public static bool IsNegativeInteger(string input)
        {
            return Regex.IsMatch(input, "^-[0-9]*[1-9][0-9]*$");
        }

        public static bool IsPositiveInteger(string input)
        {
            return Regex.IsMatch(input, "^[0-9]*[1-9][0-9]*$");
        }

        public static bool IsNotPositiveInteger(string input)
        {
            return Regex.IsMatch(input, "^((-\\d+)|(0+))$");
        }

    }
}
