using Command.Business;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Web;

namespace Command.Service
{
    /// <summary>
    /// Summary description for HttpHandler
    /// </summary>
    public class HttpHandler : IHttpHandler
    {
        #region Fields
        ApiPage _page; 
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandler"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public HttpHandler(ApiPage page)
        {
            _page = page;
        } 
        #endregion

        #region IHttpHandler
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            switch (_page)
            {
                case ApiPage.Home: context.Server.Transfer("~/default.aspx", false); break;
                case ApiPage.Help: ProcessHelpRequest(context); break;
                case ApiPage.Execute: ProcessExecuteRequest(context); break;
                case ApiPage.Source: context.Server.Transfer("~/pages/source.aspx", false); break;
                case ApiPage.Search: context.Server.Transfer("~/pages/search.aspx", false); break;
                case ApiPage.Validate: context.Server.Transfer("~/pages/validate.aspx", false); break;
                default: context.Server.Transfer("~/default.aspx", false); break;
            }
        } 
        #endregion

        #region Methods
        private void ProcessHelpRequest(HttpContext context)
        {
            string category = context.Request.RequestContext.RouteData.Values["commandcategory"] as string;
            string name = context.Request.RequestContext.RouteData.Values["commandname"] as string;

            if (category.Length > 0 && name.Length > 0)
            {
                context.Items.Add("commandcategory", category);
                context.Items.Add("commandname", name);
                context.Server.Transfer("~/pages/helpcommand.aspx");
            }
            else if (category.Length > 0)
            {
                context.Items.Add("commandcategory", category);
                context.Server.Transfer("~/pages/helpcategory.aspx");
            }
            else
            {
                context.Server.Transfer("~/pages/help.aspx");
            }
        }

        private void ProcessExecuteRequest(HttpContext context)
        {
            //CommandExecuteRequest request = new CommandExecuteRequest() { Category = "Pharmacy", Name = "SearchPharmacies" };
            //request.Parameters.Add(new CommandParameter("MaxRecordsToReturn", "50"));
            //request.Parameters.Add(new CommandParameter("Keywords", "walmart"));
            //request.Parameters.Add(new CommandParameter("Include24Hour", "true"));
            //request.Parameters.Add(new CommandParameter("IncludeEPCS", "true"));
            //request.Parameters.Add(new CommandParameter("IncludeFaxOnly", "true"));
            //request.Parameters.Add(new CommandParameter("IncludeLongTermCare", "true"));
            //request.Parameters.Add(new CommandParameter("IncludeMailOrder", "true"));
            //request.Parameters.Add(new CommandParameter("IncludeRetail", "true"));
            //request.Parameters.Add(new CommandParameter("IncludeSpecialty", "true"));
            //request.Parameters.Add(new CommandParameter("Options", "8"));

            //Message<CommandExecuteRequest> incoming = new CommandMessage<CommandExecuteRequest>("CommandExecuteRequest", Guid.NewGuid().ToString(), request);
            //string xml = incoming.Write();
            //string json = JsonConvert.SerializeObject(incoming);

            //JsonSerializerSettings jss = new JsonSerializerSettings();
            //jss.TypeNameHandling = TypeNameHandling.Objects;
            //DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
            //jss.ContractResolver = dcr;
            //Message<CommandExecuteRequest> jsonIncoming = (Message<CommandExecuteRequest>)JsonConvert.DeserializeObject(json, typeof(Message<CommandExecuteRequest>), jss);

            //using (StringReader sr = new StringReader(xml))
            //{
            //    CommandMessage<CommandExecuteRequest>  xmlIncoming = new CommandMessage<CommandExecuteRequest>();
            //    xmlIncoming.Read(xml);
            //}

            //if (incoming.Payload.Category.Length > 0 || incoming.Payload.Name.Length > 0)
            //{
            //    CommandParameterCollection parameterCollection = new CommandParameterCollection(incoming.Payload.Parameters);
            //    object result = Commands.Instance.ExecuteCommand(incoming.Payload.Category, incoming.Payload.Name, parameterCollection);
            //    CommandExecuteResponse response = new CommandExecuteResponse(incoming.Payload, result);
            //    CommandMessage<CommandExecuteResponse> outgoing = new CommandMessage<CommandExecuteResponse>("CommandExecuteResponse", Guid.NewGuid().ToString(), incoming.TransactionID, response);
            //    context.Response.Write(JsonConvert.SerializeObject(outgoing));
            //}

            if (!string.Equals(context.Request.RequestType, "POST", System.StringComparison.CurrentCultureIgnoreCase))
            {
                //response back with error
            }
            else
            {
                var stream = context.Request.InputStream;
                using (StreamReader sr = new StreamReader(stream))
                {
                    string input = sr.ReadToEnd();
                    JsonSerializerSettings jss = new JsonSerializerSettings();
                    jss.TypeNameHandling = TypeNameHandling.Objects;
                    DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
                    jss.ContractResolver = dcr;
                    CommandMessage<CommandExecuteRequest> incoming = (CommandMessage<CommandExecuteRequest>)JsonConvert.DeserializeObject(input, typeof(CommandMessage<CommandExecuteRequest>), jss);
                    CommandExecuteResponse response = new CommandExecuteResponse(incoming.Payload);
                    try
                    {
                        CommandParameterCollection parameterCollection = new CommandParameterCollection(incoming.Payload.Parameters);
                        object result = Commands.Instance.ExecuteCommand(incoming.Payload.Category, incoming.Payload.Name, parameterCollection);
                        response.Response = result;
                        response.Success = true;
                    }
                    catch (Exception ex)
                    {
                        response.Response = ex;
                        response.Success = false;
                    }
                    finally
                    {
                        CommandMessage<CommandExecuteResponse> outgoing = new CommandMessage<CommandExecuteResponse>("CommandExecuteResponse", Guid.NewGuid().ToString(), incoming.MessageID, response);
                        context.Response.Write(JsonConvert.SerializeObject(outgoing));
                    }
                }
            }
        } 
        #endregion
    }
}