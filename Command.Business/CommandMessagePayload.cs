
namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandMessagePayload : ICommandMessagePayload
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMessagePayload" /> class.
        /// </summary>
        public CommandMessagePayload()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether [success].
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets the response result.
        /// </summary>
        public object Response { get; set; }

        private string _error;
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public string Error
        {
            get { return _error ?? string.Empty; }
            set { _error = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }
        #endregion

        #region Methods
        #endregion
    }
}
