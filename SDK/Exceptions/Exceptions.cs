using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teflon.SDK.Exceptions
{
    public class TeflonRuntimeException : Exception
    {
        public TeflonRuntimeException(string msg) : base(msg)
        {

        }
    }
    public class TestFailException : TeflonRuntimeException
    {
        public TestFailException(string msg) : base(msg)
        {
        }
    }
    public class AssertFailExeception:TeflonRuntimeException
    {
        public AssertFailExeception(string msg):base(msg)
        {

        }
    }
    public class TestFormatException : TeflonRuntimeException
    {
        public TestFormatException(string msg) : base(msg)
        {

        }
    }
}
