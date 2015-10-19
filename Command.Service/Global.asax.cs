using Command.Business;
using System;
using System.Configuration;
using System.Web.Routing;

namespace Command.Service
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(
                new Route(
                    "execute",
                    new RouteHandler(ApiPage.Execute)));

            RouteTable.Routes.Add(
                new Route(
                    "help/{commandcategory}/{commandname}",
                    new RouteValueDictionary() { { "commandcategory", string.Empty }, { "commandname", string.Empty } },
                    new RouteHandler(ApiPage.Help)));

            RouteTable.Routes.Add(
                new Route(
                    "validate",
                    new RouteHandler(ApiPage.Validate)));

            RouteTable.Routes.Add(
                new Route(
                    "source",
                    new RouteHandler(ApiPage.Source)));

            RouteTable.Routes.Add(
                new Route(
                    "search",
                    new RouteHandler(ApiPage.Search)));

            RouteTable.Routes.Add(
                new Route(
                    "{home}",
                    new RouteValueDictionary() { { "home", string.Empty } },
                    new RouteHandler(ApiPage.Home)));

            LoadCommands();
        }

        private void LoadCommands()
        {
            CommandConfiguration config = CommandConfiguration.GetConfig();
            Commands.Instance.Load(config);
            CommandSettingManager manager = new CommandSettingManager();

            foreach(ConnectionStringSettings connectionStringSettings in ConfigurationManager.ConnectionStrings)
            {
                manager.AddSetting(connectionStringSettings.Name, connectionStringSettings.ConnectionString);
            }
            Commands.Instance.Settings = manager.Settings;
        }
    }
}