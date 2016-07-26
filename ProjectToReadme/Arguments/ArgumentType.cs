using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectToReadme.Arguments
{
    public enum ArgumentType
    {
        //project.json
        SourceFile,
        //Markdown, Html or Text
        OutputFormat,
        //Text or File
        OutputType,
        None  
    }
}
