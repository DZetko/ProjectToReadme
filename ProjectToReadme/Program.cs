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
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectToReadme.Projects;
using ProjectToReadme.Arguments;

namespace ProjectToReadme
{
    class Program
    {
        public static ArgumentsList arguments = new ArgumentsList();
        public static Project project = new Project();
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                ArgumentsParser parser = new ArgumentsParser(args);
                arguments = parser.Parse();
            }
            else
            {
                Environment.Exit(1);
            }

            project = ParseProject();

            if (GenerateReadme())
            {
                Console.WriteLine($"README generated successfully!{Environment.NewLine}Ending the program in 5 seconds.");
            }
            else
            {
                Console.WriteLine($"README generation failed!{Environment.NewLine}Ending the program in 5 seconds.");
            }

            Stopwatch watch = Stopwatch.StartNew();
            while(true)
            {
                if (watch.ElapsedMilliseconds == 5000)
                {
                    Environment.Exit(0);
                }
            }
        }

        public static void Exit(string message)
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

        public static Project ParseProject()
        {
            string json = String.Empty;
            string file = arguments[ArgumentType.SourceFile].Value;
            if (File.Exists(file))
            {
                using (StreamReader rdr = new StreamReader(file))
                {
                    string line = String.Empty;
                    while ((line = rdr.ReadLine()) != null)
                    {
                        json += line;
                    }
                    Console.WriteLine(json);

                    return JsonConvert.DeserializeObject<Project>(json);
                }
            }
            Exit("No file called " + file + " found.");
            return null;
        }

        public static bool GenerateReadme()
        {
            string output = String.Empty;
            string outputFile = String.Empty;
            switch (arguments[ArgumentType.OutputFormat].Value)
            {
                case "Markdown":
                {
                    outputFile = "README.md";
                    output = GenerateMarkdownReadme();
                    break;
                }
                case "Html":
                {
                    outputFile = "README.html";
                    output = GenerateHtmlReadme();
                    break;
                }
                case "Text":
                {
                    outputFile = "README.txt";
                    output = GenerateTextReadme();
                    break;
                }
                default:
                {
                    break;
                }
            }

            if (arguments[ArgumentType.OutputType].Value == "Text")
            {
                Console.WriteLine(output);
                return true;
            }

            using (StreamWriter wrtr = new StreamWriter(outputFile))
            {
                wrtr.WriteLine(output);
                return true;
            }
        }

        private static string GenerateMarkdownReadme()
        {
            string output = String.Empty;
            if (project.Name != null)
                output += $"# {project.Name}";
            if (project.Version != null)
                output += $" (version {project.Version}){Environment.NewLine}";
            if (project.Description != null)
                output += $"## Description{Environment.NewLine}{project.Description}{Environment.NewLine}";

            if (project.Authors != null)
            {
                output += $"## Authors{Environment.NewLine}";
                foreach (string author in project.Authors)
                    output += $"* {author}{Environment.NewLine}";
            }

            if (project.Dependencies != null)
            {
                output += $"## Dependencies{Environment.NewLine}";
                foreach (var dependency in project.Dependencies)
                {
                    if (dependency.Value.GetType() == typeof(ExpandoObject))
                    {
                        var innerDependency = (ExpandoObject)dependency.Value;
                        output += $"* {dependency.Key} (version: ";
                        foreach (var innerDependencyProperty in innerDependency)
                            if (innerDependencyProperty.Key == "version")
                                output += innerDependencyProperty.Value + $"){Environment.NewLine}";
                    }
                    else
                    {
                        output += $"* {dependency.Key} (version: {dependency.Value}){Environment.NewLine}";
                    }
                }
            }

            if (project.PackOptions != null)
            {
                output += $"## Links{Environment.NewLine}";
                foreach (var packOption in project.PackOptions)
                {
                    string packOptionFriendlyName = String.Empty;
                    if (packOption.Key == "projectUrl")
                    {
                        output += $"Project: <{packOption.Value}>";
                    }
                    else if (packOption.Key == "licenseUrl")
                    {
                        output += $"License: <{packOption.Value}>";
                    }

                    output += $"{Environment.NewLine}{Environment.NewLine}";
                }
            }
            if (project.Copyright != null)
                output += $"## Copyright{Environment.NewLine}{project.Copyright}";
            return output;
        }

        private static string GenerateHtmlReadme()
        {
            string markdownVersion = GenerateMarkdownReadme();
            return CommonMark.CommonMarkConverter.Convert(markdownVersion);
        }
        #region Text README
        private static string GenerateTextReadme()
        {
            string output = String.Empty;
            if (project.Name != null)
                output += $"{project.Name}";
            if (project.Version != null)
                output += $" (version {project.Version}){Environment.NewLine}{GetSeparator(project.Name.Length + 10 + project.Version.Length + 1, "=")}{Environment.NewLine}";
            if (project.Description != null)
                output += $"Description{Environment.NewLine}{project.Description}{Environment.NewLine}";

            if (project.Authors != null)
            {
                output += $"Authors{Environment.NewLine}";
                foreach (string author in project.Authors)
                    output += $"--> {author}{Environment.NewLine}";
            }

            if (project.Dependencies != null)
            {
                output += $"Dependencies{Environment.NewLine}";
                foreach (var dependency in project.Dependencies)
                {
                    if (dependency.Value.GetType() == typeof(ExpandoObject))
                    {
                        var innerDependency = (ExpandoObject)dependency.Value;
                        output += $"--> {dependency.Key} (version: ";
                        foreach (var innerDependencyProperty in innerDependency)
                            if (innerDependencyProperty.Key == "version")
                                output += innerDependencyProperty.Value + $"){Environment.NewLine}";
                    }
                    else
                    {
                        output += $"--> {dependency.Key} (version: {dependency.Value}){Environment.NewLine}";
                    }
                }
            }

            if (project.PackOptions != null)
            {
                output += $"Links{Environment.NewLine}";
                foreach (var packOption in project.PackOptions)
                {
                    string packOptionFriendlyName = String.Empty;
                    if (packOption.Key == "projectUrl")
                    {
                        output += $"Project: <{packOption.Value}>{Environment.NewLine}";
                    }
                    else if (packOption.Key == "licenseUrl")
                    {
                        output += $"License: <{packOption.Value}>{Environment.NewLine}";
                    }
                }
            }
            if (project.Copyright != null)
                output += $"Copyright{Environment.NewLine}{project.Copyright}";
            return output;
        }

        private static string GetSeparator(int length, string separator)
        {
            string output = String.Empty;
            for (int i = 0; i > length; i++)
            {
                output += separator;
            }
            return output;
        }
        #endregion
    }
}