namespace MasterProjectWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var config = new ConfigurationBuilder().AddJsonFile($"appsettings.{env}.json", optional: false).Build();
                var url = config.Providers.FirstOrDefault();
                string port = string.Empty;
                if (url!=null &&  url.TryGet("Port", out string? value))
                {
                    if (value != null)
                    {
                        port = value;
                    }
                }
                webBuilder.UseStartup<Startup>().UseUrls(new[] { $"http://localhost:{port}" });
            });
    }
}



