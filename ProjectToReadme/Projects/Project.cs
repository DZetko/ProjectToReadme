using System;
using System.Collections.Generic;
using System.Dynamic;
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
        [JsonProperty("authors")]
        public List<string> Authors { get; set; }
        [JsonProperty("dependencies")]
        public ExpandoObject Dependencies { get; set; }
        [JsonProperty("packOptions")]
        public ExpandoObject PackOptions { get; set; }
    }
}
