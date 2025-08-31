using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTemplates.App
{
    public class TemplateConfig
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DefaultTargetPath { get; set; }
        public List<TemplateParameter>? Parameters { get; set; }

        public TemplateConfig() { }
    }
}
