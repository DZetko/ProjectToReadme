using System;
using System.Collections.Generic;
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
    }
}
