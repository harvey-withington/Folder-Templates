namespace FolderTemplates.CommandLine
{
    /// <summary>
    /// Represents a CmdLine object to use with console applications.
    /// The -help parameter will be registered automatically.
    /// Any errors will be written to the console instead of generating exceptions.
    /// </summary>
    public class ConsoleCommandLine : CommandLineProcessor
    {
        public ConsoleCommandLine()
        {
            base.RegisterParameter(new CommandLineString("help", false, "Prints the help screen."));
        }

        public new string[]? Parse(string[] args, bool allowUnspecified)
        {
            string[]? ret = null;
            string error = "";
            try
            {
                ret = base.Parse(args, allowUnspecified);
            }
            catch (CommandLineException ex)
            {
                error = ex.Message;
            }

            if (this["help"].Exists)
            {
                Console.WriteLine(base.HelpScreen());
                System.Environment.Exit(0);
            }

            if (error != "")
            {
                Console.WriteLine(error);
                Console.WriteLine("Use -help for more information.");
                System.Environment.Exit(1);
            }

            return ret;
        }
    }
}
