using Command.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.TestCommands
{
    [CommandMetadata("Test", "HelloWorld")]
    public sealed class HelloWorld : ICommand
    {
        #region Methods
        /// <summary>
        /// The HelloWorld Command
        /// </summary>
        /// <returns></returns>
        public CommandDocumentation Documentation()
        {
            string description = "HelloWorld command. Returns the message duplicated based on repeat count.";
            List<CommandParameterDocumentation> parameters = new List<CommandParameterDocumentation>()
            {
                new CommandParameterDocumentation(typeof(String), "Message", "The message."),
                new CommandParameterDocumentation(typeof(int), "RepeatCount", "Number of time to repeat the message (must be >= 0)."),
            };
            CommandReturnTypeDocumentation returns = new CommandReturnTypeDocumentation(typeof(String), "Returns the message repeated as per repeat count.");
            return new CommandDocumentation(description, returns, parameters);
        }

        /// <summary>
        /// Method execution logic.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public object Execute(CommandParameterCollection args, CommandSettingCollection settings, ICommandContext context)
        {
            return new CommandProcessor().Execute(this, args, settings, context);
        }
        #endregion

        #region CommandProcessor
        sealed class CommandProcessor
        {
            private string _message;
            private int _repeatCount;

            internal string Execute(ICommand command, CommandParameterCollection args, CommandSettingCollection settings, ICommandContext context)
            {
                if (!ParseArgs(args))
                    throw new CommandParameterException("Failed to parse the arguments.", command, args);
                try
                {
                    return ExecuteCommand(settings, context);
                }
                catch (Exception e)
                {
                    throw new CommandException(command, e);
                }
            }

            private bool ParseArgs(CommandParameterCollection args)
            {
                object arg;

                if (args == null || args.Count != 2) return false;

                if (!args.TryGetParameter("Message", typeof(string), true, out arg)) return false;
                _message = Convert.ToString(arg);

                if (!args.TryGetParameter("RepeatCount", typeof(int), false, out arg)) return false;
                _repeatCount = Convert.ToInt32(arg);
                
                return true;
            }

            private string ExecuteCommand(CommandSettingCollection settings, ICommandContext context)
            {
                //Get value from settings collection as needed
                //string connectionString = null;
                //if (!CommandSettingCollection.TryGetValue(settings, "DbConn", out connectionString))
                //    throw new Exception("Unable to get the connection string to the database.");

                StringBuilder sb = new StringBuilder("Hello World!!!");

                if (string.IsNullOrEmpty(_message)) 
                    _repeatCount = 0;

                while (_repeatCount > 0)
                {
                    sb.AppendLine().Append(_message);
                    _repeatCount--;
                }

                return sb.ToString();
            }
        }
        #endregion
    }
}
