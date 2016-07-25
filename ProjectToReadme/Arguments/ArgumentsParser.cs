using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectToReadme.Arguments
{
    public class ArgumentsParser
    {
        private string[] _cmdArgs = new string[255];
        public ArgumentsParser(string[] cmdArgs)
        {
            _cmdArgs = cmdArgs;
        }

        public ArgumentsList Parse()
        {
            ArgumentsList arguments = new ArgumentsList();
            for (int index = 0; index < _cmdArgs.Length; index += 2)
            {
                arguments.Add(GetArgument(new string[] {_cmdArgs[index], _cmdArgs[index + 1]}));
            }
            return arguments;
        }

        private Argument GetArgument(string[] argsPair)
        {
            if (argsPair.Length == 2)
            {
                Argument newArgument = new Argument();
                switch (argsPair[0])
                {
                    case "-s":
                    case "-S":
                    {
                        newArgument.Type = ArgumentType.SourceFile;
                        break;
                    }
                    case "-t":
                    case "-T":
                    {
                        newArgument.Type = ArgumentType.OutputType;
                        break;
                    }
                    case "-o":
                    case "-O":
                    {
                        newArgument.Type = ArgumentType.OutputFile;
                        break;
                    }
                    default:
                    {
                        throw new ArgumentsParserException("Not a valid command line argument: " + argsPair[0]);
                    }
                }
                newArgument.Value = argsPair[1];
                return newArgument;
            }
            return null;
        }
    }
}
