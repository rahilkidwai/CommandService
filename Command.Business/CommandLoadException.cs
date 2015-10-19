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
    public class CommandLoadException : CommandException
    {
        #region Fields
        private readonly string _loaderExceptions;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoadException"/> class.
        /// </summary>
        public CommandLoadException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoadException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CommandLoadException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoadException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandLoadException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoadException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="loaderExceptions">The loader exceptions.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandLoadException(string message, string loaderExceptions, Exception inner)
            : base(message, inner)
        {
            _loaderExceptions = loaderExceptions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoadException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
         protected CommandLoadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _loaderExceptions = (string)info.GetValue("LoaderExceptions", typeof(string));
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
                else msg.AppendLine("Loader Exception Details:");
                if (_loaderExceptions != null)
                {
                    msg.Append(_loaderExceptions);
                }
                return msg.ToString();
            }
        }

        /// <summary>
        /// Gets the loader exceptions.
        /// </summary>
        public string LoaderExceptions
        {
            get { return _loaderExceptions; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">info</exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            base.GetObjectData(info, context);
            info.AddValue("LoaderExceptions", _loaderExceptions, typeof(string));
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