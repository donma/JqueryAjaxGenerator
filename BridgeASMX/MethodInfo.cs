using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BridgeASMX
{
    public class MethodInfo
    {
        public string MethodName { get; set; }
        public string InputMessageName { get; set; }
        public string OutputMessageName { get; set; }
        public Parameter[] InputParameters { get; set; }
        public Parameter[] OutParameters { get; set; }
        public string Desc { get; set; }
    }

}
