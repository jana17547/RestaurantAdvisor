using Neo4j.Driver.Extensions;

namespace Server.Models
{
    public class TypeKitchen
    {
        [Neo4jProperty(Name = "type")]
        public string type { get; set; }
      
    }
}