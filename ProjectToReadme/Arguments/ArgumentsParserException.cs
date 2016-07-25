using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectToReadme.Arguments
{
    public class ArgumentsParserException : Exception
    {
        public ArgumentsParserException()
        {
        }

        public ArgumentsParserException(string message) : base(message)
        {
        }

    }
}
