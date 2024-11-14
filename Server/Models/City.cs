using Neo4j.Driver.Extensions;

namespace Server.Models
{
    public class City
    {
        [Neo4jProperty(Name = "nameCity")]
        public string nameCity { get; set; }

         [Neo4jProperty(Name = "country")]
        public string country { get; set; }

    }
}