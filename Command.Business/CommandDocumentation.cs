using System;
using System.Collections.Generic;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandDocumentation
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDocumentation"/> class.
        /// </summary>
        public CommandDocumentation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDocumentation" /> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="returnTypeDocumentation">The return type documentation.</param>
        /// <param name="parametersDocumentation">The parameters documentation.</param>
        public CommandDocumentation(string description, CommandReturnTypeDocumentation returnTypeDocumentation, List<CommandParameterDocumentation> parametersDocumentation)
        {
            Description = description;
            ReturnTypeDocumentation = returnTypeDocumentation;
            ParametersDocumentation = parametersDocumentation;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of related / associated command.
        /// </summary>
        public Type RelatedType { get; internal set; }

        private List<CommandParameterDocumentation> _parametersDocumentation;
        /// <summary>
        /// Gets or sets the parameters documentation.
        /// </summary>
        public List<CommandParameterDocumentation> ParametersDocumentation
        {
            get { if (_parametersDocumentation == null) _parametersDocumentation = new List<CommandParameterDocumentation>(); return _parametersDocumentation; }
            set { _parametersDocumentation = value; }
        }

        /// <summary>
        /// Gets or sets the return type documentation.
        /// </summary>
        public CommandReturnTypeDocumentation ReturnTypeDocumentation { get; set; }

        private string _description;
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get { return _description ?? string.Empty; }
            set { _description = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }
        #endregion
    }
}