using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandExecuteResponse : CommandExecuteRequest
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecuteResponse" /> class.
        /// </summary>
        /// <param name="relatedRequest">The related request.</param>
        public CommandExecuteResponse(CommandExecuteRequest relatedRequest)
        {
            if (relatedRequest != null)
            {
                Name = relatedRequest.Name;
                Category = relatedRequest.Category;
                Parameters = relatedRequest.Parameters;
            }
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
        #endregion

        #region Methods
        /// <summary>
        /// Gets the body of the payload as an xml fragment.
        /// </summary>
        /// <returns></returns>
        public override XElement GetPayloadAsXml()
        {
            XElement parameters = new XElement("Parameters");
            foreach (CommandParameter parameter in Parameters)
                parameters.Add(new XElement("Parameter", new XAttribute("Name", parameter.Name), new XAttribute("Value", parameter.Value)));
            XElement response = null;
            if (Response != null)
            {
                string xml = Serialize(Response);
                response = XElement.Parse(xml);
            }
            XElement result = new XElement("CommandExecuteResponse",
                new XElement("Category", Category),
                new XElement("Name", Name),
                parameters,
                new XElement("Success", Success));
            if (response != null) result.Add(response);
            return result;
        }

        private string Serialize(object objectToSerialize)
        {
            if (objectToSerialize == null) return string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(objectToSerialize.GetType());
                serializer.WriteObject(stream, objectToSerialize);
                stream.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }
        #endregion
    }
}