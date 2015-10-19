using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Command.Business
{
    /// <summary>
    /// Provides command cataloging services.
    /// </summary>
    public class CommandCatalog : IDisposable
    {
        #region Fields
        private List<Assembly> _sources;
        private AggregateCatalog _aggregateCatalog;

        // [ImportMany(typeof(ICommand), AllowRecomposition=true, RequiredCreationPolicy=CreationPolicy.Shared)]
        [CommandImportManyAttribute]
        internal IEnumerable<Lazy<ICommand, ICommandProperties>> Commands = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCatalog"/> class.
        /// </summary>
        public CommandCatalog()
        {
            _sources = new List<Assembly>();
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the sources.
        /// </summary>
        public Assembly[] Sources { get { return _sources.ToArray(); } }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the parts from specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public void Load(Assembly[] assemblies)
        {
            if (_aggregateCatalog != null) _aggregateCatalog.Dispose();
            _sources.Clear();
            //Adds all the parts found in the same assembly as this class
            _aggregateCatalog = new AggregateCatalog();
            foreach (Assembly assembly in assemblies)
            {
                _aggregateCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
                _sources.Add(assembly);
            }
            //Create the CompositionContainer with the parts in the catalog
            CompositionContainer container = new CompositionContainer(_aggregateCatalog);
            container.ComposeParts(this);
        }

        /// <summary>
        /// Loads the parts from assemblies of the specified types.
        /// </summary>
        /// <param name="types">The types.</param>
        public void Load(Type[] types)
        {
            List<Assembly> assembiles = new List<Assembly>();            
            foreach (Type type in types)
            {
                if (!assembiles.Contains(type.Assembly))
                    assembiles.Add(type.Assembly);
            }
            Load(assembiles.ToArray());
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_aggregateCatalog != null) _aggregateCatalog.Dispose();
            _sources.Clear();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Validates the catalog.
        /// </summary>
        /// <param name="throwExceptionIfInvalid">if set to <c>true</c> [throw exception if invalid].</param>
        /// <returns></returns>
        public bool IsCatalogValid(bool throwExceptionIfInvalid)
        {
            StringBuilder sb = throwExceptionIfInvalid ? new StringBuilder() : null;

            //find commands with no name
            foreach (Lazy<ICommand, ICommandProperties> command in Commands)
            {
                if (command.Metadata.Category.Length == 0 || command.Metadata.Name.Length == 0)
                {
                    if (throwExceptionIfInvalid)
                    {
                        sb.AppendLine("Error: Undefined / invalid command category and/or name");
                        sb.Append("Code: ").AppendLine(command.Metadata.Code.ToString());
                        sb.Append("Type: ").AppendLine(command.Value.GetType().FullName).AppendLine();
                    }
                    else 
                        return false; //command with invalid name
                }
            }

            //find duplicates - multiple commands with same code
            var duplicates = from c in Commands
                        group c by c.Metadata.Code
                        into g
                        select new { Code = g.Key, Commands = g.ToList() };

            if (duplicates.Count() > 0)
            { 
                if (throwExceptionIfInvalid)
                {
                    sb.AppendLine("Error: Multiple commands with given category and name exists:");
                    foreach (var duplicate in duplicates)
                    {                        
                        sb.Append("******Code: ").Append(duplicate.Code.ToString()).AppendLine("******");
                        foreach(var entry in duplicate.Commands)
                        {
                            sb.Append("Category: ").AppendLine(entry.Metadata.Category);
                            sb.Append("Name: ").AppendLine(entry.Metadata.Name);
                            sb.Append("Type: ").AppendLine(entry.Value.GetType().FullName);
                        }
                    }
                }
                else return false; //duplicate commands
            }            

            if (throwExceptionIfInvalid && sb != null && sb.Length > 0)
            {
                sb.Insert(0, Environment.NewLine);
                sb.Insert(0, "Command(s) having inconsistent / erroneous metadata:");
                throw new Exception(sb.ToString());
            }

            return true;
        }
        #endregion
    }
}