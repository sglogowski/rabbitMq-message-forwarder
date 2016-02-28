using CommandLine;

namespace MessageForwarder
{
    internal class Arguments
    {
        [Option("fromQueue", Required = true)]
        public string FromQueue { get; set; }

        [Option("toExchange", Required = true)]
        public string ToExchange { get; set; }

        [Option("host", Required = false, DefaultValue = "localhost")]
        public string Host { get; set; }

        [Option("port", Required = false, DefaultValue = 5672)]
        public int Port { get; set; }

        [Option("userName", Required = false, DefaultValue = "guest")]
        public string UserName { get; set; }

        [Option("password", Required = false, DefaultValue = "guest")]
        public string Password { get; set; }

        [Option("publisherRoutingKey", Required = false, DefaultValue = "")]
        public string PublisherRoutingKey { get; set; }
    }
}
