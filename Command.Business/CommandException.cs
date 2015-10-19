using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CommandException : Exception
    {
        #region Fields
        private readonly ICommand _command;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException"/> class.
        /// </summary>
        public CommandException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CommandException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="command">The command.</param>
        public CommandException(string message, ICommand command)
            : base(message)
        {
            _command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandException(ICommand command, Exception inner)
            : base(string.Empty, inner)
        {
            _command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="command">The command.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandException(string message, ICommand command, Exception inner)
            : base(message, inner)
        {
            _command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected CommandException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _command = (ICommand)info.GetValue("Command", typeof(ICommand));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <value></value>
        /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
        public override string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder(base.Message);
                if (msg.Length > 0) msg.AppendLine();
                else msg.AppendLine("Error occured while executing command.");
                if (InnerException != null) msg.AppendLine(InnerException.Message);
                if(_command != null)
                {
                    MemberInfo info = _command.GetType();
                    msg.AppendLine("Command Details:");
                    foreach (CommandMetadata attrib in info.GetCustomAttributes(typeof(CommandMetadata), false))
                    {
                        msg.Append("Category: ").AppendLine(attrib.Category);
                        msg.Append("Name: ").AppendLine(attrib.Name); 
                    }
                    CommandDocumentation doc = _command.Documentation();
                    if(doc != null)
                    {
                        if(doc.Description.Length > 0)
                            msg.Append("Description: ").AppendLine(doc.Description);
                        if(doc.ParametersDocumentation.Count > 0)
                            msg.AppendLine("Parameters: ");
                        foreach(CommandParameterDocumentation pdoc in doc.ParametersDocumentation)
                        {
                            msg.Append(pdoc.Name).Append(": ").AppendLine(pdoc.Description);
                        }
                    }
                }
                return msg.ToString();
            }
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        public ICommand Command 
        {
            get { return _command; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        ///   </PermissionSet>
        /// <exception cref="System.ArgumentNullException">info</exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            base.GetObjectData(info, context);
            info.AddValue("Command", _command, typeof(ICommand));
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        ///   </PermissionSet>
        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            Type self = this.GetType();
            foreach (PropertyInfo prop in self.GetProperties())
            {
                buf.Append("[").Append(prop.Name).Append("=").Append(prop.GetValue(this, null)).Append("],");
            }
            return buf.ToString();
        }
        #endregion
    }
}