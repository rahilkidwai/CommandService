
namespace Command.Business.Command
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandSetting
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSetting"/> class.
        /// </summary>
        public CommandSetting()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSetting"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public CommandSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }
        #endregion

        #region Properties
        private string _key;
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key
        {
            get { return _key ?? string.Empty; }
            set { _key = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _value;
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public string Value
        {
            get { return _value ?? string.Empty; }
            set { _value = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }      
        #endregion
    }
}