using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Data;

namespace MyVotingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                string script1 = File.ReadAllText("..\\create-database.sql");
                context.Database.ExecuteSqlRaw(script1);

                string script2 = File.ReadAllText("..\\load_dummy_data.sql");
                context.Database.ExecuteSqlRaw(script2);
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}