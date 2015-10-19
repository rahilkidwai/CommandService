using System;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandParameterDocumentation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterDocumentation" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public CommandParameterDocumentation(Type type, string name, string description)
        {
            Type = type;
            Name = (name == null) ? string.Empty : name.Trim();
            Description = (description == null) ? string.Empty : description.Trim();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

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