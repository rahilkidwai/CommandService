using System.Web;
using System.Web.Routing;

namespace Command.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class RouteHandler : IRouteHandler
    {
        #region Fields
        ApiPage _page; 
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteHandler"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public RouteHandler(ApiPage page)
        {
            _page = page;
        } 
        #endregion

        #region IRouteHandler
        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        /// <returns>
        /// An object that processes the request.
        /// </returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new HttpHandler(_page);
        } 
        #endregion
    }
}