namespace Zip.InstallmentsService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) =>
            {
                var builtConfig = config.Build();
                var secretsPath = builtConfig["secrets_path"];

                if (secretsPath != null) config.AddKeyPerFile(secretsPath, true);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).UseConsoleLifetime();
    }
}