using System;
using System.Runtime.CompilerServices;

namespace AiUoVsix.Common.Windows
{
    public static class PathInternalNetCore
    {
        public const char DirectorySeparatorChar = '\\';

        public const char AltDirectorySeparatorChar = '/';

        public const char VolumeSeparatorChar = ':';

        public const char PathSeparator = ';';

        public const string ExtendedDevicePathPrefix = "\\\\?\\";

        public const string UncPathPrefix = "\\\\";

        public const string UncDevicePrefixToInsert = "?\\UNC\\";

        public const string UncExtendedPathPrefix = "\\\\?\\UNC\\";

        public const string DevicePathPrefix = "\\\\.\\";

        public const int DevicePrefixLength = 4;

        public static bool AreRootsEqual(string first, string second, StringComparison comparisonType)
        {
            int rootLength = GetRootLength(first);
            int rootLength2 = GetRootLength(second);
            return rootLength == rootLength2 && string.Compare(first, 0, second, 0, rootLength, comparisonType) == 0;
        }

        public static int GetRootLength(string path)
        {
            int i = 0;
            int num = 2;
            int num2 = 2;
            bool flag = path.StartsWith("\\\\?\\");
            bool flag2 = path.StartsWith("\\\\?\\UNC\\");
            if (flag)
            {
                if (flag2)
                {
                    num2 = "\\\\?\\UNC\\".Length;
                }
                else
                {
                    num += "\\\\?\\".Length;
                }
            }

            if ((!flag || flag2) && path.Length > 0 && IsDirectorySeparator(path[0]))
            {
                i = 1;
                if (flag2 || (path.Length > 1 && IsDirectorySeparator(path[1])))
                {
                    i = num2;
                    int num3 = 2;
                    for (; i < path.Length; i++)
                    {
                        if (IsDirectorySeparator(path[i]) && --num3 <= 0)
                        {
                            break;
                        }
                    }
                }
            }
            else if (path.Length >= num && path[num - 1] == NetFxRelativePath.VolumeSeparatorChar)
            {
                i = num;
                if (path.Length >= num + 1 && IsDirectorySeparator(path[num]))
                {
                    i++;
                }
            }

            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDirectorySeparator(char c)
        {
            return c == NetFxRelativePath.DirectorySeparatorChar || c == NetFxRelativePath.AltDirectorySeparatorChar;
        }

        public static int GetCommonPathLength(string first, string second, bool ignoreCase)
        {
            int num = EqualStartingCharacterCount(first, second, ignoreCase);
            if (num == 0)
            {
                return num;
            }

            if (num == first.Length && (num == second.Length || IsDirectorySeparator(second[num])))
            {
                return num;
            }

            if (num == second.Length && IsDirectorySeparator(first[num]))
            {
                return num;
            }

            while (num > 0 && !IsDirectorySeparator(first[num - 1]))
            {
                num--;
            }

            return num;
        }

        public unsafe static int EqualStartingCharacterCount(string first, string second, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
            {
                return 0;
            }

            int num = 0;
            fixed (char* ptr = first)
            {
                fixed (char* ptr3 = second)
                {
                    char* ptr2 = ptr;
                    char* ptr4 = ptr3;
                    char* ptr5 = ptr2 + first.Length;
                    char* ptr6 = ptr4 + second.Length;
                    while (ptr2 != ptr5 && ptr4 != ptr6 && (*ptr2 == *ptr4 || (ignoreCase && char.ToUpperInvariant(*ptr2) == char.ToUpperInvariant(*ptr4))))
                    {
                        num++;
                        ptr2++;
                        ptr4++;
                    }
                }
            }

            return num;
        }

        internal static bool EndsInDirectorySeparator(string path)
        {
            return path.Length > 0 && IsDirectorySeparator(path[path.Length - 1]);
        }
    }
}