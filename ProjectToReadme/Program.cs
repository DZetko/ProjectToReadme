using System;
using System.Collections.Generic;
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

            GenerateReadme();

            Console.ReadLine();
        }

        public static Project ParseProject()
        {
            string json = String.Empty;
            using (StreamReader rdr = new StreamReader(arguments[ArgumentType.SourceFile].Value))
            {
                string line = String.Empty;
                while ((line = rdr.ReadLine()) != null)
                {
                    json += line;
                }

                return JsonConvert.DeserializeObject<Project>(json);
            }
        }

        public static void GenerateReadme()
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
            using (StreamWriter wrtr = new StreamWriter(outputFile))
            {
                wrtr.WriteLine(output);
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
    }
}