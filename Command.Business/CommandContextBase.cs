using System.Runtime.Serialization;
using System.Text;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandContextBase : ICommandContext
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandContextBase"/> class.
        /// </summary>
        public CommandContextBase()
        {
        }
        #endregion

        #region Methods
        private string _securityToken;
        /// <summary>
        /// Gets or sets the security token.
        /// </summary>
        public string SecurityToken
        {
            get { return _securityToken ?? string.Empty; }
            set { _securityToken = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _applicationName;
        /// <summary>
        /// Gets or sets the source application name.
        /// </summary>
        public string ApplicationName
        {
            get { return _applicationName ?? string.Empty; }
            set { _applicationName = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _applicationComponent;
        /// <summary>
        /// Gets or sets the application component.
        /// </summary>
        public string ApplicationComponent
        {
            get { return _applicationComponent ?? string.Empty; }
            set { _applicationComponent = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _localUser;
        /// <summary>
        /// Gets or sets the local account (ex: windows user name).
        /// </summary>
        public string LocalUser
        {
            get { return _localUser ?? string.Empty; }
            set { _localUser = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _machineIP;
        /// <summary>
        /// Gets or sets the machine / station ip address.
        /// </summary>
        public string MachineIP
        {
            get { return _machineIP ?? string.Empty; }
            set { _machineIP = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _machineName;
        /// <summary>
        /// Gets or sets the name of the machine / station.
        /// </summary>
        public string MachineName
        {
            get { return _machineName ?? string.Empty; }
            set { _machineName = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Machine:{0}; IP:{1}; Local User:{2}; App:{3}; Comp:{4}; Sec Token:{5};", MachineName, MachineIP, LocalUser, ApplicationName, ApplicationComponent, SecurityToken);
        }
        #endregion
    }
}