using System.ComponentModel.Composition;

namespace Command.Business
{
    /// <summary>
    /// ImportManyAttribute has default CreationPolicy of Any / Shared, whereas 
    /// CommandImportManyAttribute default CreationPolicy is NonShared.
    /// </summary>
    public sealed class CommandImportManyAttribute : ImportManyAttribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandImportManyAttribute"/> class.
        /// </summary>
        public CommandImportManyAttribute()
            : base(typeof(ICommand))
        {
            base.RequiredCreationPolicy = CreationPolicy.NonShared;
            base.AllowRecomposition = true;
        }
        #endregion
    }
}