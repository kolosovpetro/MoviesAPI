using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MoviesAPI.Core
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
                //LoggerToFile.PrintToLog("Main successful.");
            }
            catch (Exception e)
            {
                //LoggerToFile.PrintExceptionToFile(e);
                Console.WriteLine(e);
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            try
            {
                var result =  Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

                //LoggerToFile.PrintToLog("CreateHostBuilder successful.");
                
                return result;
            }
            catch (Exception e)
            {
                //LoggerToFile.PrintExceptionToFile(e);
                Console.WriteLine(e);
                throw;
            }
        }
    }
}