using System;
using System.ComponentModel.Composition;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CommandMetadata : ExportAttribute
    {
        #region Fields
        private string _category;
        private string _name;
        private int _code;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMetadata" /> class.
        /// </summary>
        /// <param name="category">The command category.</param>
        /// <param name="name">The command name.</param>
        public CommandMetadata(string category, string name)
            : base(typeof(ICommand))
        {
            _category = (category == null) ? string.Empty : category.Trim();
            _name = (name == null) ? string.Empty : name.Trim();
            _code = GetCommandCode(_category, _name);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the command category.
        /// </summary>
        public string Category
        {
            get { return _category; }
        }

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the command code.
        /// </summary>
        public int Code
        {
            get { return _code; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the command code.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static int GetCommandCode(string category, string name)
        {
            category = (category == null) ? string.Empty : category.Trim();
            name = (name == null) ? string.Empty : name.Trim();
            return string.Format("{0}.{1}", category, name).ToUpper().GetHashCode();
        }
        #endregion
    }
}