using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectToReadme.Projects
{
    public class Project
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("Version")]
        public string Version { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("copyright")]
        public string Copyright { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("entryPoint")]
        public string EntryPoint { get; set; }
        [JsonProperty("testRunner")]
        public string TestRunner { get; set; }
        [JsonProperty("authors")]
        public List<string> Authors { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("embedInteropTypes")]
        public bool EmbedInteropTypes { get; set; }
        [JsonProperty("preprocess")]
        public List<string> Preprocess { get; set; }
        [JsonProperty("shared")]
        public List<string> Shared { get; set; }
        [JsonProperty("dependencies")]
        public string Dependencies { get; set; }
        [JsonProperty("tools")]
        public string Tools { get; set; }
        [JsonProperty("scripts")]
        public string Scripts { get; set; }
        [JsonProperty("buildOptions")]
        public string BuildOptions { get; set; }
        [JsonProperty("publishOptions")]
        public string PublishOptions { get; set; }
        [JsonProperty("runtimeOptions")]
        public string RuntimeOptions { get; set; }
        [JsonProperty("packOptions")]
        public string PackOptions { get; set; }
        [JsonProperty("analyzerOptions")]
        public string AnalyzerOptions { get; set; }
        [JsonProperty("configurations")]
        public string Configurations { get; set; }
        [JsonProperty("frameworks")]
        public string Frameworks { get; set; }
    }
}
