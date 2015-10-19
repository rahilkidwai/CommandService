using System;
using System.Globalization;
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
    public sealed class CommandNotFoundException : Exception
    {
        #region Fields
        private readonly string _category;
        private readonly string _name;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        public CommandNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CommandNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        public CommandNotFoundException(string message, string category, string name)
            : base(message)
        {
            _category = category;
            _name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException" /> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        public CommandNotFoundException(string category, string name)
        {
            _category = category;
            _name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="inner">The inner exception.</param>
        public CommandNotFoundException(string message, string category, string name, Exception inner)
            : base(message, inner)
        {
            _category = category;
            _name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        private CommandNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _category = info.GetString("Category");
            _name = info.GetString("Name");
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
                string message = base.Message;
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = "Command with given category and name not found.";
                }
                if (!string.IsNullOrWhiteSpace(_category))
                {
                    message = string.Format(CultureInfo.InvariantCulture, "{0}{1}Category: {2}", message, Environment.NewLine, _category);
                }
                if (!string.IsNullOrWhiteSpace(_name))
                {
                    message = string.Format(CultureInfo.InvariantCulture, "{0}{1}Name: {2}", message, Environment.NewLine, _name);
                }
                return message;
            }
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
            info.AddValue("Category", _category, typeof(string));
            info.AddValue("Name", _name, typeof(string));
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