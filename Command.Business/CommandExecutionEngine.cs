using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandExecutionEngine
    {
        #region Fields
        private Assembly[] _assemblies;
        private readonly Dictionary<int, Lazy<ICommand, ICommandProperties>> _commands;
        private List<Lazy<ICommand, ICommandProperties>> _dupes;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutionEngine" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public CommandExecutionEngine(CommandSettingCollection settings = null)
        {
            Settings = settings;
            _commands = new Dictionary<int, Lazy<ICommand, ICommandProperties>>();
        }
        #endregion

        #region Properties 
        /// <summary>
        /// Gets the settings collection.
        /// </summary>
        public CommandSettingCollection Settings { get; set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Loads the commands from the dlls defined in configuration section.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <exception cref="System.ArgumentNullException">assembly</exception>
        public void Load(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                LoadCommands(new Assembly[] { assembly });
            }
            catch(Exception ex)
            {
                StringBuilder sb = new StringBuilder();                
                if(ex is ReflectionTypeLoadException)
                {
                    ReflectionTypeLoadException rtle = ex as ReflectionTypeLoadException;
                    if(rtle.LoaderExceptions != null)
                    {
                        foreach(Exception e in rtle.LoaderExceptions)
                        {
                            sb.Append("***").Append(e.Message).AppendLine();
                            sb.AppendLine(e.StackTrace);
                        }

                    }
                }
                throw new CommandLoadException("Failed to load commands from one or more assembly.", sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Loads the specified assemblies.
        /// </summary>
        /// <param name="assembliesPath">The assemblies path.</param>
        /// <exception cref="System.ArgumentNullException">assemblies</exception>
        /// <exception cref="System.ArgumentException">No assembly exist in the array.</exception>
        public void Load(List<string> assembliesPath)
        {
            if (assembliesPath == null) throw new ArgumentNullException("assembliesPath");
            if (assembliesPath.Count == 0) throw new ArgumentException("No assembly path exist in the list.");
            try
            {
                AssemblyName name;
                Assembly assembly;
                List<Assembly> assemblies = new List<Assembly>();
                foreach (string path in assembliesPath)
                {
                    name = AssemblyName.GetAssemblyName(path);
                    assembly = Assembly.Load(name);
                    assemblies.Add(assembly);
                }
                LoadCommands(assemblies.ToArray());
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                if (ex is ReflectionTypeLoadException)
                {
                    ReflectionTypeLoadException rtle = ex as ReflectionTypeLoadException;
                    if (rtle.LoaderExceptions != null)
                    {
                        foreach (Exception e in rtle.LoaderExceptions)
                        {
                            sb.Append("***").Append(e.Message).AppendLine();
                            sb.AppendLine(e.StackTrace);
                        }

                    }
                }
                throw new CommandLoadException("Failed to load commands from one or more assembly.", sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Loads the commands from the dlls defined in configuration section.
        /// </summary>
        /// <param name="configSection">The configuration section.</param>
        /// <exception cref="System.ArgumentNullException">configSection</exception>
        public void Load(CommandConfiguration configSection)
        {
            if (configSection == null) throw new ArgumentNullException("configSection");
            try
            {
                AssemblyName name;
                Assembly assembly;
                List<Assembly> assemblies = new List<Assembly>();
                foreach (CommandConfigurationElement element in configSection.Sources)
                {
                    name = AssemblyName.GetAssemblyName(element.FilePath);
                    assembly = Assembly.Load(name);
                    assemblies.Add(assembly);
                }
                LoadCommands(assemblies.ToArray());
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                if (ex is ReflectionTypeLoadException)
                {
                    ReflectionTypeLoadException rtle = ex as ReflectionTypeLoadException;
                    if (rtle.LoaderExceptions != null)
                    {
                        foreach (Exception e in rtle.LoaderExceptions)
                        {
                            sb.Append("***").Append(e.Message).AppendLine();
                            sb.AppendLine(e.StackTrace);
                        }

                    }
                }
                throw new CommandLoadException("Failed to load commands from one or more assembly.", sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Invalid command category supplied.
        /// or
        /// Invalid command name supplied.
        /// </exception>
        /// <exception cref="Command.Business.CommandNotFoundException"></exception>
        public object ExecuteCommand(string category, string name, CommandParameterCollection args, ICommandContext context = null)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid command category supplied.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid command name supplied.");
            int code = CommandMetadata.GetCommandCode(category, name);
            if (_commands.ContainsKey(code))
            {
                Lazy<ICommand, ICommandProperties> command = _commands[code];
                if (context == null) context = new CommandContextBase();
                if (args == null) args = new CommandParameterCollection();
                return command.Value.Execute(args, Settings, context);
            }

            throw new CommandNotFoundException(category, name);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="Command.Business.CommandException">Unable to cast the return value to specified type.</exception>
        public T ExecuteCommand<T>(string category, string name, CommandParameterCollection args, ICommandContext context = null) where T : class
        {
            object result = ExecuteCommand(category, name, args, context);

            if (result is T)
            {
                return (T)result;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(result, typeof(T));
                }
                catch (InvalidCastException ice)
                {
                    throw new CommandException("Unable to cast the return value to specified type.", ice);
                }
            }
        }

        /// <summary>
        /// Gets the command documentation.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Invalid command category supplied.
        /// or
        /// Invalid command name supplied.
        /// </exception>
        /// <exception cref="System.Exception"></exception>
        public KeyValuePair<ICommandProperties, CommandDocumentation> GetCommandDocumentation(string category, string name)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid command category supplied.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid command name supplied.");
            int code = CommandMetadata.GetCommandCode(category, name);
            if (_commands.ContainsKey(code))
            {
                Lazy<ICommand, ICommandProperties> command = _commands[code];
                CommandDocumentation documentation = command.Value.Documentation();
                documentation.RelatedType = command.Value.GetType();
                return new KeyValuePair<ICommandProperties, CommandDocumentation>(command.Metadata, documentation);
            }
            throw new Exception(string.Format("Command with given category and name does not exists '{0}.{1}'", category.Trim(), name.Trim()));
        }

        /// <summary>
        /// Gets the commands documentation.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid command category supplied.</exception>
        public IList<KeyValuePair<ICommandProperties, CommandDocumentation>> GetCommandsDocumentation(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid command category supplied.");
            List<KeyValuePair<ICommandProperties, CommandDocumentation>> commands = new List<KeyValuePair<ICommandProperties, CommandDocumentation>>();
            category = category.Trim();
            CommandDocumentation documentation = null;
            foreach (Lazy<ICommand, ICommandProperties> command in _commands.Values)
            {
                if (string.Equals(command.Metadata.Category, category, StringComparison.CurrentCultureIgnoreCase))
                {
                    documentation = command.Value.Documentation();
                    documentation.RelatedType = command.Value.GetType();
                    commands.Add(new KeyValuePair<ICommandProperties, CommandDocumentation>(command.Metadata, documentation));
                }
            }
            return commands;
        }

        /// <summary>
        /// Gets the command categories.
        /// </summary>
        /// <returns></returns>
        public IList<KeyValuePair<string, int>> GetCommandCategories()
        {
            List<KeyValuePair<string, int>> categories = new List<KeyValuePair<string, int>>();                        
            var results = from c in _commands.Values
                          group c
                          by c.Metadata.Category.ToLower()
                              into g
                              select new { Category = g.First().Metadata.Category, Count = g.Count() }; //use first item instead of key to get category name as key is in lower case

            foreach (var group in results)
            {
                categories.Add(new KeyValuePair<string, int>(group.Category, group.Count));
            }
            return categories;
        }

        /// <summary>
        /// Returns true if command with given name can be created.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        /// Returns true if command with given name can be created, false otherwise.
        /// </returns>
        public bool IsCommandAvailable(string category, string name)
        {
            if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(name)) return false;
            int code = CommandMetadata.GetCommandCode(category, name);
            return _commands.ContainsKey(code);            
        }

        /// <summary>
        /// Gets the sources.
        /// </summary>
        /// <returns></returns>
        public Assembly[] GetSources()
        {
            if (_assemblies == null)
            {
                _assemblies = new Assembly[0];
            }
            return _assemblies;
        }

        /// <summary>
        /// Gets the commands unavailable through the execution engine.
        /// </summary>
        public List<Lazy<ICommand, ICommandProperties>> GetUnavailableCommands()
        {
            return _dupes;
        }

        /// <summary>
        /// Unloads this instance.
        /// </summary>
        public void Unload()
        {
            if (_commands != null)
            {
                _commands.Clear();
            }
        }
        
        private void LoadCommands(Assembly[] assemblies)
        {
            _assemblies = assemblies;
            CommandCatalog catalog = new CommandCatalog();
            catalog.Load(assemblies);
            _dupes = new List<Lazy<ICommand, ICommandProperties>>();                    
            foreach (Lazy<ICommand, ICommandProperties> command in catalog.Commands)
            {
                if (_commands.ContainsKey(command.Metadata.Code))
                {
                    _dupes.Add(command);
                }
                else
                {
                    _commands.Add(command.Metadata.Code, command);
                }
            }            
            catalog.Dispose();
        }
        #endregion
    }
}
