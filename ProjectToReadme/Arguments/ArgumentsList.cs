using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectToReadme.Arguments
{
    public class ArgumentsList : List<Argument>
    {
        public Argument this[ArgumentType type]
        {
            get
            {
                foreach (Argument argument in this)
                {
                    if (argument.Type == type)
                    {
                        return argument;
                    }
                }
                return null;
            }
        }
    }
}
