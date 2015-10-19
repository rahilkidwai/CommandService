using System.Collections.Generic;
using System.Xml.Linq;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandExecuteRequest : ICommandMessagePayload
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecuteRequest" /> class.
        /// </summary>
        public CommandExecuteRequest()
        {
        }
        #endregion

        #region Properties
        private string _category;
        /// <summary>
        /// Gets or sets the command category.
        /// </summary>
        public string Category
        {
            get { return _category ?? string.Empty; }
            set { _category = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _name;
        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }
        
        private List<CommandParameter> _parameters;
        /// <summary>
        /// Gets or sets the command parameters.
        /// </summary>
        public List<CommandParameter> Parameters//CommandParameterCollection has problems with json setter
        {
            get { if (_parameters == null) _parameters = new List<CommandParameter>(); return _parameters; }
            set { _parameters = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the body of the payload as an xml fragment.
        /// </summary>
        /// <returns></returns>
        public virtual XElement GetPayloadAsXml()
        {
            XElement parameters = new XElement("Parameters");
            foreach (CommandParameter parameter in Parameters)
                parameters.Add(new XElement("Parameter", new XAttribute("Name", parameter.Name), new XAttribute("Value", parameter.Value)));

            XElement payload = new XElement("CommandExecuteRequest",
                new XElement("Category", Category),
                new XElement("Name", Name),
                parameters);

            return payload;
        }
        #endregion
    }
}