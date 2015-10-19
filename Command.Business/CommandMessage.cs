using System;

namespace Command.Business
{
	/// <summary>
	/// The Message class is used for communication between clients and services. Represents the unit of communication between endpoints.
	/// </summary>
    public sealed class CommandMessage<T> where T : ICommandMessagePayload
	{
		#region Fields
		#endregion

		#region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMessage{T}" /> class.
        /// </summary>
        public CommandMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMessage{T}" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="authenticationToken">The authentication token.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <param name="payload">The payload.</param>
        public CommandMessage(string type, string authenticationToken, string messageID, T payload)
        {
            UtcTimestamp = DateTime.UtcNow;
            MessageType = type;
            AuthenticationToken = authenticationToken;
            MessageID = messageID;
            Payload = payload;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMessage{T}" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="authenticationToken">The authentication token.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <param name="relatedMessageID">The related message identifier.</param>
        /// <param name="payload">The payload.</param>
        public CommandMessage(string type, string authenticationToken, string messageID, string relatedMessageID, T payload)
        {
            UtcTimestamp = DateTime.UtcNow;
            MessageType = type;
            AuthenticationToken = authenticationToken;
            MessageID = messageID;
            RelatedMessageID = relatedMessageID;
            Payload = payload;
        }
		#endregion

		#region Properties
        private string _authenticationToken;
        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        public string AuthenticationToken
        {
            get { return _authenticationToken ?? string.Empty; }
            set { _authenticationToken = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _messageID;
        /// <summary>
        /// Gets or sets the message id associated with the message.
        /// </summary>
        public string MessageID
        {
            get { return _messageID ?? string.Empty; }
            set { _messageID = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _relatedMessageID;
        /// <summary>
        /// Gets or sets the related message id associated with the message.
        /// </summary>
        public string RelatedMessageID
        {
            get { return _relatedMessageID ?? string.Empty; }
            set { _relatedMessageID = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        private string _messageType;
        /// <summary>
        /// Gets or sets the message type.
        /// </summary>
        public string MessageType
        {
            get { return _messageType ?? string.Empty; }
            set { _messageType = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        /// <summary>
        /// Gets the UTC timestamp.
        /// </summary>
        public DateTime? UtcTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        public T Payload { get; set; }
        #endregion

		#region Methods                
        /// <summary>
        /// Implicit operators so that you do not need to explicitly get the Payload property,
        /// but can write code to access the value in a more "natural" way: object myPayload = instance.Payload;
        /// </summary>
        public static implicit operator T(CommandMessage<T> value)
        {
            return value.Payload;
        }

        /// <summary>
        /// Implicit operators so that you do not need to explicitly set the Payload property,
        /// but can write code to access the value in a more "natural" way: instance.Payload = myPayload;
        /// </summary>
        public static implicit operator CommandMessage<T>(T value)
        {
            return new CommandMessage<T> { Payload = value };
        }
		#endregion
	}
}
