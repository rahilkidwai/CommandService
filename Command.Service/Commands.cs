using Command.Business;
using System;

namespace Command.Service
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Commands : CommandExecutionEngine
    {
        #region Fields
        private static volatile Commands _instance;//the variable is declared to be volatile to ensure that assignment to the instance variable completes before the instance variable can be accessed
        private readonly static object _syncRoot = new Object();//for locking purpose
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Commands" /> class.
        /// </summary>
        private Commands()
        {
        }
        #endregion

        #region Properties
        ///<summary>
        /// Gets the instance.
        /// </summary>
        public static Commands Instance
        {
            get
            {
                //double-check locking approach solves the thread concurrency problems while avoiding an exclusive lock in every call
                //to the Instance property method. It also allows you to delay instantiation until the object is first accessed
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Commands();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion
    }
}