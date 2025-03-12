using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;

namespace AiUoVsix.Common
{
    public class EnvDTEWraper
    {
        public DTE2 DTE { get; }

        public Solution Solution => DTE.Solution;

        public string SolutionFile => ((_Solution)Solution).FileName;

        public string SolutionFileName => Path.GetFileName(((_Solution)Solution).FileName);

        public string SolutionName => ((_Solution)Solution).Properties.Item((object)"Name").Value.ToString();

        public EnvProjectWraper ActiveProject
        {
            get
            {
                Project val = null;
                dynamic activeSolutionProjects = DTE.ActiveSolutionProjects;
                if (activeSolutionProjects.Length == 1)
                {
                    dynamic value = activeSolutionProjects.GetValue(0);
                    val = (Project)(object)((value is Project) ? value : null);
                }

                return (val != null) ? new EnvProjectWraper(val) : null;
            }
        }

        public List<EnvProjectWraper> Projects
        {
            get
            {
                //IL_0021: Unknown result type (might be due to invalid IL or missing references)
                //IL_0027: Expected O, but got Unknown
                List<EnvProjectWraper> list = new List<EnvProjectWraper>();
                foreach (Project project in ((_Solution)Solution).Projects)
                {
                    Project val = project;
                    if (val != null)
                    {
                        if (val.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                        {
                            list.AddRange(GetSolutionFolderProjects(val));
                        }
                        else
                        {
                            list.Add(new EnvProjectWraper(val));
                        }
                    }
                }

                return list;
            }
        }

        public EnvDTEWraper(DTE dte)
        {
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Expected O, but got Unknown
            DTE = (DTE2)dte;
        }

        public static IEnumerable<DTE2> GetAllDTE()
        {
            //IL_006c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0076: Expected O, but got Unknown
            List<DTE2> list = new List<DTE2>();
            GetRunningObjectTable(0, out var prot);
            prot.EnumRunning(out var ppenumMoniker);
            ppenumMoniker.Reset();
            IntPtr zero = IntPtr.Zero;
            IMoniker[] array = new IMoniker[1];
            while (ppenumMoniker.Next(1, array, zero) == 0)
            {
                CreateBindCtx(0, out var ppbc);
                array[0].GetDisplayName(ppbc, null, out var ppszDisplayName);
                if (ppszDisplayName.StartsWith("!VisualStudio.DTE."))
                {
                    prot.GetObject(array[0], out var ppunkObject);
                    list.Add((DTE2)ppunkObject);
                }
            }

            return list;
        }

        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);

        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        public EnvProjectWraper GetProject(string projectName)
        {
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_0028: Expected O, but got Unknown
            Project project = null;
            foreach (Project project2 in ((_Solution)DTE.Solution).Projects)
            {
                Project val = project2;
                if (val.Name == projectName)
                {
                    project = val;
                    break;
                }
            }

            return new EnvProjectWraper(project);
        }

        private IEnumerable<EnvProjectWraper> GetSolutionFolderProjects(Project solutionFolder)
        {
            List<EnvProjectWraper> list = new List<EnvProjectWraper>();
            for (int i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                Project subProject = solutionFolder.ProjectItems.Item((object)i).SubProject;
                if (subProject != null)
                {
                    if (subProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                    {
                        list.AddRange(GetSolutionFolderProjects(subProject));
                    }
                    else
                    {
                        list.Add(new EnvProjectWraper(subProject));
                    }
                }
            }

            return list;
        }

        public void SetVariable(string name, string value)
        {
            SetVariable(((_Solution)Solution).Globals, name, value);
        }

        public string GetVariable(string name)
        {
            return GetVariable(((_Solution)Solution).Globals, name);
        }

        internal static void SetVariable(Globals globals, string name, string value)
        {
            if ((bool)globals[name])
            {
                globals[name] = value;
                return;
            }

            globals[name] = value;
            globals[name] = true;
        }

        internal static string GetVariable(Globals globals, string name)
        {
            return (bool)globals[name] ? Convert.ToString(globals[name]) : null;
        }
    }
}
