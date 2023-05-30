using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace FolderTemplates.CommandLine
{

    /// <summary>
    /// Provides a simple strongly typed interface to work with command line parameters.
    /// </summary>
    public class CommandLineProcessor
    {

        // A private dictonary containing the parameters.
        private readonly Dictionary<string, CommandLineParameter> parameters = new();

        /// <summary>
        /// Creats a new empty command line object.
        /// </summary>
        public CommandLineProcessor()
        {
        }

        /// <summary>
        /// Returns a command line parameter by the name.
        /// </summary>
        /// <param name="name">The name of the parameter (the word after the initial hyphen (-).</param>
        /// <returns>A reference to the named comman line object.</returns>
        public CommandLineParameter this[string name]
        {
            get
            {
                if (!parameters.ContainsKey(name))
                    throw new CommandLineException(name, "Not a registered parameter.");
                return parameters[name];
            }
        }

        public string[] Names
        {
            get
            {
                return parameters.Keys.ToArray();
            }
        }

        /// <summary>
        /// Registers a parameter to be used and adds it to the help screen.
        /// </summary>
        /// <param name="p">The parameter to add.</param>
        public void RegisterParameter(CommandLineParameter parameter)
        {
            if (parameters.ContainsKey(parameter.Name))
                throw new CommandLineException(parameter.Name, "Parameter is already registered.");
            parameters.Add(parameter.Name, parameter);
        }

        /// <summary>
        /// Registers parameters to be used and adds hem to the help screen.
        /// </summary>
        /// <param name="p">The parameter to add.</param>
        public void RegisterParameter(CommandLineParameter[] parameters)
        {
            foreach (CommandLineParameter p in parameters)
                RegisterParameter(p);
        }


        public string[] Parse(string[] args, bool allowUnspecified)
        {
            return Parse(args, true, allowUnspecified);
        }

        /// <summary>
        /// Parses the command line and sets the value of each registered parmaters.
        /// </summary>
        /// <param name="args">The arguments array sent to main()</param>
        /// <returns>Any reminding strings after arguments has been processed.</returns>
        public string[] Parse(string[] args, bool clear, bool allowUnspecified)
        {
            if (clear)
                ClearValues();

            int i = 0;

            List<string> new_args = new();

            while (i < args.Length)
            {
                if (args[i].Length > 1 && args[i][0] == '-')
                {
                    // The current string is a parameter name
                    string key = args[i][1..];
                    string? value = null;
                    i++;
                    if (i < args.Length)
                    {
                        if (args[i].Length > 0 && args[i][0] == '-')
                        {
                            // The next string is a new parameter, do nothing
                        }
                        else
                        {
                            // The next string is a value, read the value and move forward
                            value = args[i];
                            i++;
                        }
                    }
                    if (!parameters.ContainsKey(key) && !allowUnspecified)
                        throw new CommandLineException(key, "Parameter is not allowed.");

                    if (parameters.ContainsKey(key))
                    {
                        if (parameters[key].Exists)
                            throw new CommandLineException(key, "Parameter is specified more than once.");

                        parameters[key].SetValue(value);
                    }
                }
                else
                {
                    new_args.Add(args[i]);
                    i++;
                }
            }


            // Check that required parameters are present in the command line. 
            foreach (string key in parameters.Keys)
                if (parameters[key].Required && !parameters[key].Exists)
                    throw new CommandLineException(key, "Required parameter is not found.");

            return new_args.ToArray();
        }

        public void ClearValues()
        {
            foreach (string key in parameters.Keys)
                parameters[key].ClearValue();
        }

        /// <summary>
        /// Generates the help screen.
        /// </summary>
        public string HelpScreen()
        {
            int len = 0;
            foreach (string key in parameters.Keys)
                len = Math.Max(len, key.Length);

            string help = "\nParameters:\n\n";
            foreach (string key in parameters.Keys)
            {

                string s = "-" + parameters[key].Name;
                while (s.Length < len + 3)
                    s += " ";
                s += parameters[key].Help + "\n";
                help += s;
            }
            return help;
        }

    }
}
