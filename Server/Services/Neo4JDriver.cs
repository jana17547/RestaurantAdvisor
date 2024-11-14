using Neo4j.Driver;

namespace Server.Services
{
    public class Neo4JDriver
    {
        private IAsyncSession session;
        public IAsyncSession Session 
        {
            get
            {
                if (session == null)
                {
                    var _Driver = GraphDatabase.Driver("neo4j+s://d03d72aa.databases.neo4j.io", AuthTokens.Basic("neo4j", "D7A6DDHwxFE0QIQRV72tOfmn6Sl-QX_G5H6L6V8Kp0k"));
                    session = _Driver.AsyncSession();
                }
                return session;
            }
        }
    }
}
