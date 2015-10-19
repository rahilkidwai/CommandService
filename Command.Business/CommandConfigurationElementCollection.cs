using System.Configuration;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandConfigurationElementCollection : ConfigurationElementCollection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandConfigurationElementCollection"/> class.
        /// </summary>
        public CommandConfigurationElementCollection()
        {
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="CommandConfigurationElement"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public CommandConfigurationElement this[int index]
        {
            get
            {
                return (CommandConfigurationElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CommandConfigurationElement();
        }

        /// <summary>
        /// Creates a new <see cref="T:System.Configuration.ConfigurationElement" /> when overridden in a derived class.
        /// </summary>
        /// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to create.</param>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new CommandConfigurationElement() { Key = elementName };
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CommandConfigurationElement)element).Key;
        }
        #endregion
    }
}