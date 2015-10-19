using System;
using System.Collections.Generic;

namespace Command.Business
{
    /// <summary>
    /// Provides settings for commands
    /// </summary>
    public sealed class CommandSettingManager
    {
        #region Fields
        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the <see cref="CommandSettingManager" /> class.
        /// </summary>
        public CommandSettingManager()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the settings.
        /// </summary>
        public CommandSettingCollection Settings
        {
            get
            {
                return new CommandSettingCollection(_settings);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the connection string to the collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">name
        /// or
        /// connectionString</exception>
        /// <exception cref="System.ArgumentException">Invalid value supplied.;name
        /// or
        /// Invalid value supplied.;connectionString</exception>
        /// <exception cref="System.Exception"></exception>
        public void AddSetting(string key, string value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid value supplied.", "key");

            if (value == null) throw new ArgumentNullException("value");
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Invalid value supplied.", "value");

            key = key.Trim().ToUpper();

            if (_settings.ContainsKey(key)) throw new Exception(string.Format("Key already exists ({0}).", key));

            _settings.Add(key, value);
        }
        #endregion
    }
}