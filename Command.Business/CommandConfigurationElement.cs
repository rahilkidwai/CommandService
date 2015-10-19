using System.Configuration;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandConfigurationElement : ConfigurationElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandConfigurationElement"/> class.
        /// </summary>
        public CommandConfigurationElement()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [ConfigurationProperty("key", IsKey=true, IsRequired=true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        /// <summary>
        /// Gets or sets the dll path.
        /// </summary>
        [ConfigurationProperty("filePath", IsKey = false, IsRequired = true)]
        public string FilePath
        {
            get { return (string)this["filePath"]; }
            set { this["filePath"] = value; }
        }
        #endregion
    }
}