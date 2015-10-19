using System.Configuration;

namespace Command.Business
{
    /*
    <configuration>
        <configSections>
		    <section name="commandConfiguration" type="Command.Command.BusinessConfiguration, Command.Business" />
	    </configSections>
	    <commandConfiguration>
            <sources>
                <add key="Assembly1" filePath="C:\MyProduct\Assembly1.dll" />
                <add key="Assembly2" filePath="C:\MyProduct\Assembly2.dll" />
            </sources>
	    </commandConfiguration>
    </configuration>
    */

    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets the sources.
        /// </summary>
        [ConfigurationProperty("sources")]
        public CommandConfigurationElementCollection Sources
        {
            get
            {
                return (CommandConfigurationElementCollection)this["sources"] ?? new CommandConfigurationElementCollection();
            }
        }
        
        /// <summary>
        /// Returns an CommandConfiguration instance
        /// </summary>
        public static CommandConfiguration GetConfig()
        {
            return (CommandConfiguration)System.Configuration.ConfigurationManager.GetSection("commandConfiguration") ?? new CommandConfiguration();
        }
    }
}