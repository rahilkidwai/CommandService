using System;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandParameter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameter"/> class.
        /// </summary>
        public CommandParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameter" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentException">Invalid parameter name</exception>
        public CommandParameter(string name, object value)
        {
            if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid parameter name");            
            Name = name.Trim().ToUpper();
            Value = value;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public object Value { get; set; }
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
            return Name;
        } 
        #endregion
    }
}