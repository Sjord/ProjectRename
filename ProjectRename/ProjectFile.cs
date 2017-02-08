using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectRename
{
    class ProjectFile
    {
        public string Path { get; private set; }
        public string NewPath { get; private set; }

        private Project project;

        public ProjectFile(Project project, string path)
        {
            this.project = project;
            this.Path = path;
            this.NewPath = CreateNewPath(path);
        }

        private string CreateNewPath(string path)
        {
            return Regex.Replace(path, @"\\kkkkk-[0-9xyp]{4}", "\\" + project.ProjectCode);
        }
    }
}