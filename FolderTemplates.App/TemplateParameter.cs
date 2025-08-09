using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTemplates.App
{
    public class TemplateParameter
    {
        
        public string? Name { get; set; }
        public string? Match { get; set; }
        public string? Type { get; set; }
        public string? Prompt { get; set; }
        public string? Placeholder { get; set; }
        public string DefaultValue { get; set; }
        public bool ReplaceInFileNames { get; set; }
        public bool ReplaceInFiles { get; set; }

        public TemplateParameter()
        {
            Name = null;
            Match = null;
            Type = null;
            Prompt = null;
            Placeholder = null;
            DefaultValue = "";
            ReplaceInFileNames = true;
            ReplaceInFiles = false;
        }
    }
}
