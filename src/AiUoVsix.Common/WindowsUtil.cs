using System;
using System.Diagnostics;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using AiUoVsix.Common.Windows;
using Microsoft.Win32;

namespace AiUoVsix.Common
{
    public static class WindowsUtil
    {
        public static bool RunCommand(string commandText, out string output)
        {
            bool result = false;
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                var text = string.Empty;
                var text2 = string.Empty;
                using (StreamWriter streamWriter = process.StandardInput)
                {
                    streamWriter.AutoFlush = true;
                    streamWriter.Write(commandText + Environment.NewLine);
                    streamWriter.Write("exit" + Environment.NewLine);
                }
                using (StreamReader streamReader = process.StandardOutput)
                {
                    text = streamReader.ReadToEnd();
                }
                using (StreamReader streamReader2 = process.StandardError)
                {
                    text2 = streamReader2.ReadToEnd();
                }
                 
                if (!process.HasExited)
                {
                    process.Kill();
                    output = text2;
                }
                else
                {
                    result = true;
                    output = text;
                }
            }

            return result;
        }

        public static bool GacInstall(string dll)
        {
            Assembly a = Assembly.LoadFrom(dll);
            Publish val = new Publish();
            if (RuntimeEnvironment.FromGlobalAccessCache(a))
            {
                val.GacRemove(dll);
            }

            val.GacInstall(dll);
            return RuntimeEnvironment.FromGlobalAccessCache(a);
        }

        public static void ClearIETraces(IEMyTraces traces = IEMyTraces.DeleteAll)
        {
            Process process = new Process();
            process.StartInfo.FileName = "RunDll32.exe";
            process.StartInfo.Arguments = $"InetCpl.cpl,ClearMyTracksByProcess {traces}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = false;
            process.Start();
        }

        public static void SetIEProxy(string ip, int port)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", writable: true);
            registryKey.SetValue("ProxyEnable", 1);
            registryKey.SetValue("ProxyServer", $"{ip}:{port}");
            registryKey.Close();
        }

        public static void ClearIEProxy()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", writable: true);
            registryKey.SetValue("ProxyEnable", 0);
            registryKey.Close();
        }
    }
}
