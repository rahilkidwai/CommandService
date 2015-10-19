using System;
using System.Collections;
using System.Collections.Generic;

namespace Command.Business
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandParameterCollection : IEnumerable
    {
        #region Fields
        private Dictionary<string, object> _parameters;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterCollection"/> class.
        /// </summary>
        public CommandParameterCollection()
        {
            _parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterCollection"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public CommandParameterCollection(IEnumerable<CommandParameter> list)
        {
            _parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (list != null)
                foreach (CommandParameter parameter in list) Add(parameter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterCollection"/> class.
        /// </summary>
        /// <param name="array">The parameter array.</param>
        public CommandParameterCollection(CommandParameter[] array)
        {
            _parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (array != null)
                foreach (CommandParameter parameter in array) Add(parameter);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the parameters count.
        /// </summary>
        public int Count
        {
            get
            {
                return _parameters.Count;
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Object">parameter</see> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(name) || _parameters.Count == 0) return null;
                name = name.Trim();
                object value;
                if (_parameters.TryGetValue(name, out value)) return value;
                return null;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the collection contains parameter with given name.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns></returns>
        public bool ContainsParameter(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || _parameters.Count == 0) return false;
            name = name.Trim();
            return _parameters.ContainsKey(name);
        }

        /// <summary>
        /// Returns true if the collection contains parameter with given name having value of the given type.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public bool ContainsParameter(string name, Type type, bool isNullable)
         {
            if (type == null) throw new ArgumentNullException("type");
            if (string.IsNullOrWhiteSpace(name) || _parameters.Count == 0) return false;
            name = name.Trim();
            if (!_parameters.ContainsKey(name)) return false;
            object obj = _parameters[name];
            if (!isNullable && obj == null) return false;
            if (obj != null) 
            {
                if (type.IsAssignableFrom(obj.GetType())) return true;
                if (type.IsInterface && obj.GetType().GetInterface(type.FullName) != null) return true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds the given parameter in collection.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.ArgumentNullException">parameter</exception>
        /// <exception cref="System.ArgumentException">Invalid parameter name</exception>
        public void Add(CommandParameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException("parameter");
            if (parameter.Name.Length == 0) throw new ArgumentException("Invalid parameter name", "parameter");
            _parameters[parameter.Name] = parameter.Value; // this will overwrite the existing value for given key if any
        }

        /// <summary>
        /// Tries to get parameter from collection. Returns true if parameter is present else false.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public bool TryGetParameter(string name, Type type, bool isNullable, out object value)
        {
            value = null;
            if (type == null) throw new ArgumentNullException("type");
            if (string.IsNullOrWhiteSpace(name) || _parameters.Count == 0) return false;
            name = name.Trim();
            if (!_parameters.ContainsKey(name)) return false;
            object obj = _parameters[name];
            if (!isNullable && obj == null) return false;
            if (obj != null)
            {
                try
                {
                    obj = Convert.ChangeType(obj, type);
                    value = obj;
                    return true;
                }
                catch
                { }

                if (type.IsAssignableFrom(obj.GetType()) || (type.IsInterface && obj.GetType().GetInterface(type.FullName) != null))
                {
                    value = obj;
                    return true;
                }
            }
            else //null value for given parameter name
            {
                value = null;
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Adds the given parameter name / value pair in collection.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        public void Add(string name, object value)
        {
            Add(new CommandParameter(name, value));
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        public IEnumerator GetEnumerator()
        {
            return ToList().GetEnumerator();
        }

        /// <summary>
        /// Returns the list containing all command parameters.
        /// </summary>
        /// <returns></returns>
        public List<CommandParameter> ToList()
        {
            List<CommandParameter> list = new List<CommandParameter>();
            if (_parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in _parameters)
                    list.Add(new CommandParameter(parameter.Key, parameter.Value));
            }
            return list;
        }

        /// <summary>
        /// Returns the array containing all command parameters.
        /// </summary>
        /// <returns></returns>
        public CommandParameter[] ToArray()
        {            
            CommandParameter[] array = new CommandParameter[Count];
            if (_parameters != null)
            {
                int index = 0;
                foreach (KeyValuePair<string, object> parameter in _parameters)
                    array[index++] = new CommandParameter(parameter.Key, parameter.Value);
            }
            return array;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Count.ToString();
        }
        #endregion
    }
}