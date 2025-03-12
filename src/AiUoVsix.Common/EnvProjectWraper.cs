using EnvDTE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VSLangProj;

namespace AiUoVsix.Common
{
    public class EnvProjectWraper : Project
    {
        public Project EnvProject { get; }

        public VSProject EnvVSProject => (VSProject)EnvProject;

        public bool IsWebProject
        {
            get
            {
                //IL_001b: Unknown result type (might be due to invalid IL or missing references)
                //IL_0021: Expected O, but got Unknown
                bool result = false;
                foreach (Property property in Properties)
                {
                    Property val = property;
                    if ((val.Name == "WebApplication.AspNetDebugging" && GetPropertyValue(val) == "True") || (val.Name == "WebApplication.IISUrl" && !string.IsNullOrEmpty(GetPropertyValue(val))) || (val.Name == "WebApplication.BrowseURL" && !string.IsNullOrEmpty(GetPropertyValue(val))))
                    {
                        result = true;
                        break;
                    }
                }

                return result;
            }
        }

        public List<ProjectItem> ProjectItems => Descendants(((IEnumerable)EnvProject.ProjectItems).Cast<ProjectItem>(), (ProjectItem pi) => ((IEnumerable)pi.ProjectItems).Cast<ProjectItem>()).ToList();

        public string Name => EnvProject.Name;

        public string FileName => EnvProject.FileName;

        public bool IsDirty => EnvProject.IsDirty;

        public Projects Collection => EnvProject.Collection;

        public DTE DTE => EnvProject.DTE;

        public string Kind => EnvProject.Kind;

        public Properties Properties => EnvProject.Properties;

        public string UniqueName => EnvProject.UniqueName;

        public dynamic Object => EnvProject.Object;

        public dynamic ExtenderNames => EnvProject.ExtenderNames;

        public string ExtenderCATID => EnvProject.ExtenderCATID;

        public string FullName => EnvProject.FullName;

        public bool Saved => EnvProject.Saved;

        public ConfigurationManager ConfigurationManager => EnvProject.ConfigurationManager;

        public Globals Globals => EnvProject.Globals;

        public ProjectItem ParentProjectItem => EnvProject.ParentProjectItem;

        public CodeModel CodeModel => EnvProject.CodeModel;

        string Project.Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        bool Project.IsDirty
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        ProjectItems Project.ProjectItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object Extender
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool Project.Saved
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public EnvProjectWraper(Project project)
        {
            EnvProject = project;
        }

        public void SetVariable(string name, string value)
        {
            EnvDTEWraper.SetVariable(EnvProject.Globals, name, value);
        }

        public string GetVariable(string name)
        {
            return EnvDTEWraper.GetVariable(EnvProject.Globals, name);
        }

        public void AddReference(string bstrPath)
        {
            EnvVSProject.References.Add(bstrPath);
        }

        public void AddFromFile(string fileName)
        {
            EnvProject.ProjectItems.AddFromFile(fileName);
            EnvProject.Save("");
        }

        public void AddFromFiles(IEnumerable<string> files)
        {
            foreach (string file in files)
            {
                AddFromFile(file);
            }

            EnvProject.Save("");
        }

        public void AddFromDirectory(string dir)
        {
            EnvProject.ProjectItems.AddFromDirectory(dir);
        }

        private string GetPropertyValue(Property prop)
        {
            string result = null;
            try
            {
                result = Convert.ToString(prop.Value);
            }
            catch
            {
            }

            return result;
        }

        private static IEnumerable<T> Descendants<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> descendBy)
        {
            foreach (T value in source)
            {
                yield return value;
                foreach (T item in Descendants(descendBy(value), descendBy))
                {
                    yield return item;
                }
            }
        }

        public void SaveAs(string NewFileName)
        {
            EnvProject.SaveAs(NewFileName);
        }

        public void Save(string FileName = "")
        {
            EnvProject.Save(FileName);
        }

        public void Delete()
        {
            EnvProject.Delete();
        }

        public static string GetFullPath(ProjectItem item)
        {
            return (string)item.Properties.Item((object)"FullPath").Value;
        }

        public dynamic get_Extender(string ExtenderName)
        {
            throw new NotImplementedException();
        }
    }
}
