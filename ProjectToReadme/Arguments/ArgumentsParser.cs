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
                ArgumentType newType = ArgumentType.None;
                switch (argsPair[0])
                {
                    case "-s":
                    case "-S":
                    {
                            newType = ArgumentType.SourceFile;
                        break;
                    }
                    case "-f":
                    case "-F":
                    {
                            newType = ArgumentType.OutputFormat;
                        break;
                    }
                    case "-t":
                    case "-T":
                    {
                            newType = ArgumentType.OutputType;
                        break;
                    }
                    default:
                    {
                        throw new ArgumentsParserException("Not a valid command line argument: " + argsPair[0]);
                    }
                }

                Argument newArgument = new Argument();
                if (ValidateArgumentValue(newType, argsPair[1]))
                {
                    newArgument.Value = argsPair[1];
                    newArgument.Type = newType;
                }
                else
                {
                    throw new ArgumentsParserException("Not a valid argument value: " + argsPair[1] + " for type: " + newType);
                }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                return newArgument;
            }
            return null;
        }

        private bool ValidateArgumentValue(ArgumentType type, string value)
        {
            if (type == ArgumentType.OutputType)
            {
                if (value == "Text" || value == "File")
                    return true;
            }

            if (type == ArgumentType.OutputFormat)
            {
                if (value == "Markdown" || value == "Html" || value == "Text")
                    return true;
            }
            if (type == ArgumentType.SourceFile)
                return true;
            return false;
        }
    }
}
