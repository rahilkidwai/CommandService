
namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Method execution logic.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="context">The context.</param>
        /// <param name="?">The ?.</param>
        /// <returns></returns>
        object Execute(CommandParameterCollection args, CommandSettingCollection settings, ICommandContext context);

        /// <summary>
        /// Returns command documentation.
        /// </summary>
        /// <returns></returns>
        CommandDocumentation Documentation();
    }
}