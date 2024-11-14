using Neo4j.Driver.Extensions;

namespace Server.Models
{
    public class Restaurant
    {
        [Neo4jProperty(Name = "name")]
        public string Name { get; set; }
        [Neo4jProperty(Name = "address")]
        public string Address { get; set; }
        [Neo4jProperty(Name = "contact")]
        public string Contact { get; set; }
        [Neo4jProperty(Name = "decription")]
        public string Description { get; set; }
        [Neo4jProperty(Name = "img")]
        public string Img { get; set; }
      
        
        [Neo4jProperty(Name = "rating")]
        public double Rating { get; set; }
        
    }
}
