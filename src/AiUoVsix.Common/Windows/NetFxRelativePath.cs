using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AiUoVsix.Common.Windows
{
    static class NetFxRelativePath
    {
        public static readonly char DirectorySeparatorChar = '\\';

        public static readonly char AltDirectorySeparatorChar = '/';

        public static readonly char VolumeSeparatorChar = ':';

        public static readonly char PathSeparator = ';';

        internal static StringComparison StringComparison => StringComparison.OrdinalIgnoreCase;

        public static string GetRelativePath(string relativeTo, string path)
        {
            return GetRelativePath(relativeTo, path, StringComparison);
        }

        private static string GetRelativePath(string relativeTo, string path, StringComparison comparisonType)
        {
            if (string.IsNullOrEmpty(relativeTo))
            {
                throw new ArgumentNullException("relativeTo");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            Debug.Assert(comparisonType == StringComparison.Ordinal || comparisonType == StringComparison.OrdinalIgnoreCase);
            relativeTo = Path.GetFullPath(relativeTo);
            path = Path.GetFullPath(path);
            if (!PathInternalNetCore.AreRootsEqual(relativeTo, path, comparisonType))
            {
                return path;
            }

            int num = PathInternalNetCore.GetCommonPathLength(relativeTo, path, comparisonType == StringComparison.OrdinalIgnoreCase);
            if (num == 0)
            {
                return path;
            }

            int num2 = relativeTo.Length;
            if (PathInternalNetCore.EndsInDirectorySeparator(relativeTo))
            {
                num2--;
            }

            bool flag = PathInternalNetCore.EndsInDirectorySeparator(path);
            int num3 = path.Length;
            if (flag)
            {
                num3--;
            }

            if (num2 == num3 && num >= num2)
            {
                return ".";
            }

            StringBuilder stringBuilder = new StringBuilder();
            if (num < num2)
            {
                stringBuilder.Append("..");
                for (int i = num + 1; i < num2; i++)
                {
                    if (PathInternalNetCore.IsDirectorySeparator(relativeTo[i]))
                    {
                        stringBuilder.Append(DirectorySeparatorChar);
                        stringBuilder.Append("..");
                    }
                }
            }
            else if (PathInternalNetCore.IsDirectorySeparator(path[num]))
            {
                num++;
            }

            int num4 = num3 - num;
            if (flag)
            {
                num4++;
            }

            if (num4 > 0)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(DirectorySeparatorChar);
                }

                stringBuilder.Append(path, num, num4);
            }

            return stringBuilder.ToString();
        }
    }
}