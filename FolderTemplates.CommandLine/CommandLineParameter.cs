namespace FolderTemplates.CommandLine
{
    /// <summary>
    /// Represents a command line parameter. 
    /// Parameters are words in the command line beginning with a hyphen (-).
    /// The value of the parameter is the next word in
    /// </summary>
    public class CommandLineParameter
    {
        private readonly string _name;
        private string? _value = null;
        private readonly bool _required = false;
        private readonly string _helpMessage = "";
        private bool _exists = false;

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="required">Require that the parameter is present in the command line.</param>
        /// <param name="helpMessage">The explanation of the parameter to add to the help screen.</param>
        public CommandLineParameter(string name, bool required, string helpMessage)
        {
            _name = name;
            _required = required;
            _helpMessage = helpMessage;
        }

        /// <summary>
        /// Sets the value of the parameter.
        /// </summary>
        /// <param name="value">A string containing a integer expression.</param>
        public virtual void SetValue(string? value)
        {
            _value = value;
            _exists = true;
        }

        public virtual void ClearValue()
        {
            _value = null;
            _exists = false;
        }

        /// <summary>
        /// Returns the value of the parameter.
        /// </summary>
        public string? Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Returns the help message associated with the parameter.
        /// </summary>
        public string Help
        {
            get { return _helpMessage; }
        }

        /// <summary>
        /// Returns true if the parameter was found in the command line.
        /// </summary>
        public bool Exists
        {
            get { return _exists; }
        }

        /// <summary>
        /// Returns true if the parameter is required in the command line.
        /// </summary>
        public bool Required
        {
            get { return _required; }
        }

        /// <summary>
        /// Returns the name of the parameter.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
    }
}
