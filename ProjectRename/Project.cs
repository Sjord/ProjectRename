using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectRename
{
    class Project
    {
        public string ProjectPath { get; private set; }
        public string ProjectCode { get; private set; }
        public ISet<ProjectFile> UnnamedFiles { get; private set; }

        public Project(string path)
        {
            this.ProjectPath = path;
            this.ProjectCode = this.FindProjectCode(path);
            this.UnnamedFiles = this.FindUnnamedFiles();
        }

        private string FindProjectCode(string path)
        {
            string number = null;

            var folders = path.Split(Path.DirectorySeparatorChar);
            for (var i = folders.Length - 1; i >= 0; i--)
            {
                var match = Regex.Match(folders[i], @"^([0-9]{4})");
                if (match.Success)
                {
                    number = match.Groups[1].ToString();
                }

                match = Regex.Match(folders[i], @"^([A-Z]{5})");
                if (number != null && match.Success)
                {
                    var identifier = match.Groups[1].ToString();
                    return string.Format("{0}-{1}", identifier, number);
                }
            }

            throw new Exception("Could not determine project code");
        }

        public void RenameUnnamedFiles()
        {
            foreach (var file in this.UnnamedFiles)
            {
                File.Move(file.Path, file.NewPath);
            }
        }

        public ISet<ProjectFile> FindUnnamedFiles()
        {
            return FindUnnamedFiles(this.ProjectPath);
        }

        private ISet<ProjectFile> FindUnnamedFiles(string path)
        {
            var results = new HashSet<ProjectFile>();
            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetFileName(file).StartsWith("kkkkk"))
                {
                    results.Add(new ProjectFile(this, file));
                }
            }
            foreach (var dir in Directory.GetDirectories(path))
            {
                results.UnionWith(FindUnnamedFiles(dir));
            }
            return results;
        }
    }
}
