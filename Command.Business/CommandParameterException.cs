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
    public class CommandParameterException : CommandException
    {
        #region Fields
        private readonly CommandParameterCollection _parameters;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterException"/> class.
        /// </summary>
        public CommandParameterException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CommandParameterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandParameterException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="command">The command.</param>
        /// <param name="passedParameters">The passed parameters.</param>
        public CommandParameterException(string message, ICommand command, CommandParameterCollection passedParameters)
            : base(message, command)
        {
            _parameters = passedParameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterException" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="passedParameters">The passed parameters.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandParameterException(ICommand command, CommandParameterCollection passedParameters, Exception inner)
            : base(command, inner)
        {
            _parameters = passedParameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="command">The command.</param>
        /// <param name="passedParameters">The passed parameters.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandParameterException(string message, ICommand command, CommandParameterCollection passedParameters, Exception inner)
            : base(message, command, inner)
        {
            _parameters = passedParameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected CommandParameterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _parameters = (CommandParameterCollection)info.GetValue("Parameters", typeof(CommandParameterCollection));
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
                else msg.AppendLine("Parameters passed to command execute method:");
                if (_parameters != null)
                {
                    foreach (CommandParameter parameter in _parameters)
                    {
                        msg.Append(parameter.Name).Append(": ").AppendLine(parameter.Value.ToString());
                    }
                }
                return msg.ToString();
            }
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public CommandParameterCollection ParametersPassed
        {
            get { return _parameters; }
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
            info.AddValue("ParametersPassed", _parameters, typeof(CommandParameterCollection));
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