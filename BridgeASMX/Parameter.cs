using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BridgeASMX
{
    /// <summary>
    /// 參數
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">
        /// <param name="type">
        public Parameter(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name;
        /// <summary>
        /// Type
        /// </summary>
        public string Type;
    }

}
