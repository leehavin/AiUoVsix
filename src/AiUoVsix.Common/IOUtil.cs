using AiUoVsix.Common.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AiUoVsix.Common
{
    public class IOUtil
    {
        private class FindFileHelper
        {
            public string Result { get; set; }

            public void FindFile(string path, string filename)
            {
                if (Result != null)
                {
                    return;
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo fileInfo = directoryInfo.GetFiles().FirstOrDefault((FileInfo f) => f.Name == filename);
                if (fileInfo != null)
                {
                    Result = fileInfo.FullName;
                    return;
                }

                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                foreach (DirectoryInfo directoryInfo2 in directories)
                {
                    if (Result != null)
                    {
                        break;
                    }

                    FindFile(directoryInfo2.FullName, filename);
                }
            }
        }

        public static byte[] ReadStreamToBytes(Stream stream, bool isClose = false)
        {
            byte[] array = new byte[stream.Length];
            try
            {
                stream.Seek(0L, SeekOrigin.Begin);
                stream.Read(array, 0, (int)stream.Length);
            }
            finally
            {
                if (isClose)
                {
                    stream.Close();
                }
                else
                {
                    stream.Seek(0L, SeekOrigin.Begin);
                }
            }

            return array;
        }

        public static void WriteStreamToFile(Stream stream, string path, Encoding encoding = null)
        {
            byte[] array = new byte[2048];
            string directoryName = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                BinaryReader binaryReader = new BinaryReader(stream, encoding ?? Encoding.UTF8);
                int count;
                while ((count = binaryReader.Read(array, 0, array.Length)) > 0)
                {
                    fileStream.Write(array, 0, count);
                }
            }
        }

        public static Stream ReadFileToStream(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                return new MemoryStream(buffer);
            }
        }

        public static string ReadStreamToString(Stream stream, Encoding encoding = null)
        {
            using (StreamReader streamReader = new StreamReader(stream, encoding ?? Encoding.UTF8))
            {
                stream.Position = 0L;
                return streamReader.ReadToEnd();
            } 
        }

        public static void ReplaceTextFileContent(string file, string oldValue, string newValue)
        {
            string text = File.ReadAllText(file);
            text.Replace(oldValue, newValue);
            File.WriteAllText(file, text);
        }

        public static string GetRelativePath(string fromDir, string absolutePath)
        {
            return NetFxRelativePath.GetRelativePath(fromDir, absolutePath);
        }

        public static void RemoveReadOnlyAttr(string destDirectoryPath)
        {
            Queue<FileSystemInfo> queue = new Queue<FileSystemInfo>(new DirectoryInfo(destDirectoryPath).GetFileSystemInfos());
            while (queue.Count > 0)
            {
                FileSystemInfo fileSystemInfo = queue.Dequeue();
                if (!(fileSystemInfo is FileInfo fileInfo))
                {
                    DirectoryInfo directoryInfo = fileSystemInfo as DirectoryInfo;
                    FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
                    foreach (FileSystemInfo item in fileSystemInfos)
                    {
                        queue.Enqueue(item);
                    }
                }
                else
                {
                    fileInfo.Attributes &= ~FileAttributes.ReadOnly;
                }
            }
        }

        public static bool IsDir(string path)
        {
            return (new FileInfo(path).Attributes & FileAttributes.Directory) != 0;
        }

        public static string GetAbsolutePath(string fromDir, string relativePath)
        {
            string path = Path.Combine(fromDir, relativePath);
            return Path.GetFullPath(path);
        }
          

        public static string FindFile(string path, string filename)
        {
            FindFileHelper findFileHelper = new FindFileHelper();
            findFileHelper.FindFile(path, filename);
            return findFileHelper.Result;
        }

        public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive = true)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(sourceDir);
            if (!directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException("源目录未找到: " + directoryInfo.FullName);
            }

            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            Directory.CreateDirectory(destinationDir);
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in files)
            {
                string destFileName = Path.Combine(destinationDir, fileInfo.Name);
                fileInfo.CopyTo(destFileName);
            }

            if (recursive)
            {
                DirectoryInfo[] array = directories;
                foreach (DirectoryInfo directoryInfo2 in array)
                {
                    string destinationDir2 = Path.Combine(destinationDir, directoryInfo2.Name);
                    CopyDirectory(directoryInfo2.FullName, destinationDir2);
                }
            }
        }
    }
}
