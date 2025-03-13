using AiUoVsix.Command.NugetPublish.Enums;
using AiUoVsix.Command.NugetPublish.Models;
using AiUoVsix.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiUoVsix.Command.NugetPublish
{

    public class ProjectInfoParser
    {
        public ProjectInfo ParseInfo(string project, string basePath)
        {
            ProjectInfo info = new ProjectInfo();
            info.ProjectName = Path.GetFileNameWithoutExtension(project);
            info.ProjectPath = Path.Combine(basePath, project);
            if (File.Exists(project))
            {
                string str;
                info.ProjectType = this.GetProjectType(project, out str);
                info.ProjectTypeString = str;
                info.PublishType = this.GetPublishType(project);
                info.Version = this.GetVersion(project, info.ProjectType, info.PublishType);
                info.Status = true;
            }
            else
            {
                info.PublishType = PublishType.Unknow;
                info.ProjectTypeString = "未知";
                info.ProjectType = ProjectType.Unknow;
                info.Status = false;
            }
            return info;
        }

        private ProjectType GetProjectType(string project, out string str)
        {
            ProjectType projectType = ProjectType.Framework;
            string singleNodeInnerText1 = this.GetSingleNodeInnerText(project, "/Project/PropertyGroup/TargetFramework");
            if (string.IsNullOrEmpty(singleNodeInnerText1))
            {
                singleNodeInnerText1 = this.GetSingleNodeInnerText(project, "/Project/PropertyGroup/TargetFrameworks");
                if (!string.IsNullOrEmpty(singleNodeInnerText1))
                    singleNodeInnerText1 = singleNodeInnerText1.Split(';')[0];
            }
            if (string.IsNullOrEmpty(singleNodeInnerText1))
            {
                string singleNodeInnerText2 = this.GetSingleNodeInnerText(project, "/Project/PropertyGroup/TargetFrameworkVersion");
                str = !string.IsNullOrEmpty(singleNodeInnerText2) ? "framwork" + singleNodeInnerText2 : "未知";
            }
            else
                str = singleNodeInnerText1;
            if (!string.IsNullOrEmpty(singleNodeInnerText1))
            {
                if (singleNodeInnerText1.StartsWith("netstandard"))
                    projectType = ProjectType.Standard;
                else if (singleNodeInnerText1.StartsWith("netcoreapp"))
                {
                    projectType = ProjectType.Core;
                }
                else
                {
                    if (!singleNodeInnerText1.StartsWith("net"))
                        throw new Exception("未知");
                    projectType = ProjectType.Net;
                }
            }
            return projectType;
        }

        private ProjectVersion GetVersion(
          string project,
          ProjectType projType,
          PublishType publishType)
        {
            string str = (string)null;
            switch (projType)
            {
                case ProjectType.Standard:
                case ProjectType.Core:
                case ProjectType.Net:
                    str = this.GetSingleNodeInnerText(project, "/Project/PropertyGroup/Version");
                    if (string.IsNullOrEmpty(str))
                    {
                        str = "1.0.0";
                        XmlWrapper xmlWrapper = new XmlWrapper(project);
                        xmlWrapper.AppendChildNode("/Project/PropertyGroup", "Version", str);
                        xmlWrapper.Save();
                        break;
                    }
                    break;
                case ProjectType.Framework:
                    foreach (string readAllLine in File.ReadAllLines(Path.Combine(Path.GetDirectoryName(project), "Properties\\AssemblyInfo.cs")))
                    {
                        if (readAllLine.StartsWith("[assembly: AssemblyVersion("))
                            str = readAllLine.Trim("[assembly: AssemblyVersion(\"", "\")]");
                    }
                    break;
            }
            if (publishType == PublishType.Vsix)
                str = new XmlWrapper(Path.Combine(Path.GetDirectoryName(project), "source.extension.vsixmanifest")).GetAttributeValue("/PackageManifest/Metadata/Identity", "Version");
            return !string.IsNullOrEmpty(str) ? new ProjectVersion(str) : throw new Exception("项目版本读取失败！/r/n" + project);
        }

        private PublishType GetPublishType(string project)
        {
            return string.IsNullOrEmpty(this.GetSingleNodeInnerText(project, "/Project/PropertyGroup/VSToolsPath")) ? PublishType.Nuget : PublishType.Vsix;
        }

        private string GetSingleNodeInnerText(string file, string path)
        {
            return File.Exists(file) ? new XmlWrapper(file).GetInnerText(path) : throw new FileNotFoundException("文件不存在: " + file);
        }

        public void UpdateVersion(ProjectInfo project, int versionIdx, string beta)
        {
            int major = project.Version.Major;
            int minor = project.Version.Minor;
            int build = project.Version.Build;
            string suffix = (string)null;
            switch (versionIdx)
            {
                case 0:
                    return;
                case 1:
                    ++build;
                    break;
                case 2:
                    ++minor;
                    build = 0;
                    break;
                case 3:
                    ++major;
                    minor = 0;
                    build = 0;
                    break;
                case 4:
                    ++build;
                    suffix = "beta." + beta;
                    break;
            }
            project.Version = new ProjectVersion(major, minor, build, suffix);
            switch (project.ProjectType)
            {
                case ProjectType.Standard:
                case ProjectType.Core:
                case ProjectType.Net:
                    XmlWrapper xmlWrapper1 = new XmlWrapper(project.ProjectPath);
                    xmlWrapper1.SetInnerText("/Project/PropertyGroup/Version", project.Version.ToString() ?? "");
                    xmlWrapper1.Save();
                    break;
                case ProjectType.Framework:
                    string path = Path.Combine(Path.GetDirectoryName(project.ProjectPath), "Properties\\AssemblyInfo.cs");
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string readAllLine in File.ReadAllLines(path))
                    {
                        if (readAllLine.StartsWith("[assembly: AssemblyVersion("))
                            stringBuilder.AppendLine(string.Format("[assembly: AssemblyVersion(\"{0}\")]", (object)project.Version));
                        else if (readAllLine.StartsWith("[assembly: AssemblyFileVersion("))
                            stringBuilder.AppendLine(string.Format("[assembly: AssemblyFileVersion(\"{0}\")]", (object)project.Version));
                        else
                            stringBuilder.AppendLine(readAllLine);
                    }
                    File.WriteAllText(path, stringBuilder.ToString());
                    break;
            }
            if (project.PublishType != PublishType.Vsix)
                return;
            XmlWrapper xmlWrapper2 = new XmlWrapper(Path.Combine(Path.GetDirectoryName(project.ProjectPath), "source.extension.vsixmanifest"));
            xmlWrapper2.SetAttribute("/PackageManifest/Metadata/Identity", "Version", (object)string.Format("{0}.{1}.{2}", (object)major, (object)minor, (object)build));
            xmlWrapper2.Save();
        }
    }
}
