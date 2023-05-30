namespace FolderTemplates.CommandLine
{
    /// <summary>
    /// Represents an string command line parameter.
    /// </summary>
    public class CommandLineString : CommandLineParameter
    {
        public CommandLineString(string name, bool required, string helpMessage)
            : base(name, required, helpMessage)
        {
        }
        public static implicit operator string?(CommandLineString s)
        {
            return s.Value;
        }

    }
}
