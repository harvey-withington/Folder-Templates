namespace FolderTemplates.Data
{
    public class ParameterInfo
    {
        public ParameterInfo()
        {

        }

        public ParameterInfo(string? name, string? type, string? prompt, string? placeholder, string? defaultValue)
        {
            Name = name;
            Type = type;
            Prompt = prompt;
            Placeholder = placeholder;
            DefaultValue = defaultValue;
        }

        public string? Name { get; set; }
        public string? Type { get; set; } = "text";
        public string? Prompt { get; set; } = null;
        public string? Placeholder { get; set; } = null;
        public string? DefaultValue { get; set; } = null;
    }
}