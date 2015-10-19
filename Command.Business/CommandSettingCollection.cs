using System.Collections;
using System.Collections.Generic;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandSettingCollection : IEnumerable
    {
        #region Fields
        private readonly IDictionary<string, string> _dictionary;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSettingCollection"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public CommandSettingCollection(IDictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified database name / key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string this[string key]
        {   
            get 
            {
                if (string.IsNullOrWhiteSpace(key)) 
                    return null;
                key = key.Trim().ToUpper();
                return _dictionary[key];
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return _dictionary.Count; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified collection contains specified database name / key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return false;
            key = key.Trim().ToUpper();
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Try to get the setting for given name / key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetValue(string key, out string value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = key.Trim().ToUpper();
            }
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Try to get the setting for given name / key.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool TryGetValue(CommandSettingCollection settings, string key, out string value)
        {
            value = null;
            if (settings == null) return false;
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = key.Trim().ToUpper();
            }
            return settings._dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }
        #endregion
    }
}