
namespace Command.Business
{
    /// <summary>
    /// To access metadata in a strongly-typed fashion create a metadata view by definining an interface with matching read only properties (names and types).
    /// </summary>
    public interface ICommandProperties
    {
        /// <summary>
        /// Gets the command category.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Gets the command name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the command code.
        /// </summary>
        int Code { get; }
    }
}