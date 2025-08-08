using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTemplates.App
{
    public class TemplateParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Prompt { get; set; }
        public string Placeholder { get; set; }
        public string DefaultValue { get; set; }
        public bool ReplaceInFileNames { get; set; }
        public bool ReplaceInFiles { get; set; }

        public TemplateParameter()
        {
            Name = "";
            Type = "text";
            Prompt = "";
            Placeholder = "";
            DefaultValue = "";
            ReplaceInFileNames = false;
            ReplaceInFiles = false;
        }
    }
}
