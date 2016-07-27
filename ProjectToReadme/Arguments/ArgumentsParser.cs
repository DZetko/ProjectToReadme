/*
    Copyright 2016 Daniel Zikmund
    
    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at
    
        http://www.apache.org/licenses/LICENSE-2.0
    
    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Console.WriteLine(_cmdArgs[index + 1]);
            }
            return arguments;
        }

        private static void Exit(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Will exit in 5 seconds");
            Stopwatch watch = Stopwatch.StartNew();
            while (true)
            {
                if (watch.ElapsedMilliseconds == 5000)
                {
                    Environment.Exit(0);
                }
            }
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
                        Exit("Not a valid command line argument: " + argsPair[0]);
                        break;
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
                    Exit("Not a valid argument value: " + argsPair[1] + " for type: " + newType);
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
