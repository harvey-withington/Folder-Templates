namespace FolderTemplates.CommandLine
{
    /// <summary>
    /// Represents an integer command line parameter. 
    /// </summary>
    public class CommandLineInt : CommandLineParameter
    {
        private readonly int _min = int.MinValue;
        private readonly int _max = int.MaxValue;
        private int _value;

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="required">Require that the parameter is present in the command line.</param>
        /// <param name="helpMessage">The explanation of the parameter to add to the help screen.</param>
        public CommandLineInt(string name, bool required, string helpMessage)
            : base(name, required, helpMessage)
        {
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="required">Require that the parameter is present in the command line.</param>
        /// <param name="helpMessage">The explanation of the parameter to add to the help screen.</param>
        /// <param name="min">The minimum value of the parameter.</param>
        /// <param name="max">The maximum valie of the parameter.</param>
        public CommandLineInt(string name, bool required, string helpMessage, int min, int max)
            : base(name, required, helpMessage)
        {
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Sets the value of the parameter.
        /// </summary>
        /// <param name="value">A string containing a integer expression.</param>
        public override void SetValue(string? value)
        {
            base.SetValue(value);
            int i;
            try
            {
                i = Convert.ToInt32(value);
            }
            catch (Exception)
            {
                throw new CommandLineException(base.Name, "Value is not an integer.");
            }
            if (i < _min) throw new CommandLineException(base.Name, String.Format("Value must be greather or equal to {0}.", _min));
            if (i > _max) throw new CommandLineException(base.Name, String.Format("Value must be less or equal to {0}.", _max));
            _value = i;
        }

        /// <summary>
        /// Returns the current value of the parameter.
        /// </summary>
        new public int Value
        {
            get { return _value; }
        }

        /// <summary>
        /// A implicit converion to a int data type.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator int(CommandLineInt s)
        {
            return s.Value;
        }
    }
}
