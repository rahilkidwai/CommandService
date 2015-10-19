using System;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandReturnTypeDocumentation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandReturnTypeDocumentation" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        public CommandReturnTypeDocumentation(Type type, string description)
        {
            Type = type;
            Description = (description == null) ? string.Empty : description.Trim();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }
        #endregion
    }
}