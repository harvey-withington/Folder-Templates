namespace FolderTemplates.CommandLine
{
    /// <summary>
    /// Represents a CmdLine object to use with console applications.
    /// The -help parameter will be registered automatically.
    /// Any errors will be written to the console instead of generating exceptions.
    /// </summary>
    public class ConsoleCommandLine : CommandLineProcessor
    {
        private bool parsedSuccessfully = false;
        public bool ParsedSuccessfully { get => parsedSuccessfully; set => parsedSuccessfully = value; }
        public ConsoleCommandLine()
        {
            base.RegisterParameter(new CommandLineString("help", false, "Prints the help screen."));
        }

        public new string[]? Parse(string[] args, bool allowUnspecified, string? defaultUnspecifiedFlag = null)
        {
            string[]? ret = null;
            string error = "";
            try
            {
                ret = base.Parse(args, allowUnspecified, defaultUnspecifiedFlag);
            }
            catch (CommandLineException ex)
            {
                error = ex.Message;
            }

            if (this["help"].Exists)
            {
                Console.WriteLine(base.HelpScreen());
            }

            if (error != "")
            {
                Console.WriteLine(error);
                Console.WriteLine("Use -help for more information.");
                parsedSuccessfully = false;
            } else
            {
                parsedSuccessfully = true;
            }

            return ret;
        }
    }
}
