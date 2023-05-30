namespace FolderTemplates.CommandLine
{
    /// <summary>
    /// Represents an error occuring during command line parsing.
    /// </summary>
    public class CommandLineException : Exception
    {
        public CommandLineException(string parameter, string message)
            :
            base(String.Format("Syntax error of parameter -{0}: {1}", parameter, message))
        {
        }
        public CommandLineException(string message)
            :
            base(message)
        {
        }
    }
}
