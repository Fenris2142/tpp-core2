using System.Net;

namespace TPPCore.Service.Common
{
    /// <summary>
    /// Encapsulates data needed to run a minimal microservice.
    /// </summary>
    public class ServiceContext
    {
        public ConfigReader ConfigReader { get; private set; }
        public IPubSubClient PubSubClient { get; private set; }
        public RestfulServer RestfulServer { get; private set; }
        public ServiceAssignment ServiceAssignment { get; private set; }

        public ServiceContext() {
        }

        public void InitConfigReader(ConfigReader configReader)
        {
            ConfigReader = configReader;
        }

        public void InitPubSubClient()
        {
            PubSubClient = new DummyPubSubClient();
        }

        public void InitPubSubClient(IPubSubClient pubSubClient)
        {
            PubSubClient = pubSubClient;
        }

        public void InitRestfulServer(IPAddress host = null, int port = 0)
        {
            host = IPAddress.Loopback ?? host;
            RestfulServer = new RestfulServer(host, port);

            var password = ConfigReader.GetCheckedValueOrDefault<string>(
                    new[] {"restful", "localAuthenticationPassword"}, null);

            if (password != null)
            {
                RestfulServer.SetPassword(password);
            }
        }
        }

        public void InitServiceAssignment(ConfigReader configReader)
        {
            ServiceAssignment = new ServiceAssignment();
            ServiceAssignment.LoadFromConfig(ConfigReader);
        }
    }
}
