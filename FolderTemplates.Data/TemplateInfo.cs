using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTemplates.Data
{
    public class TemplateInfo
    {
        public TemplateInfo() { 
        }

        public TemplateInfo(string? name, string? defaultTargetPath)
        {
            Name = name;
            DefaultTargetPath = defaultTargetPath;
        }
        public string? Name { get; set; }
        public string? DefaultTargetPath { get; set; } = null;
    }
}
